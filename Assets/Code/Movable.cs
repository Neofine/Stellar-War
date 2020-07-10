using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour {
    private int roadSmoothness = 6;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKey(KeyCode.Backspace)) {
            print("READ");
            Vector3 mousePos = Input.mousePosition;
            Vector3 gamePos = ClickCoords.getCords();
            List<Ship> objToMove = Game.getObjClick().getObjHighlighted();

            if (objToMove != null && objToMove.Count != 0) {
                foreach (Ship obj in objToMove) {
                    print("MOVABLE");
                    Vector3 position = obj.getObj().transform.position;

                    if (Useful.abs(position.x - ClickCoords.getX(position, gamePos)) <= 1 && Useful.abs(position.y - Game.getMesh().getHeight()) <= 1 &&
                        Useful.abs(ClickCoords.getZ(position, gamePos) - position.z) <= 1)
                        continue;

                    Vector3 end = new Vector3(ClickCoords.getX(position, gamePos), Game.getMesh().getHeight(), ClickCoords.getZ(position, gamePos));
                    List<Vector3> route = new List<Vector3>();
                    print("MAKING ROUTE");
                    float timer = Time.time;
                    route = Game.getGraph().planRoute(position, end, 20, obj);
                    print("END MAKING ROUTE " + (Time.time - timer));
                    if (route == null)
                        print("NOT FOUND!");
                    else {
                        route.Reverse();
                        route = Game.getCompressingRoad().compress(obj, route);
                        route = Game.getCornerCutting().smoothPath(route, roadSmoothness, obj);
                        Game.getStdMove().queueMove(route, obj);
                    }
                }
            }
        }
    }

}
