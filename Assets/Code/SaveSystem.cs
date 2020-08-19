using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    
    /*public static void saveAllPlanets(Planet[] planets) {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/data/planets/all.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        AllPlanetData data = new AllPlanetData(planets);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static AllPlanetData loadAllPlanets() {
        string path = Application.persistentDataPath + "/data/planets/all.data";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            AllPlanetData data = formatter.Deserialize(stream) as AllPlanetData;
            
            stream.Close();
            return data;
        }
        else {
            Debug.Log("SAVE FILE NOT FOUND");
            return null;
        }
    }*/
    
    
    
    public static void savePlanet(Planet planet) {
        BinaryFormatter formatter = new BinaryFormatter();
        Debug.Log("SAVING " + planet.getNumber());

        string path = Application.persistentDataPath + "/single" + planet.getNumber().ToString() + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        DataPlanet data = new DataPlanet(planet);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DataPlanet loadPlanet(int number) {
        string path = Application.persistentDataPath + "/single" + number.ToString() + ".data";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            DataPlanet data = formatter.Deserialize(stream) as DataPlanet;
            
            stream.Close();
            return data;
        }
        else {
            Debug.Log("SAVE FILE NOT FOUND");
            return null;
        }
    }
}
