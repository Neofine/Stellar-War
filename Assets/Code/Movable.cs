using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour{
    void Update() {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1)) {
            Vector3 mousePos = Input.mousePosition;
            Vector3 gamePos = ClickCoords.getCords();
            List<Ship> objToMove = Game.getObjClick().getObjHighlighted();
            
            if (objToMove != null && objToMove.Count != 0) {
                foreach (Ship obj in objToMove) {
                    Vector3 position = obj.getObj().transform.position;
                    //print("I WANT TO " + ClickCoords.getX(position, gamePos) + " " + position.y + " " + ClickCoords.getZ(position, gamePos));
                    if (Useful.abs(position.x - ClickCoords.getX(position, gamePos)) <= 1 && Useful.abs(position.y - Game.getMesh().getHeight()) <= 1 &&
                        Useful.abs(ClickCoords.getZ(position, gamePos) - position.z) <= 1)
                        continue;
                    Game.getStdMove().move(obj, new Vector3(ClickCoords.getX(position, gamePos), Game.getMesh().getHeight() , ClickCoords.getZ(position, gamePos)));
                }
            }
        }
    }

}
