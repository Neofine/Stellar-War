﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralPlanet : Planet {

    void Start() {
        radiusAroudSun = 0f;

        angle = 0f;
        speed = 2 * Mathf.PI / 60;
    }
}
