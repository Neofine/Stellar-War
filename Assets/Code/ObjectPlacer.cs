using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour {

    private GameObject objectDuringPlacement = null;
    private GameObject objectPlaced = null;
    private SpawningSurfaceObjects spwn;
    private Renderer rend;
    private Color color;
    private float mouseRotation = 0f;
    private bool notHere = false;

    private void Start() {
        spwn = Game.getScnLoad().getOriginal().GetComponent<SpawningSurfaceObjects>();
        rend = GetComponent<Renderer>();
        color = rend.material.color;
        foreach (Renderer r in objectDuringPlacement.GetComponentsInChildren<Renderer>()) {
            r.material.color = Color.white;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            if (objectDuringPlacement == null) {
                objectDuringPlacement = Instantiate(GameObject.Find("mainBase"));
            }
            else {
                Destroy(objectDuringPlacement);
            }
        }

        if (objectDuringPlacement != null && spwn.isFrobidden(objectDuringPlacement.transform.position - new Vector3(1000f, 1000f, 1100f))) {
            notHere = true;
            foreach (Renderer r in objectDuringPlacement.GetComponentsInChildren<Renderer>()) {
                r.material.color = Color.white;
            }
        }
        else if (objectDuringPlacement != null) {
            notHere = false;
            foreach (Renderer r in objectDuringPlacement.GetComponentsInChildren<Renderer>()) {
                r.material.color = Color.blue;
            }
        }
            

        if (objectDuringPlacement != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            if (Physics.Raycast(ray, out hit, 100000f, layerMask)) {
                objectDuringPlacement.transform.position = hit.point;
                objectDuringPlacement.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                objectPlaced = hit.transform.gameObject;
            }

            mouseRotation += Input.mouseScrollDelta.y;
            objectDuringPlacement.transform.Rotate(Vector3.up, mouseRotation * 10f);
        }

        if (Input.GetKeyDown(KeyCode.S) && objectDuringPlacement != null && !notHere) {
            if (objectPlaced != null)
                objectDuringPlacement.transform.parent = objectPlaced.transform;
            objectDuringPlacement = null;
            spwn.forbid(objectDuringPlacement.transform.position - new Vector3(1000f, 1000f, 1100f));
        }
    }
}
