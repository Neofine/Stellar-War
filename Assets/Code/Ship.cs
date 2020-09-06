using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour, Clickable {

    private GameObject obj;
    protected float speed;
    protected int faction = 1;
    public float attackRange = 50f;
    protected bool isMoving = false;
    protected bool duringAttack = false;
    protected Vector3 lastPosition;
    protected float reservedRadius = 10;

    void makeObj() {
        obj = this.gameObject;
    }

    public float getAttackRange() {
        return attackRange;
    }

    void Update() {
        //print(isMoving);
        //print(this.ToString() + " " + isMoving);
        if (obj == null) {
            makeObj();
            Game.addShip(this);
        }
        //print(transform.position + " " + lastPosition);
        if (transform.position != lastPosition) {
            isMoving = true;
        }
        else {
            isMoving = false;
        }
        lastPosition = transform.position;
    }

    public float getRadius() {
        return reservedRadius;
    }

    public bool inMovement() {
        return isMoving;
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
