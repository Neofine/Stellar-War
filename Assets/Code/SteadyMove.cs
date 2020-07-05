using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyMove : MonoBehaviour {

   private List<objDestination> objToMove;

    private class objDestination {
        public Vector3 coords;
        public Ship obj;
        public int iteration;
        public objDestination(Ship obj, Vector3 coords) {
            this.obj = obj;
            this.coords = coords;
        }
    }

    void Start () {
        objToMove = new List<objDestination>();
	}

    private float moveSpeed; //tmp for saving speed 
	
	void Update () {
        if (objToMove != null && objToMove.Count != 0) {
            for (int i = 0; i < objToMove.Count; i++) {
                objDestination objNow = objToMove[i];
                objNow.iteration++;
                Vector3 position = objNow.obj.transform.position;
                if (position == new Vector3(objNow.coords.x, objNow.coords.y, objNow.coords.z)) {
                    //print("DONE! " + position);
                    objToMove.Remove(objNow);
                    i--;
                    continue;
                }
                moveSpeed = objNow.obj.getSpeed();
                float xDiff, yDiff, zDiff, max;
                xDiff = Useful.abs(position.x - objNow.coords.x);
                yDiff = Useful.abs(position.y - objNow.coords.y);
                zDiff = Useful.abs(position.z - objNow.coords.z);
                max = Useful.max(xDiff, Useful.max(yDiff, zDiff));
                xDiff /= max;
                yDiff /= max;
                zDiff /= max;
                if (objNow.iteration == 1) {
                    objNow.obj.getObj().transform.LookAt(new Vector3(objNow.coords.x, objNow.coords.y, objNow.coords.z));
                    objNow.obj.getObj().transform.Rotate(new Vector3(-90f, 0f, 0f), Space.Self);
                    if (objNow.obj.toString() == "spy") {
                        objNow.obj.getObj().transform.Rotate(new Vector3(0f, 0f, 180f), Space.Self);
                    }
                }
                objNow.obj.getObj().transform.position = new Vector3(adjust(position.x, objNow.coords.x, xDiff), adjust(position.y, objNow.coords.y, yDiff), adjust(position.z, objNow.coords.z, zDiff));
            }
        }
	}

    private float adjust(float from, float to, float frac) {
        float posChange = moveSpeed;
        while (Useful.abs(to - from) < posChange)
            posChange /= 2;
        if (to > from)
            return from + posChange * frac;
        return from - posChange * frac;
    }

    public void move(Ship obj, Vector3 coords) {
        for (int i = 0; i < objToMove.Count; i++) {
            if (objToMove[i].obj == obj) {
                objToMove.Remove(objToMove[i]);
                i--;
                continue;
            }
        }
        objToMove.Add(new objDestination(obj, coords));
    }
}
