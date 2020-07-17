using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMove : MonoBehaviour {

    private bool stopPlanets = false;
    void Update() {
        if (Input.GetKeyDown("f"))
            stopPlanets = !stopPlanets;
        if (!stopPlanets) {
            foreach (Planet planet in Game.getPlanets()) {
                planet.addToAngle(planet.getSpeed() * Time.deltaTime);
                float x = (float)Math.Cos(planet.getAngle()) * planet.getRadSun();
                float z = (float)Math.Sin(planet.getAngle()) * planet.getRadSun();
                planet.getObj().transform.position = new Vector3(x, 0, z);
            }
        }
    }
}
