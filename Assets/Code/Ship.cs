using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour, Clickable {

    private GameObject obj;
    protected float speed;
    protected int faction = 1;
    protected float attackRange = 10f;
    protected bool duringAttack = false;

    void makeObj() {
        obj = this.gameObject;
    }

    public float getAttackRange() {
        return attackRange;
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

    public bool isAttacking() {
        return duringAttack;
    }

    public void notAttacking() {
        duringAttack = false;
    }

    public void startAttack() {
        duringAttack = true;
    }

    public abstract string toString();

    public bool isShip() {
        return true;
    }

    public bool isPlanet() {
        return false;
    }

    public bool isBuilding() {
        return false;
    }
}
