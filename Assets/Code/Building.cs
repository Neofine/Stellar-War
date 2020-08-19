using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, Clickable {
    private int planetNr = 0;
    private Planet planet;
    private bool added;
    private GameObject disturbing;
    private string exactName = null;
    private void Start() {
        planetNr = 0;
        exactName = OverallUtility.simplify(gameObject.ToString());

        added = false;
        //planet = gameObject.GetComponentInParent<Planet>();
    }
    

    public string getName() {
        return exactName;
    }
    
    public Building(DataBuilding data) {
        planetNr = data.planetNr;
        added = data.added;
        //disturbing = data.disturbing;
        
        gameObject.transform.position = data.position;
        gameObject.transform.rotation = data.rotation;
    }

    public Building() {
        
    }

    private void Update() {
        if (planetNr == 0 || planet == null) {
            planet = gameObject.GetComponentInParent<Planet>();
            planetNr = planet.getNumber();
        }
        if (disturbing == null && added) {
            added = false;
            planet.unBlock();
        }

    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.name[0] == 'B' && !added) {
            added = true;
            if (planet == null)
                planet = gameObject.GetComponentInParent<Planet>();
            planet.block();
            disturbing = other.gameObject;
        }
    }

    public void refreshPlanet() {
        planet = gameObject.GetComponentInParent<Planet>();
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name[0] == 'B' && added) {
            added = false;
            planet.unBlock();
            disturbing = null;
        }
    }

    public Planet getPlanet() {
        return planet;
    }

    public bool isAdded() {
        return added;
    }

    public GameObject distObj() {
        return disturbing;
    }
    
    public bool isShip() {
        return false;
    }

    public bool isPlanet() {
        return false;
    }

    public bool isBuilding() {
        return true;
    }
}
