using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCircleMovement : MonoBehaviour {
    private Planet planet;
    private float cameraRadius;
    private bool nowSub, nowAdd;

    void Start() {
        planet = transform.parent.GetComponent<Planet>();
        cameraRadius = VectorUtility.vecLength(transform.localPosition, Vector3.zero);
    }

    void Update() {
        Vector3 coords = transform.localPosition;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (coords.z > 0 || (coords.z == 0 && nowAdd)) {
                coords.x += 0.1f;
                
                if (coords.x >= cameraRadius) {
                    coords.x = cameraRadius;
                    nowSub = true;
                }
                nowAdd = false;
                coords.z = Mathf.Sqrt(cameraRadius * cameraRadius - coords.x * coords.x);
                
                transform.localPosition = coords;
            }
            else if (coords.z < 0 || (coords.z == 0 && nowSub)) {
                coords.x -= 0.1f;
                if (coords.x <= -cameraRadius) {
                    coords.x = -cameraRadius;
                    nowAdd = true;
                }
                nowSub = false;
                coords.z = Mathf.Sqrt(cameraRadius * cameraRadius - coords.x * coords.x);
                coords.z = -coords.z;

                transform.localPosition = coords;
            }
            transform.LookAt(planet.getObj().transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (coords.z > 0 || (coords.z == 0 && nowSub)) {
                coords.x -= 0.1f;
                if (coords.x <= -cameraRadius) {
                    coords.x = -cameraRadius;
                    nowAdd = true;
                }
                nowSub = false;
                
                coords.z = Mathf.Sqrt(cameraRadius * cameraRadius - coords.x * coords.x);
                
                transform.localPosition = coords;
            }
            else if (coords.z < 0 || (coords.z == 0 && nowAdd)) {
                coords.x += 0.1f;
                if (coords.x >= cameraRadius) {
                    coords.x = cameraRadius;
                    nowSub = true;
                }
                nowAdd = false;
                coords.z = Mathf.Sqrt(cameraRadius * cameraRadius - coords.x * coords.x);
                coords.z = -coords.z;

                transform.localPosition = coords;
            }
            transform.LookAt(planet.getObj().transform.position);
        }
    }
}
