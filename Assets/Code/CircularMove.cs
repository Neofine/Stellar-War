using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMove : MonoBehaviour {

    void Update() {
        print(Game.getPlanets().Count);
        foreach (Planet planet in Game.getPlanets()) {
            print("WORKS");
            planet.addToAngle(planet.getSpeed() * Time.deltaTime);
            float x = (float)Math.Cos(planet.getAngle()) * planet.getRadius();
            float z = (float)Math.Sin(planet.getAngle()) * planet.getRadius();
            planet.getObj().transform.position = new Vector3(x, 0, z);
        }
    }
}
