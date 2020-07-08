using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CompressingRoad : MonoBehaviour{
    public List<Vector3> compress(Ship ship, List<Vector3> road) {
        //return road;
        List<Vector3> answer = new List<Vector3>();
        Vector3 now = ship.gameObject.transform.position;
        int idx = 1;
        //print("START SHORTCUT");
        while (now != road[road.Count - 1]) {
            while (idx <= road.Count - 1 && noObjectOnWay(now, road[idx], ship)) {
                idx++;
            }
            now = road[idx - 1];
            answer.Add(road[idx - 1]);
            //print(road[idx - 1]);
            idx++;
        }
        //print("END SHORTCUT");
        return answer;
    }

    private bool noObjectOnWay(Vector3 start, Vector3 end, Ship ship) {
        float width = ship.GetComponent<Renderer>().bounds.size.x;
        float height = ship.GetComponent<Renderer>().bounds.size.y;
        float length = ship.GetComponent<Renderer>().bounds.size.z;
        float neverTouch = Math.Max(width, Math.Max(height, length));

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                for (int z = -1; z <= 1; z++) {
                    RaycastHit hit;
                    //Vector3 newVec = new Vector3(x * width / 2, y * height / 2, z * length / 2);
                    Vector3 newVec = new Vector3(x * neverTouch / 2, y * neverTouch / 2, z * neverTouch / 2);
                    if (Physics.Linecast(start + newVec, end + newVec, out hit) && hit.collider.gameObject != ship.getObj()) {
                        print(hit.collider.gameObject.name);
                        return false;
                    }
                        
                }
            }
        }
        return true;
        
            //        return !Physics.Linecast(start, end) && !Physics.Linecast(start - new Vector3(width / 2, width / 2, length/2), end - new Vector3(width / 2, width / 2, length / 2)) &&
            //!Physics.Linecast(start + new Vector3(width / 2, width / 2, length / 2), end + new Vector3(width / 2, width / 2, length / 2)) &&
            //!Physics.Linecast(start - new Vector3(width / 2, -width / 2, length / 2), end - new Vector3(width / 2, -width / 2, length / 2)) &&
            //!Physics.Linecast(start - new Vector3(-width / 2, width / 2, length / 2), end - new Vector3(-width / 2, width / 2, length / 2));
    }
}
