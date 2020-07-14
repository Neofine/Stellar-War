using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game {
    //public static Game game;
    private static ObjectClick objClick = GameObject.Find("Main Camera").GetComponent<ObjectClick>();
    private static SteadyMove stdMove = GameObject.Find("GameObject").GetComponent<SteadyMove>();
    private static Mesh mesh = GameObject.Find("GameObject").GetComponent<Mesh>();
    private static Graph graph = GameObject.Find("GameObject").GetComponent<Graph>();
    private static CompressingRoad compRoad = GameObject.Find("GameObject").GetComponent<CompressingRoad>();
    private static CornerCutting cornCut = GameObject.Find("GameObject").GetComponent<CornerCutting>();
    private static CircularMove circMove = GameObject.Find("GameObject").GetComponent<CircularMove>();
    private static List<Ship> movableObj = new List<Ship>();
    private static List<Planet> planets = new List<Planet>();



    static public CircularMove getCircularMove() {
        return circMove;
    }

    static public CornerCutting getCornerCutting() {
        return cornCut;
    }

    static public CompressingRoad getCompressingRoad() {
        return compRoad;
    }

    static public Graph getGraph() {
        return graph;
    }

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

    static public List<Planet> getPlanets() {
        return planets;
    }

    static public void addPlanet(Planet planet) {
        planets.Add(planet);
    }

    static public void addShip(Ship ship) {
        movableObj.Add(ship);
    }
}
