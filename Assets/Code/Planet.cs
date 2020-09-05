using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour, Clickable {
    public float radiusAroudSun;
    protected float planetRadius = 0;
    protected float speed = 0;
    protected float angle;
    protected int number;
    private int blocking;
    protected int cratersAdded = 0;
    protected List<Building> buildings;

    public List<Building> getBuildings() {
        return buildings;
    }

    public Planet(DataPlanet data) {
        load(data, true, Vector3.zero);
    }

    public void addBuilding(Building building) {
        buildings.Add(building);
    }

    public Planet() {
        
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            SaveSystem.savePlanet(this);    
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            Game.erasePlanets();
            load(null, true, Vector3.zero);
        }
    }

    public void load(DataPlanet data, bool withLocation, Vector3 center) {
        if (data == null)
            data = SaveSystem.loadPlanet(number);
        foreach (Transform child in transform) {
            string childName = OverallUtility.simplify(child.gameObject.ToString());
            if (childName != "planet" && childName != "particles")
                Destroy(child.gameObject);
        }
        radiusAroudSun = data.radiusAroudSun;
        planetRadius = data.planetRadius;
        speed = data.speed;
        angle = data.angle;
        number = data.number;
        cratersAdded = data.cratersAdded;
        blocking = data.blocking;
        buildings = new List<Building>();
        buildings.Clear();
        if (withLocation == true) {
            gameObject.transform.position = data.position;
            gameObject.transform.rotation = data.rotation;
        }
        Rigidbody planetRgb = gameObject.GetComponent<Rigidbody>();
        planetRgb.constraints = RigidbodyConstraints.FreezeAll;
        if (data.buildings != null) {
            foreach (DataBuilding datBil in data.buildings) {
                GameObject building = Instantiate(GameObject.Find(datBil.exactName), datBil.position + center, datBil.rotation);
                building.transform.parent = gameObject.transform;
                Rigidbody rgb = building.GetComponent<Rigidbody>();
                rgb.constraints = RigidbodyConstraints.FreezeAll;
                print(building.GetComponent<Building>());
                buildings.Add(building.GetComponent<Building>());
            }
        }

        planetRgb.constraints = RigidbodyConstraints.None;
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

    public int CratersAdded {
        get => cratersAdded;
        set => cratersAdded = value;
    }
}
