using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToPlanetCamera : MonoBehaviour {
    private Camera planetCamera;
    public Camera gameCamera;

    private void Start() {
        //Camera.main = gameCamera;
        gameCamera.enabled = true;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SwitchBack();
        }
    }

    public void SwitchCamera(Planet planet) {
        foreach(Transform child in planet.transform) {  
            if (child.gameObject.name == "Camera") {
                planetCamera = child.gameObject.GetComponent<Camera>();
                break;
            }
        }
        planetCamera.enabled = true;
        gameCamera.enabled = false;
        planetCamera.gameObject.AddComponent<CameraCircleMovement>();
        Game.inspectModeOn();

        gameCamera.GetComponent<ObjectClick>().enabled = false;
        gameCamera.GetComponent<Mesh>().enabled = false;
        gameCamera.GetComponent<CameraEdgeMovement>().enabled = false;
    }

    public void SwitchBack() {
        planetCamera.enabled = false;
        gameCamera.enabled = true;
        Game.inspectModeOff();

        Destroy(planetCamera.gameObject.GetComponent<CameraCircleMovement>());

        Camera.main.GetComponent<ObjectClick>().enabled = true;
        Camera.main.GetComponent<Mesh>().enabled = true;
        Camera.main.GetComponent<CameraEdgeMovement>().enabled = true;
    }
}
