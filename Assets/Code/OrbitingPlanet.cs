using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrbitingPlanet : Planet {
    public OrbitingPlanet() {
        radiusAroudSun = 150f;
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            SaveSystem.savePlanet(this);
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            load();
        }
    }

    private void Start() {
        angle = Random.Range(0, 360f);
        speed = 2 * Mathf.PI / Random.Range(60, 120f);
        number = Game.getNumber();
        float width = GetComponent<Collider>().bounds.size.x;
        float height = GetComponent<Collider>().bounds.size.y;
        float length = GetComponent<Collider>().bounds.size.z;
        planetRadius = Math.Max(width, Math.Max(height, length)) / 2;
    }
}
