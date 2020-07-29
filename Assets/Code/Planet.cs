using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour, Clickable {
    protected float radiusAroudSun;
    protected float planetRadius;
    protected float speed;
    protected float angle;
    protected int number;
    private GameObject obj;
    private bool blocking;

    void makeObj() {
        obj = this.gameObject;
        blocking = false;
    }

    void Update() {
        if (obj == null) {
            makeObj();
            Game.addPlanet(this);
            number = Game.getNumber();
            float width = this.GetComponent<Collider>().bounds.size.x;
            float height = this.GetComponent<Collider>().bounds.size.y;
            float length = this.GetComponent<Collider>().bounds.size.z;
            planetRadius = Math.Max(width, Math.Max(height, length)) / 2;
            print(ToString() + " " + planetRadius);
        }
    }

    public void changePlanet(GameObject toWhat) {
        Vector3 coords = obj.transform.position;
        Quaternion then = obj.transform.rotation;
        string copyName = string.Copy(obj.name);
        Destroy(this.gameObject);
        Instantiate(toWhat, coords, then);
        obj = toWhat;
        obj.name = copyName;
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

    private void OnTriggerEnter(Collider collider) {
        Ship ship = collider.gameObject.GetComponent<Ship>();
        if (ship == null)
            return;
        Game.getMovOrg().recalcPath(ship);
    }

    public int getNumber() {
        return number;
    }

    public bool isShip() {
        return false;
    }

    public bool isPlanet() {
        return true;
    }

    public void block() {

    }

    public bool isBlocked() {
        return blocking;
    }
}
