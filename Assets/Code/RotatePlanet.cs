// code taken from YT tutorial, will be changed soon to my own
using UnityEngine;
using System.Collections;

public class RotatePlanet : MonoBehaviour {

    private Vector3 prevPos;
    private Vector3 posDelta;

    private void Update() {
        if (Input.GetMouseButton(0)) {
            posDelta = Input.mousePosition - prevPos;
            if (Vector3.Dot(transform.up, Vector3.up) >= 0) {
                transform.Rotate(transform.up, -Vector3.Dot(posDelta, Camera.main.transform.right), Space.World);
            }
            else {
                transform.Rotate(transform.up, Vector3.Dot(posDelta, Camera.main.transform.right), Space.World);
            }

            transform.Rotate(Camera.main.transform.right, Vector3.Dot(posDelta, Camera.main.transform.up), Space.World);
        }
        prevPos = Input.mousePosition;
    }

}