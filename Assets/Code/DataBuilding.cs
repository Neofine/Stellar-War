using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class DataBuilding {
    public int planetNr;
    public bool added;
    //public GameObject disturbing;
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public string exactName;

    public DataBuilding(Building building) {
        if (building.getPlanet() == null)
            building.refreshPlanet();
        planetNr = building.getPlanet().getNumber();
        added = building.isAdded();
        //disturbing = building.distObj();
        exactName = building.getName();
        
        position = building.gameObject.transform.position;
        rotation = building.gameObject.transform.rotation;
    }
}