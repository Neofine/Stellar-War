using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    public static void savePlanet(Planet planet) {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/game.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        DataPlanet data = new DataPlanet(planet);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DataPlanet loadPlanet() {
        string path = Application.persistentDataPath + "/game.data";
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
