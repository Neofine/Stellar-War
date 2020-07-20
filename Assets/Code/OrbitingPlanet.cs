using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingPlanet : Planet {

    void Start() {
        radiusAroudSun = 150f;

        angle = Random.Range(0, 360f);
        speed = 2 * Mathf.PI / Random.Range(60, 120f);
    }
}
