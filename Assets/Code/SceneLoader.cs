using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour {

    private bool isSpawned = false;
    private Planet spawnWhat;
    private Vector3 saved;
    private Quaternion planetRotation;
    private Quaternion savedRotation;
    private GameObject spawned;

    void Update() {
        if (!isSpawned && spawnWhat != null) {
            Game.inspectModeOn();
            spawned = Instantiate(spawnWhat.getObj(), new Vector3(1000f, 1000f, 1100f), planetRotation);
            spawned.name = "PLANET COPY";
            spawned.GetComponent<RotatePlanet>().enabled = true;
            spawned.GetComponent<ObjectPlacer>().enabled = true;
            spawned.GetComponent<CircularMove>().enabled = false;
            spawned.GetComponent<SpawningSurfaceObjects>().enabled = false;
            Camera.main.GetComponent<CameraEdgeMovement>().enabled = false;
            isSpawned = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isSpawned) {
            Game.inspectModeOff();
            Camera.main.transform.position = saved;
            Camera.main.transform.rotation = savedRotation;
            spawned.GetComponent<RotatePlanet>().enabled = false;
            spawned.GetComponent<ObjectPlacer>().enabled = false;
            spawned.GetComponent<CircularMove>().enabled = true;
            Camera.main.GetComponent<CameraEdgeMovement>().enabled = true;
            spawnWhat.changePlanet(spawned);
        }
    }

    public Planet getOriginal() {
        return spawnWhat;
    }

    public void loadScene(Planet with) {
        if (spawned != null)
            Destroy(spawned);
        //SceneManager.LoadScene(which, LoadSceneMode.Additive);
        saved = Camera.main.transform.position;
        savedRotation = Camera.main.transform.rotation;
        Camera.main.transform.position = new Vector3(1000f, 1000f, 1000f);
        Camera.main.transform.LookAt(new Vector3(1000f, 1000f, 1100f));
        planetRotation = with.getObj().transform.rotation;
        spawnWhat = with;
        isSpawned = false;
    }
}
