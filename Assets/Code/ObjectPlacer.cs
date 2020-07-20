using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour {

    private GameObject objectDuringPlacement = null;
    private GameObject objectPlaced = null;
    private float mouseRotation = 0f;

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            if (objectDuringPlacement == null) {
                objectDuringPlacement = Instantiate(GameObject.Find("mainBase"));
            }
            else {
                Destroy(objectDuringPlacement);
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

        if (Input.GetKeyDown(KeyCode.S) && objectDuringPlacement != null) {
            if (objectPlaced != null)
                objectDuringPlacement.transform.parent = objectPlaced.transform;
            objectDuringPlacement = null;
        }
    }
}
