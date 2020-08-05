using UnityEngine;
using System.Collections;

public class RotatePlanet : MonoBehaviour {
    private float sensitivity;
    private Vector3 prevPos;
    private Vector3 deltaMouse;
    private Vector3 changeRot;
    private bool amIClicking;
    private int cameraState;

    void Start() {
        sensitivity = 0.4f;
        changeRot = Vector3.zero;
        cameraState = 0;
    }

    void Update() {
        if (amIClicking) {
            deltaMouse = (Input.mousePosition - prevPos);
            changeRot.y = -(deltaMouse.x + deltaMouse.y) * sensitivity;
            transform.Rotate(changeRot);
            prevPos = Input.mousePosition;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            goCameraUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            goCameraDown();
        }
    }

    void goCameraUp() {
        cameraState++;
        if (cameraState > 1) {
            cameraState = 1;
            return;
        }
        if (cameraState == 1) {
            cameraNorthPole();
        }
        else if (cameraState == 0) {
            cameraEquator();
        }
    }

    void goCameraDown() {
        cameraState--;
        if (cameraState < -1) {
            cameraState = -1;
            return;
        }
        if (cameraState == -1) {
            cameraSouthPole();
        }
        else if (cameraState == 0) {
            cameraEquator();
        }
    }

    void cameraNorthPole() {
        Camera.main.transform.position = new Vector3(1000f, 1100f, 1100f);
        Camera.main.transform.LookAt(new Vector3(1000f, 1000f, 1100f));
    }

    void cameraEquator() {
        Camera.main.transform.position = new Vector3(1000f, 1000f, 1000f);
        Camera.main.transform.LookAt(new Vector3(1000f, 1000f, 1100f));
    }

    void cameraSouthPole() {
        Camera.main.transform.position = new Vector3(1000f, 900f, 1100f);
        Camera.main.transform.LookAt(new Vector3(1000f, 1000f, 1100f));
    }

    void OnMouseDown() {
        amIClicking = true;
        prevPos = Input.mousePosition;
    }

    void OnMouseUp() {
        amIClicking = false;
    }
}