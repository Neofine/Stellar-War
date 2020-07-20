using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CompressingRoad : MonoBehaviour{
    public List<Vector3> compress(Ship ship, List<Vector3> road) {
        List<Vector3> answer = new List<Vector3>();
        Vector3 now = ship.gameObject.transform.position;
        int idx = 1;
        while (now != road[road.Count - 1]) {
            while (idx <= road.Count - 1 && noObjectOnWay(now, road[idx], ship)) {
                idx++;
            }
            now = road[idx - 1];
            answer.Add(road[idx - 1]);
            idx++;
        }
        return answer;
    }

    /*public bool noObjectOnWay(Vector3 start, Vector3 end, Ship ship) {
        float width = ship.GetComponent<Renderer>().bounds.size.x;
        float height = ship.GetComponent<Renderer>().bounds.size.y;
        float length = ship.GetComponent<Renderer>().bounds.size.z;
        float neverTouch = Math.Max(width, Math.Max(height, length));

        Vector3 offset = transform.up * (transform.localScale.y / 2f) * -1f;
        Vector3 pos = transform.position + offset; //This is the position
        print(transform.position + " " + pos);

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                for (int z = -1; z <= 1; z++) {
                    if (x != 0 && y != 0 && z != 0)
                        continue;
                    RaycastHit hit;
                    Vector3 newVec = new Vector3(x * neverTouch / 2, y * neverTouch / 2, z * neverTouch / 2);
                    if (Physics.Linecast(start + newVec, end + newVec, out hit) && hit.collider.gameObject != ship.getObj()) {
                        return false;
                    }
                        
                }
            }
        }
        return true;
    }*/

    public bool noObjectOnWay(Vector3 start, Vector3 end, Ship ship) {
        Renderer rend = ship.GetComponent<Renderer>();
        Quaternion saved = ship.getObj().transform.rotation;
        //ship.getObj().transform.rotation = Game.getStdMove().giveRotation(start, end, ship);

        Vector3 max = rend.bounds.max;
        Vector3 min = rend.bounds.min;
        Vector3 pos = ship.getObj().transform.position;

        RaycastHit hit;
        if (Physics.Linecast(start, end, out hit) && hit.collider.gameObject != ship.getObj()) {
            return false;
        }
        if (Physics.Linecast(start + (max - pos), end + (max - pos), out hit) && hit.collider.gameObject != ship.getObj()) {
            return false;
        }
        if (Physics.Linecast(start + (min - pos), end + (min - pos), out hit) && hit.collider.gameObject != ship.getObj()) {
            return false;
        }


        /*for (int x = -1; x <= 1; x++) {
            Vector3 newVec = new Vector3();

            if (x == -1) {
                newVec.x = min.x;
            }
            else if (x == 0) {
                newVec.x = pos.x;
            }
            else {
                newVec.x = max.x;
            }

            for (int y = -1; y <= 1; y++) {

                if (y == -1) {
                    newVec.y = min.y;
                }
                else if (x == 0) {
                    newVec.y = pos.y;
                }
                else {
                    newVec.y = max.y;
                }

                for (int z = -1; z <= 1; z++) {

                    if (z == -1) {
                        newVec.z = min.z;
                    }
                    else if (x == 0) {
                        newVec.z = pos.z;
                    }
                    else {
                        newVec.z = max.z;
                    }

                    newVec -= pos;
                    RaycastHit hit;
                    
                    if (Physics.Linecast(start + newVec, end + newVec, out hit) && hit.collider.gameObject != ship.getObj()) {
                        return false;
                    }

                }
            }
        }*/
        return true;
    }
}
