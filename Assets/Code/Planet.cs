﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    protected float radiusAroudSun;
    protected float planetRadius;
    protected float speed;
    protected float angle;
    private GameObject obj;

    void Start() {
        radiusAroudSun = 150f;
        
        angle = 0f;
        speed = 2 * Mathf.PI / 60;  
    }

    void makeObj() {
        obj = this.gameObject;
    }

    void Update() {
        if (obj == null) {
            makeObj();
            Game.addPlanet(this);
            
            float width = this.GetComponent<Collider>().bounds.size.x;
            float height = this.GetComponent<Collider>().bounds.size.y;
            float length = this.GetComponent<Collider>().bounds.size.z;
            planetRadius = Math.Max(width, Math.Max(height, length));
            print("RADIUS IS " + planetRadius);
        }
    }

    public float getSpeed() {
        return speed;
    }

    public float getAngle() {
        return angle;
    }

    public GameObject getObj() {
        return obj;
    }

    public void addToAngle(float amount) {
        angle += amount;
        angle %= 360;
    }

    public float getRadSun() {
        return radiusAroudSun;
    }

    public float getRadPln() {
        return planetRadius;
    }
}
