using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOrganiser : MonoBehaviour {
    private int roadSmoothness = 6;
    private float lastTime = 0.0f;
    void Update() {
        if (Input.GetMouseButtonDown(1) || (Input.GetMouseButton(1) && Time.time - lastTime > 0.2f)) {
            lastTime = Time.time;
            Vector3 mousePos = Input.mousePosition;
            Vector3 gamePos = ClickCoords.getCords();
            List<Ship> objToMove = Game.getObjClick().getObjHighlighted();

            if (objToMove != null && objToMove.Count != 0) {
                foreach (Ship obj in objToMove) {
                    Vector3 position = obj.getObj().transform.position;

                    if (Mathf.Abs(position.x - ClickCoords.getX(position, gamePos)) <= 5 && Mathf.Abs(position.y - Game.getMesh().getHeight()) <= 5 &&
                        Mathf.Abs(ClickCoords.getZ(position, gamePos) - position.z) <= 5)
                        continue;

                    Vector3 end = new Vector3(ClickCoords.getX(position, gamePos), Game.getMesh().getHeight(), ClickCoords.getZ(position, gamePos));
                    calcRoute(obj, end);
                }
            }
        }
    }

    private void calcRoute(Ship ship, Vector3 destination) {
        List<Vector3> route;
        float timer = Time.time;
        route = Game.getGraph().planRoute(ship.getObj().transform.position, destination, 50, ship);
        print("ROAD CALCULATING TIME: " + (Time.time - timer));

        if (route != null && route.Count != 0) {
            route.Reverse();
            
            timer = Time.time;
            route = Game.getCompressingRoad().compress(ship, route);
            print("COMPRESSING TIME: " + (Time.time - timer));
            
            timer = Time.time;
            route = Game.getCornerCutting().smoothPath(route, roadSmoothness, ship);
            print("SMOOTHING TIME: " + (Time.time - timer));
            
            if (route.Count != 0) {
                Game.getStdMove().queueMove(route, ship);
            }
        }
    }

    public void recalcPath(Ship ship) {
        Vector3 destination = Game.getStdMove().getDest(ship);
        if (destination != Vector3.zero) {
            calcRoute(ship, destination);
        }
    }
}
