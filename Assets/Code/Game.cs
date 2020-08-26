using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class Game {

    private static ObjectClick objClick = GameObject.Find("Main Camera").GetComponent<ObjectClick>();
    private static SteadyMove stdMove = GameObject.Find("GameObject").GetComponent<SteadyMove>();
    private static Mesh meshClass = GameObject.Find("GameObject").GetComponent<Mesh>();
    private static Graph graph = GameObject.Find("GameObject").GetComponent<Graph>();
    private static CompressingRoad compRoad = GameObject.Find("GameObject").GetComponent<CompressingRoad>();
    private static CornerCutting cornCut = GameObject.Find("GameObject").GetComponent<CornerCutting>();
    private static CircularMove circMove = GameObject.Find("GameObject").GetComponent<CircularMove>();
    private static MovementOrganiser movOrg = GameObject.Find("GameObject").GetComponent<MovementOrganiser>();
    private static SceneLoader scnLoad = GameObject.Find("GameObject").GetComponent<SceneLoader>();
    private static SwitchToPlanetCamera swtPln = GameObject.Find("GameObject").GetComponent<SwitchToPlanetCamera>();
    private static List<Ship> movableObj = new List<Ship>();
    private static List<Planet> planets = new List<Planet>();
    private static bool inspectMode = false;

    static public SwitchToPlanetCamera getSwitchCamera() {
        return swtPln;
    }

    static public bool getInspectMode() {
        return inspectMode;
    }

    static public void inspectModeOff() {
        inspectMode = false;
    }

    static public void inspectModeOn() {
        inspectMode = true;
    }
    static public SceneLoader getScnLoad() {
        return scnLoad;
    }

    static public MovementOrganiser getMovOrg() {
        return movOrg;
    }

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
        if (meshClass == null)
            meshClass = GameObject.Find("GameObject").GetComponent<Mesh>();
        return meshClass;
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

    static public int getNumber() {
        return planets.Count;
    }

    static public void addShip(Ship ship) {
        movableObj.Add(ship);
    }

    static public void erasePlanets() {
        planets.Clear();
    }
}
