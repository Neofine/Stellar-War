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

    void makeObj() {
        obj = this.gameObject;
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
            //print("RADIUS IS " + planetRadius);
        }
    }

    public void changePlanet(GameObject toWhat) {
        print("HEHE");
        Vector3 coords = obj.transform.position;
        Quaternion then = obj.transform.rotation;
        toWhat.name = obj.name;
        Destroy(obj);
        Instantiate(toWhat, coords, then);
        obj = toWhat;
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
        print("GOT INTO COLLIDER OF :" + ToString() + " OBJECT " + collider.gameObject.ToString());
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

    //protected abstract String ToString();
}
