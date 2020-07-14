using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour {
    private int roadSmoothness = 6;
    void Update() {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1)) {
            Vector3 mousePos = Input.mousePosition;
            Vector3 gamePos = ClickCoords.getCords();
            List<Ship> objToMove = Game.getObjClick().getObjHighlighted();

            if (objToMove != null && objToMove.Count != 0) {
                foreach (Ship obj in objToMove) {
                    Vector3 position = obj.getObj().transform.position;

                    if (Useful.abs(position.x - ClickCoords.getX(position, gamePos)) <= 5 && Useful.abs(position.y - Game.getMesh().getHeight()) <= 5 &&
                        Useful.abs(ClickCoords.getZ(position, gamePos) - position.z) <= 5)
                        continue;

                    Vector3 end = new Vector3(ClickCoords.getX(position, gamePos), Game.getMesh().getHeight(), ClickCoords.getZ(position, gamePos));
                    List<Vector3> route = new List<Vector3>();
                    float timer = Time.time;
                    route = Game.getGraph().planRoute(position, end, 1000, obj);

                    if (route != null && route.Count != 0) { 
                        route.Reverse();
                        route = Game.getCompressingRoad().compress(obj, route);
                        route = Game.getCornerCutting().smoothPath(route, roadSmoothness, obj);
                        if (route.Count != 0) {
                            Game.getStdMove().queueMove(route, obj);
                        }
                        else print("XDDDD");
                    }
                    else
                      print("NOT FOUND!");
                }
            }
        }
    }
}
