using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClickCoords {
    public static Vector3 getCords() {
        Plane plane = new Plane(Vector3.up, 0f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float planeDst;
        if (plane.Raycast(ray, out planeDst)) {
            return ray.GetPoint(planeDst);
        }
        return Vector3.zero;
    }
    
    public static Vector3 cameraCoords() {
        Vector3 startPos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(startPos);
    }

    public static float getX(Vector3 shipPos, Vector3 laserShot) {
        Vector3 camPos = cameraCoords();
        float perpd = camPos.x;
        return camPos.x + ((laserShot.x - perpd) / camPos.y * (camPos.y - Game.getMesh().getHeight()));
    }

    public static float getZ(Vector3 shipPos, Vector3 laserShot) {
        Vector3 camPos = cameraCoords();
        float perpd = camPos.z;
        return camPos.z + ((laserShot.z - perpd) / camPos.y * (camPos.y - Game.getMesh().getHeight()));
    }

    public static float getXSpec(Vector3 shipPos, Vector3 laserShot) {
        Vector3 camPos = cameraCoords();
        float perpd = camPos.x;
        return camPos.x + ((laserShot.x - perpd) / camPos.y * (camPos.y - shipPos.y));
    }

    public static float getZSpec(Vector3 shipPos, Vector3 laserShot) {
        Vector3 camPos = cameraCoords();
        float perpd = camPos.z;
        return camPos.z + ((laserShot.z - perpd) / camPos.y * (camPos.y - shipPos.y));
    }
}
