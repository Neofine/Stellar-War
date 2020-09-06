using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SteadyMove : MonoBehaviour {

    private List<ObjDestination> objToMove;
    private Dictionary<Ship, List<Vector3>> moveQueue;
    private List<Tuple<Ship, GameObject>> following;
    public float tpLength;
    public float almostZero = 0.01f;

    private class ObjDestination {
        public Vector3 coords;
        public Ship obj;
        public int iteration;
        public ObjDestination(Ship obj, Vector3 coords) {
            this.obj = obj;
            this.coords = coords;
        }
    }

    void Start () {
        objToMove = new List<ObjDestination>();
        moveQueue = new Dictionary<Ship, List<Vector3>>();
        tpLength = 3f;
    }

    float debugTimer;
	void Update() {
        float timer = Time.time;
        if (objToMove != null && objToMove.Count != 0) {
            for (int i = 0; i < objToMove.Count; i++) {
                ObjDestination objNow = objToMove[i];
                tpLength = objNow.obj.getSpeed() * Time.deltaTime * 60;
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
                if (VectorUtility.vecLength(objNow.coords, position) + almostZero >= tpLength) {
                    diff /= VectorUtility.vecLength(objNow.coords, position);
                    diff *= tpLength;
                    objNow.obj.getObj().transform.position += diff;
                }
                else if (moveQueue.ContainsKey(objNow.obj) && moveQueue[objNow.obj].Count != 0) {
                    float rest = tpLength - VectorUtility.vecLength(objNow.coords, position);
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
                        objNow.obj.getObj().transform.LookAt(new Vector3(objNow.coords.x, objNow.coords.y, objNow.coords.z));
                        objNow.obj.getObj().transform.Rotate(new Vector3(-90f, 0f, 0f), Space.Self);
                        if (objNow.obj.toString() == "spy") {
                            objNow.obj.getObj().transform.Rotate(new Vector3(0f, 0f, 180f), Space.Self);
                        }
                        diff = objNow.coords - position;
                        if (VectorUtility.vecLength(objNow.coords, position) + almostZero >= rest) {
                            diff /= VectorUtility.vecLength(objNow.coords, position);
                            diff *= rest;
                            objNow.obj.getObj().transform.position += diff;
                            break;
                        }
                        else {
                            objNow.obj.getObj().transform.position = objNow.coords;
                            rest -= VectorUtility.vecLength(objNow.coords, position);
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
                    else {
                        // This is very precious source of information, don't delete!
                        print("AUTO PILOT END IN: " + (Time.time - debugTimer));
                        objNow.obj.getObj().GetComponent<MeshCollider>().enabled = true;
                    }
                }
            }
        }

        if (following != null) {
            foreach (Tuple<Ship, GameObject> follower in following) {
                Ship attacker = follower.Item1;
                GameObject dest = follower.Item2;
                if (VectorUtility.vecLength(attacker.getObj().transform.position, dest.transform.position) <
                    attacker.getAttackRange()) {
                    
                }
            }
        }
    }

    public Quaternion giveRotation(Vector3 position, Vector3 destination, Ship obj) {
        Quaternion answer = Quaternion.LookRotation(destination - position);
        answer.x -= 90f;
        if (obj.toString() == "spy") {
            answer.z += 180f;
        }
        return answer;
    }
    private ObjDestination getNextDest(ObjDestination examined) {
        ObjDestination ans = new ObjDestination(examined.obj, moveQueue[examined.obj].First());
        moveQueue[examined.obj].Remove(moveQueue[examined.obj].First());
        return ans;
    }

    public bool isShipMoving(Ship what) {
        for (int i = 0; i < objToMove.Count; i++) {
            ObjDestination objNow = objToMove[i];
            if (objNow.obj == what)
                return true;
        }
        return false;
    }

    public void queueMove(List <Vector3> queue, Ship onWhat) {
        //print("QUEUING MOVE");
        float timer = Time.time;
        onWhat.getObj().GetComponent<MeshCollider>().enabled = false;
        debugTimer = Time.time;
        if (moveQueue.ContainsKey(onWhat))
            moveQueue.Remove(onWhat);
        Vector3 firstDirection = queue.First();
        queue.Remove(queue.First());
        move(onWhat, firstDirection);
        moveQueue.Add(onWhat, queue);
        //print("QUEUING TIME: " + (Time.time - timer));
    }

    public void follow(GameObject what, Ship with) {
        
    }

    public Vector3 getDest(Ship ship) {
        if (moveQueue.ContainsKey(ship) && moveQueue[ship].Count != 0)
            return moveQueue[ship][moveQueue[ship].Count - 1];
        for (int i = 0; i < objToMove.Count; i++) {
            ObjDestination objNow = objToMove[i];
            if (objNow.obj == ship)
                return objNow.coords;
        }
        return Vector3.zero;
    }

    public void move(Ship obj, Vector3 coords) {
        for (int i = 0; i < objToMove.Count; i++) {
            if (objToMove[i].obj == obj) {
                objToMove.Remove(objToMove[i]);
                i--;
                continue;
            }
        }
        objToMove.Add(new ObjDestination(obj, coords));
    }
}
