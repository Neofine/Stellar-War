using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour {
    private GameObject objectDuringPlacement = null;
    private GameObject objectPlaced = null;
    private List<Color> rend;
    private float mouseRotation = 0f;
    private Planet planet1;

    private void Start() {
        planet1 = GetComponent<Planet>();
        rend = new List<Color>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            if (objectDuringPlacement == null) {
                objectDuringPlacement = Instantiate(GameObject.Find("BmainBase"));
                rend.Clear();
                foreach (Renderer r in objectDuringPlacement.GetComponentsInChildren<Renderer>()) {
                    rend.Add(r.material.color);
                }
            }
            else {
                Destroy(objectDuringPlacement);
            }
        }

        if (objectDuringPlacement != null && planet1.isBlocked()) {
            foreach (Renderer r in objectDuringPlacement.GetComponentsInChildren<Renderer>()) {
                r.material.color = Color.red;
            }
        }
        else if (objectDuringPlacement != null) {
            foreach (Renderer r in objectDuringPlacement.GetComponentsInChildren<Renderer>()) {
                r.material.color = Color.green;
            }
        }

        if (objectDuringPlacement != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            int layerMask = (1 << 8) +  (1 << 9);
            layerMask = ~layerMask;
            if (Physics.Raycast(ray, out hit, 100000f, layerMask)) {
                objectDuringPlacement.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                objectDuringPlacement.transform.position = hit.point;
                objectDuringPlacement.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                objectPlaced = hit.transform.gameObject;
            }
            else {
                objectDuringPlacement.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }

            mouseRotation += Input.mouseScrollDelta.y;
            objectDuringPlacement.transform.Rotate(Vector3.up, mouseRotation * 10f);
        }

        if (Input.GetKeyDown(KeyCode.S) && objectDuringPlacement != null && !planet1.isBlocked()) {
            int idx = 0;
            foreach (Renderer r in objectDuringPlacement.GetComponentsInChildren<Renderer>()) {
                r.material.color = rend[idx++];
            }

            if (objectPlaced != null) {
                Building building = objectDuringPlacement.AddComponent<Building>();
                objectPlaced.GetComponent<Planet>().addBuilding(building);
                print(building);
                objectDuringPlacement.transform.parent = objectPlaced.transform;
            }

            objectDuringPlacement.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            objectDuringPlacement = null;
        }
    }
}
