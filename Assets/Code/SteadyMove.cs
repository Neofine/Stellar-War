using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SteadyMove : MonoBehaviour {

    private List<objDestination> objToMove;
    private Dictionary<Ship, List<Vector3>> moveQueue;
    public float tpLength = 1f;
    public float almostZero = 0.01f;

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
        InvokeRepeating("moveABit", 0f, 0.001f);
    }

    float debugTimer;
	
	void moveABit() {
        if (objToMove != null && objToMove.Count != 0) {
            for (int i = 0; i < objToMove.Count; i++) {
                objDestination objNow = objToMove[i];
                Vector3 position = objNow.obj.transform.position;

                if (++objNow.iteration == 1) {
                    objNow.obj.getObj().transform.LookAt(new Vector3(objNow.coords.x, objNow.coords.y, objNow.coords.z));
                    objNow.obj.getObj().transform.Rotate(new Vector3(-90f, 0f, 0f), Space.Self);
                    if (objNow.obj.toString() == "spy") {
                        objNow.obj.getObj().transform.Rotate(new Vector3(0f, 0f, 180f), Space.Self);
                    }
                }

                // making a move by a fixed length, so going straight forward and
                // turning will have the same fixed speed
                Vector3 diff = objNow.coords - position;
                if (Game.getGraph().vecLength(objNow.coords, position) + almostZero >= tpLength) {
                    diff /= Game.getGraph().vecLength(objNow.coords, position);
                    diff *= tpLength;
                    objNow.obj.getObj().transform.position += diff;
                }
                else if (moveQueue.ContainsKey(objNow.obj) && moveQueue[objNow.obj].Count != 0) {
                    float rest = tpLength - Game.getGraph().vecLength(objNow.coords, position);
                    objNow.obj.getObj().transform.position = objNow.coords;
                    while (rest > almostZero) {
                        if (moveQueue.ContainsKey(objNow.obj) && moveQueue[objNow.obj].Count != 0) {
                            objToMove.RemoveAt(i);
                            objNow = getNextDest(objNow);
                            objToMove.Insert(i, objNow);
                        } 
                        else {
                            objNow.obj.getObj().transform.position = objNow.coords;
                            break;
                        }
                        diff = objNow.coords - position;
                        if (Game.getGraph().vecLength(objNow.coords, position) + almostZero >= rest) {
                            diff /= Game.getGraph().vecLength(objNow.coords, position);
                            diff *= rest;
                            objNow.obj.getObj().transform.position += diff;
                            break;
                        }
                        else {
                            objNow.obj.getObj().transform.position = objNow.coords;
                            rest -= Game.getGraph().vecLength(objNow.coords, position);
                        }
                    }
                }
                else objNow.obj.getObj().transform.position = objNow.coords;

                if (objNow.obj.transform.position == objNow.coords) {
                    objToMove.Remove(objNow);
                    i--;
                    if (moveQueue.ContainsKey(objNow.obj) && moveQueue[objNow.obj].Count != 0) {
                        objToMove.Add(getNextDest(objNow));
                    }
                    else print("AUTO PILOT END IN: " + (Time.time - debugTimer));
                }
            }
        }
	}

    private objDestination getNextDest(objDestination examined) {
        objDestination ans = new objDestination(examined.obj, moveQueue[examined.obj].First());
        moveQueue[examined.obj].Remove(moveQueue[examined.obj].First());
        return ans;
    }

    public void queueMove(List <Vector3> queue, Ship onWhat) {
        debugTimer = Time.time;
        if (moveQueue.ContainsKey(onWhat))
            moveQueue.Remove(onWhat);
        Vector3 firstDirection = queue.First();
        queue.Remove(queue.First());
        move(onWhat, firstDirection);
        moveQueue.Add(onWhat, queue);
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
