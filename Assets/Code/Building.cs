using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    private Planet planet;
    private void Start() {
        planet = transform.parent.GetComponent<Planet>();
    }
    private void OnTriggerStay(Collider other) {
        planet.block();
    }
}
