using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeMovement : MonoBehaviour
{
    float vertSpeed = 100f;
    float horSpeed = 130f;
    float scrollSpeed = 100f;
    float minFov = 5f;
    float maxFov = 70f;
    float sensitivity = 10f;
    void Update() {
        Vector3 mousePos = Input.mousePosition;
        if (mousePos.y >= Screen.height * 0.99) {
            move(0, 0, vertSpeed, Time.deltaTime);
        }
        else if (mousePos.y <= Screen.height * 0.01) {
            move(0, 0, -vertSpeed, Time.deltaTime);
        }

        if (mousePos.x >= Screen.width * 0.99) {
            move(horSpeed, 0, 0, Time.deltaTime);
        }
        else if (mousePos.x <= Screen.width * 0.01) {
            move(-horSpeed, 0, 0, Time.deltaTime);
        }

        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    public void move(float x, float y, float z, float time) {
        this.transform.position = this.transform.position + new Vector3(x, y, z) * time;
    }
}
