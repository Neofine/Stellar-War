using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CentralPlanet : Planet {
    public CentralPlanet() {
        radiusAroudSun = 0f;
        angle = 0f;
    }
    
    private void Start() {
        if (number == 0) {
            buildings = new List<Building>();
            angle = Random.Range(0, 360f);
            speed = 2 * Mathf.PI / Random.Range(60, 120f);
            float width = GetComponent<Collider>().bounds.size.x;
            float height = GetComponent<Collider>().bounds.size.y;
            float length = GetComponent<Collider>().bounds.size.z;
            planetRadius = Math.Max(width, Math.Max(height, length)) / 2;
            Game.addPlanet(this);
            number = Game.getNumber();
        }
    }
}
