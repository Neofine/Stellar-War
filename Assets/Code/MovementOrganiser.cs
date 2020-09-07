using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOrganiser : MonoBehaviour {
    private int roadSmoothness = 3;
    private float lastTime = 0.0f;
    //private ObjectClick objCli;

    private void Start() {
        //objCli = Game.getObjClick();
    }

    void Update() {
        if (Game.getInspectMode() && Time.time - Game.getObjClickClose().getTimeBuildClick() < 0.1f) {
            print("1");
            Vector3 localCoords = Game.getObjClickClose().getBuildingClicked().transform.localPosition;
            Transform[] children = Game.getObjClickClose().getPlanetOfBuilding().GetComponentsInChildren<Transform>();
            List<Ship> objToMove = Game.getObjClick().getObjHighlighted();
            foreach (Transform child in children) {
                print("2");
                if (child.localPosition == localCoords) {
                    print("3");
                    foreach (Ship ship in objToMove) {
                        print(child.position);
                        ship.startAttack();
                        if (ship.gameObject.TryGetComponent(out FollowingMovingObject foll)) {
                            foll.setNewObject(child.gameObject);
                        }
                        else {
                            ship.gameObject.AddComponent<FollowingMovingObject>().setNewObject(child.gameObject);
                        }
                    }
                }
            }
        }

        if (!Game.getInspectMode() && (Input.GetMouseButtonDown(1) || (Input.GetMouseButton(1) && Time.time - lastTime > 0.2f))) {
            lastTime = Time.time;

            Vector3 gamePos = ClickCoords.getCords();
            List<Ship> objToMove = Game.getObjClick().getObjHighlighted();

            if (objToMove != null && objToMove.Count != 0) {
                foreach (Ship obj in objToMove) {
                    Vector3 position = obj.getObj().transform.position;

                    if (Mathf.Abs(position.x - ClickCoords.getX(position, gamePos)) <= 5 && Mathf.Abs(position.y - Game.getMesh().getHeight()) <= 5 &&
                        Mathf.Abs(ClickCoords.getZ(position, gamePos) - position.z) <= 5)
                        continue;

                    Vector3 end = new Vector3(ClickCoords.getX(position, gamePos), Game.getMesh().getHeight(), ClickCoords.getZ(position, gamePos));

                    if (obj.gameObject.TryGetComponent(out FollowingMovingObject foll)) {
                        foll.setNewObject(null);
                    }
                    obj.changeDest(end);
                    calcRoute(obj, end, 100);
                }
            }
        }
    }

    public void calcRoute(Ship ship, Vector3 destination, float routePrecision, float planetDist = 70) {
        List<Vector3> route;
        float timer = Time.time;
        route = Game.getGraph().planRoute(ship.getObj().transform.position, destination, routePrecision, ship, planetDist);

        if (route != null && route.Count != 0) {
            route.Reverse();
            route = Game.getCompressingRoad().compress(ship, route);
            route = Game.getCornerCutting().smoothPath(route, roadSmoothness, ship);

            if (route.Count != 0) {
                Game.getStdMove().queueMove(route, ship);
            }
        }
    }

    public void recalcPath(Ship ship) {
        Vector3 destination = Game.getStdMove().getDest(ship);
        if (destination != Vector3.zero) {
            if (ship.isAttacking())
                calcRoute(ship, destination, ship.getAttackRange());
            else
                calcRoute(ship, destination, 100
                    );
        }
    }
}
