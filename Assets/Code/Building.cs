using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, Clickable {
    private Planet planet;
    private bool added;
    private GameObject disturbing;
    private void Start() {
        added = false;
        planet = gameObject.GetComponentInParent<Planet>();
    }

    private void Update() {
        if (planet == null)
            planet = gameObject.GetComponentInParent<Planet>();
        if (disturbing == null && added) {
            added = false;
            planet.unBlock();
        }

    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.name[0] == 'B' && !added) {
            print("XD");
            added = true;
            planet.block();
            disturbing = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name[0] == 'B' && added) {
            added = false;
            planet.unBlock();
            disturbing = null;
        }
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
