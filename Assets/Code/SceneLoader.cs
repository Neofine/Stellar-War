using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour {

    private bool isSpawned = false;
    private string whichScnShouldBe;
    private Planet spawnWhat;
    private Vector3 saved;
    private Quaternion savedRotation;
    private GameObject spawned;

    void Update() {
        if (!isSpawned && spawnWhat != null) {
            Game.inspectModeOn();
            spawned = Instantiate(spawnWhat.getObj(), new Vector3(1000f, 1000f, 1100f), Quaternion.identity);
            spawned.name = "PLANET COPY";
            //GameObject.Find("Jet").GetComponent(Flight).enabled = true;
            spawned.GetComponent<RotatePlanet>().enabled = true;
            spawned.GetComponent<ObjectPlacer>().enabled = true;
            Camera.main.GetComponent<CameraEdgeMovement>().enabled = false;
            //Destroy(spawned.GetComponent<CircularMove>());
            isSpawned = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isSpawned) {
            Game.inspectModeOff();
            Camera.main.transform.position = saved;
            Camera.main.transform.rotation = savedRotation;
            spawned.GetComponent<RotatePlanet>().enabled = false;
            spawned.GetComponent<ObjectPlacer>().enabled = false;
            Camera.main.GetComponent<CameraEdgeMovement>().enabled = true;
            //spawned.AddComponent<CircularMove>();
            spawnWhat.changePlanet(spawned);
        }
    }

    public void loadScene(string which, Planet with) {
        if (spawned != null)
            Destroy(spawned);
        //SceneManager.LoadScene(which, LoadSceneMode.Additive);
        saved = Camera.main.transform.position;
        savedRotation = Camera.main.transform.rotation;
        Camera.main.transform.position = new Vector3(1000f, 1000f, 1000f);
        Camera.main.transform.LookAt(new Vector3(1000f, 1000f, 1100f));
        whichScnShouldBe = which;
        spawnWhat = with;
        isSpawned = false;
    }
}
