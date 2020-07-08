using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SteadyMove : MonoBehaviour {

    private List<objDestination> objToMove;
    private Dictionary<Ship, List<Vector3>> moveQueue;

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
        moveQueue = new Dictionary<Ship, List<Vector3>>();
	}

    private float moveSpeed; //tmp for saving speed 
	
	void Update () {
        //print("KLATKEN");
        if (objToMove != null && objToMove.Count != 0) {
            for (int i = 0; i < objToMove.Count; i++) {
                objDestination objNow = objToMove[i];
                //print(objNow.obj.toString() + "moving");
                objNow.iteration++;
                Vector3 position = objNow.obj.transform.position;
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
                if (objNow.obj.transform.position == new Vector3(objNow.coords.x, objNow.coords.y, objNow.coords.z)) {
                    objToMove.Remove(objNow);
                    i--;
                    if (moveQueue.ContainsKey(objNow.obj) && moveQueue[objNow.obj].Count != 0) {
                        objToMove.Add(new objDestination(objNow.obj, moveQueue[objNow.obj].First()));
                        //objNow.obj.getObj().transform.position = new Vector3(adjust(position.x, moveQueue[objNow.obj].First().x, xDiff), adjust(position.y, moveQueue[objNow.obj].First().y, yDiff), adjust(position.z, moveQueue[objNow.obj].First().z, zDiff));
                        moveQueue[objNow.obj].Remove(moveQueue[objNow.obj].First());

                    }
                    continue;
                }
            }
        }
	}

    public void queueMove(List <Vector3> queue, Ship onWhat) {
        if (moveQueue.ContainsKey(onWhat))
            moveQueue.Remove(onWhat);
        Vector3 firstDirection = queue.First();
        queue.Remove(queue.First());
        move(onWhat, firstDirection);
        moveQueue.Add(onWhat, queue);
    }

    private float adjust(float from, float to, float frac) {
        float posChange = moveSpeed;
        while (Useful.abs(to - from) < posChange)
            posChange /= 2;
        if (to > from)
            return from + posChange;// * frac;
        return from - posChange;// * frac;
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
