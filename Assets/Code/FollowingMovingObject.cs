﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I assume this is script is added only to ships
public class FollowingMovingObject : MonoBehaviour {
    private GameObject follWhat;
    private Ship ship;
    private Planet planet;
    private MeshCollider coll;

    private void Start() {
        ship = GetComponent<Ship>();
        coll = GetComponent<MeshCollider>();
        planet = null;
    }

    private void Update() {
        if (follWhat == null) {
            coll.enabled = true;
            return;
        }
        
        coll.enabled = false;
        if (VectorUtility.vecLength(transform.position, follWhat.transform.position) > ship.getAttackRange()) {
            Game.getMovOrg().calcRoute(ship, follWhat.transform.position, 10);
        }
        else if (planet != null && VectorUtility.vecLength(transform.position, planet.gameObject.transform.position) - 10 < planet.getRadPln()) {
            Game.getMovOrg().calcRoute(ship, follWhat.transform.position, 10);
        }
        else {
            transform.LookAt(follWhat.transform.position);
            transform.Rotate(new Vector3(270f, 0f, 0f), Space.Self);
        }
    }

    public void setNewObject(GameObject what) {
        if (what == null) {
            follWhat = null;
            planet = null;
            return;
        }
        follWhat = what;
        if (what.TryGetComponent(out Building building)) {
            planet = building.transform.parent.GetComponent<Planet>();
        }
    }
}
