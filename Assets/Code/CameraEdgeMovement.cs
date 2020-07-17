using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeMovement : MonoBehaviour
{
    float vertSpeed = 100f;
    float horSpeed = 130f;
    float scrollSpeed = 100f;
    void Update() {
        Vector3 mousePos = Input.mousePosition;
        if (mousePos.y >= Screen.height * 0.95) {
            move(0, 0, vertSpeed, Time.deltaTime);
        }
        else if (mousePos.y <= Screen.height * 0.05) {
            move(0, 0, -vertSpeed, Time.deltaTime);
        }

        if (mousePos.x >= Screen.width * 0.95) {
            move(horSpeed, 0, 0, Time.deltaTime);
        }
        else if (mousePos.x <= Screen.width * 0.05) {
            move(-horSpeed, 0, 0, Time.deltaTime);
        }

        if (!Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y != 0) {
            move(0, scrollSpeed * Input.mouseScrollDelta.y, 0, Time.deltaTime);
        }
    }

    public void move(float x, float y, float z, float time) {
        this.transform.position = this.transform.position + new Vector3(x, y, z) * time;
    }
}
