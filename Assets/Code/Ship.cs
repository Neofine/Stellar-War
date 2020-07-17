using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour {

    private GameObject obj;
    protected float speed;
    protected int faction = 1;

    void makeObj() {
        obj = this.gameObject;
    }
	
	void Update () {
        if (obj == null) {
            makeObj();
            Game.addShip(this);
        }
            
	}

    public GameObject getObj() {
        return obj;
    }

    public float getSpeed() {
        return speed;
    }

    public abstract string toString();
    
}
