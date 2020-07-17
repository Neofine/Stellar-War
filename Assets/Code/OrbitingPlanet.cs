using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingPlanet : Planet {

    void Start() {
        radiusAroudSun = 150f;

        angle = 0f;
        speed = 2 * Mathf.PI / 60;
    }
}
