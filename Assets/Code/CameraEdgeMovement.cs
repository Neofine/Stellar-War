using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeMovement : MonoBehaviour
{
    private float vertSpeed = 100f;
    private float horSpeed = 130f;
    private float minFov = 15f;
    private float maxFov = 70f;
    private float zoomSpeed = 10f;
    private float xRotSpeed = 5f;
    private int direction = 0;
    private bool isOn;
    private void Start() {
        isOn = false;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.V))
            isOn = !isOn;
        if (!isOn)
            return;
        Vector3 mousePos = Input.mousePosition;
        if (mousePos.y >= Screen.height * 0.99) {
            goUp(direction);
        }
        else if (mousePos.y <= Screen.height * 0.01) {
            goDown(direction);
        }

        if (mousePos.x >= Screen.width * 0.99) {
            goRight(direction);
        }
        else if (mousePos.x <= Screen.width * 0.01) {
            goLeft(direction);
        }

        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl)) {
            float fov = Camera.main.fieldOfView;
            fov -= Input.mouseScrollDelta.y * zoomSpeed;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            Camera.main.fieldOfView = fov;
        }
        

        if (Input.GetKeyDown(KeyCode.LeftArrow)) { transform.Rotate(new Vector3(0, 90f, 0), Space.World); direction++; }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) { transform.Rotate(new Vector3(0, -90f, 0), Space.World); direction--; }

        if (Input.GetKey(KeyCode.LeftShift)) {
            if (!(transform.localEulerAngles.x <= 5 && Input.mouseScrollDelta.y < 0) && !(transform.localEulerAngles.x >= 89 && Input.mouseScrollDelta.y > 0)) {
                transform.Rotate(new Vector3(Input.mouseScrollDelta.y * xRotSpeed, 0, 0), Space.World);
            }
        }
        direction %= 4;
    }

    private void goUp(int direction) {
        if (direction == 0)
            move(0, 0, vertSpeed, Time.deltaTime);
        else if (direction == 1)
            move(vertSpeed, 0, 0, Time.deltaTime);
        else if (direction == 2)
            move(0, 0, -vertSpeed, Time.deltaTime);
        else
            move(-vertSpeed, 0, 0, Time.deltaTime);
    }

    private void goDown(int direction) {
        if (direction == 0)
            move(0, 0, -vertSpeed, Time.deltaTime);
        else if (direction == 1)
            move(-vertSpeed, 0, 0, Time.deltaTime);
        else if (direction == 2)
            move(0, 0, vertSpeed, Time.deltaTime);
        else
            move(vertSpeed, 0, 0, Time.deltaTime);
    }

    private void goRight(int direction) {
        if (direction == 0)
            move(horSpeed, 0, 0, Time.deltaTime);
        else if (direction == 1)
            move(0, 0, -horSpeed, Time.deltaTime);
        else if (direction == 2)
            move(-horSpeed, 0, 0, Time.deltaTime);
        else
            move(0, 0, horSpeed, Time.deltaTime);
    }

    private void goLeft(int direction) {
        if (direction == 0)
            move(-horSpeed, 0, 0, Time.deltaTime);
        else if (direction == 1)
            move(0, 0, horSpeed, Time.deltaTime);
        else if (direction == 2)
            move(horSpeed, 0, 0, Time.deltaTime);
        else
            move(0, 0, -horSpeed, Time.deltaTime);
    }
    
    public void move(float x, float y, float z, float time) {
        transform.position = this.transform.position + new Vector3(x, y, z) * time;
    }
}
