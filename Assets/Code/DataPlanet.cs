using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DataPlanet {
    public float radiusAroudSun;
    public float planetRadius = 0;
    public float speed;
    public float angle;
    public int number;
    public int blocking;
    public int cratersAdded;
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public DataBuilding[] buildings;

    public DataPlanet(Planet planet) {
        radiusAroudSun = planet.getRadSun();
        planetRadius = planet.getRadPln();
        speed = planet.getSpeed();
        angle = planet.getAngle();
        number = planet.getNumber();
        blocking = planet.amountBlocking();
        cratersAdded = planet.CratersAdded;

        if (planet.getBuildings() != null) {
            buildings = new DataBuilding[planet.getBuildings().Count];
            int it = 0;
            foreach (Building building in planet.getBuildings()) {
                buildings[it++] = new DataBuilding(building);
            }
        }
        
        position = planet.gameObject.transform.position;
        rotation = planet.gameObject.transform.rotation;
    }
}
