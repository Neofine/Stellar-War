using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game {
    //public static Game game;
    private static ObjectClick objClick = GameObject.Find("Main Camera").GetComponent<ObjectClick>();
    private static SteadyMove stdMove = GameObject.Find("GameObject").GetComponent<SteadyMove>();
    private static Mesh mesh = GameObject.Find("GameObject").GetComponent<Mesh>();
    private static List<Ship> movableObj = new List<Ship>();

    static public ObjectClick getObjClick() {
        return objClick;
    }

    static public Mesh getMesh() {
        return mesh;
    }

    static public SteadyMove getStdMove() {
        return stdMove;
    }

    static public List<Ship> getMovableObj() {
        return movableObj;
    }

    static public void addShip(Ship ship) {
        movableObj.Add(ship);
    }
}
