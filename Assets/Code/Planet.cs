using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour, Clickable {
    protected float radiusAroudSun;
    protected float planetRadius = 0;
    protected float speed;
    protected float angle;
    protected int number;
    private int blocking;

    public Planet(DataPlanet data) {
        radiusAroudSun = data.radiusAroudSun;
        planetRadius = data.planetRadius;
        speed = data.speed;
        angle = data.angle;
        number = data.number;
        blocking = data.blocking;
        //gameObject.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        gameObject.transform.position = data.position;
        gameObject.transform.rotation = data.rotation;
    }

    public Planet() {
        
    }


    void Update() {
        /*if (Input.GetKeyDown(KeyCode.P)) {
            SaveSystem.savePlanet(this);
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            load();
        }*/
    }

    protected void load() {
        DataPlanet data = SaveSystem.loadPlanet();
        radiusAroudSun = data.radiusAroudSun;
        planetRadius = data.planetRadius;
        speed = data.speed;
        angle = data.angle;
        number = data.number;
        blocking = data.blocking;
        gameObject.transform.position = data.position;
        gameObject.transform.rotation = data.rotation;
    }

    public void changePlanet(GameObject toWhat) {
        Vector3 coords = transform.position;
        Quaternion then = transform.rotation;
        string copyName = string.Copy(gameObject.name);
        Destroy(gameObject);
        Instantiate(toWhat, coords, then);
        this.name = copyName;
        Destroy(toWhat);
    }

    public float getSpeed() {
        return speed;
    }

    public float getAngle() {
        return angle;
    }

    public GameObject getObj() {
        return gameObject;
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

    public bool isBuilding() {
        return false;
    }

    public void block() {
        blocking++;
    }

    public void unBlock() {
        blocking--;
    }

    public int amountBlocking() {
        return blocking;
    }

    public bool isBlocked() {
        return (blocking != 0);
    }
}
