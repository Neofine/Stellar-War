using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToPlanetCamera : MonoBehaviour {
    private Camera planetCamera;
    private Camera miniCamera;
    public Camera gameCamera;
    private bool switched;

    public Camera getRealCamera() {
        if (switched)
            return planetCamera;
        return gameCamera;
    }

    private void Start() {
        gameCamera.enabled = true;
        switched = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SwitchBack();
        }
    }

    public void SwitchCamera(Planet planet) {
        hideMiniCamera();
        switched = true;
        foreach(Transform child in planet.getObj().transform) {
            if (child.gameObject.name == "Camera") {
                planetCamera = child.gameObject.GetComponent<Camera>();
                break;
            }
        }
        planetCamera.cullingMask = 1 << 10;
        planetCamera.cullingMask = ~planetCamera.cullingMask;
        planetCamera.rect = new Rect(new Vector2(0, 0), new Vector2(1, 1));
        planetCamera.depth = 0;
        planetCamera.enabled = true;
        gameCamera.enabled = false;
        planetCamera.gameObject.AddComponent<CameraCircleMovement>();
        Game.changeObjClick(planetCamera.gameObject.AddComponent<ObjectClick>());
        planetCamera.gameObject.AddComponent<ObjectPlacer>();

        Game.inspectModeOn();

        gameCamera.GetComponent<ObjectClick>().enabled = false;
        gameCamera.GetComponent<Mesh>().enabled = false;
        gameCamera.GetComponent<CameraEdgeMovement>().enabled = false;
    }

    public void makeMiniCamera(Planet planet) {
        quitMiniCamera();
        foreach (Transform child in planet.getObj().transform) {
            if (child.gameObject.name == "Camera") {
                miniCamera = child.gameObject.GetComponent<Camera>();
                break;
            }
        }
        miniCamera.cullingMask = 1 << 10;
        miniCamera.cullingMask = ~miniCamera.cullingMask;

        miniCamera.depth = 10;
        miniCamera.enabled = true;

        miniCamera.rect = new Rect(new Vector2(0.8f, 0.6f), new Vector2(0.3f, 0.3f));
    }

    public void quitMiniCamera() {
        if (miniCamera != null) {
            miniCamera.depth = -2;
            miniCamera.enabled = false;
            miniCamera = null;
        }
    }

    public void hideMiniCamera() {
        if (miniCamera != null) {
            miniCamera.depth = -2;
        }
    }

    public void showMiniCamera() {
        if (miniCamera != null) {
            miniCamera.depth = 0;
        }
    }

    public void SwitchBack() {
        if (!switched) {
            quitMiniCamera();
            return;
        }
        showMiniCamera();
        Game.changeObjClick(null);
        switched = false;
        planetCamera.enabled = false;
        planetCamera.depth = -2;
        gameCamera.enabled = true;
        Game.inspectModeOff();

        Destroy(planetCamera.gameObject.GetComponent<CameraCircleMovement>());
        Destroy(planetCamera.gameObject.GetComponent<ObjectClick>());
        Destroy(planetCamera.gameObject.GetComponent<ObjectPlacer>());

        Camera.main.GetComponent<ObjectClick>().enabled = true;
        Camera.main.GetComponent<Mesh>().enabled = true;
        Camera.main.GetComponent<CameraEdgeMovement>().enabled = true;
    }
}
