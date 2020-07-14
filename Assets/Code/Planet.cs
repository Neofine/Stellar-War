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
        radiusAroudSun = 300f;
        planetRadius = 300f;
        angle = 0f;
        speed = 2 * Mathf.PI / 10;  
    }

    void makeObj() {
        obj = this.gameObject;
    }

    void Update() {
        if (obj == null) {
            makeObj();
            Game.addPlanet(this);
        }
    }

    public float getSpeed() {
        return speed;
    }

    public float getAngle() {
        return angle;
    }

    public float getRadius() {
        return planetRadius;
    }

    public GameObject getObj() {
        return obj;
    }

    public void addToAngle(float amount) {
        angle += amount;
    }

    public float getRadSun() {
        return radiusAroudSun;
    }

    public float getRadPln() {
        return planetRadius;
    }
}
