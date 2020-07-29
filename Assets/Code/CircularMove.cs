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
            Planet planet = gameObject.GetComponent<Planet>();
            if (planet.getRadSun() == 0)
                return;
            planet.addToAngle(planet.getSpeed() * Time.deltaTime);
            float x = (float)Math.Cos(planet.getAngle()) * planet.getRadSun();
            float z = (float)Math.Sin(planet.getAngle()) * planet.getRadSun();
            transform.position = new Vector3(x, 0, z);
        }
    }
}
