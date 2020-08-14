using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataPlanet {
    public float radiusAroudSun;
    public float planetRadius = 0;
    public float speed;
    public float angle;
    public int number;
    public int blocking;
    public SerializableVector3 position;
    public SerializableQuaternion rotation;

    public DataPlanet(Planet planet) {
        radiusAroudSun = planet.getRadSun();
        planetRadius = planet.getRadPln();
        speed = planet.getSpeed();
        angle = planet.getAngle();
        number = planet.getNumber();
        blocking = planet.amountBlocking();
        /*position = new float[4];
        position[0] = planet.gameObject.transform.position.x;
        position[1] = planet.gameObject.transform.position.y;
        position[2] = planet.gameObject.transform.position.z;*/
        position = planet.gameObject.transform.position;
        rotation = planet.gameObject.transform.rotation;
    }
}
