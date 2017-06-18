using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using LitJson;

public static class saveLoadManager {

    static string path = Application.persistentDataPath + "/playerData.dat";

    public static void Save(saveGameData saveGame)
    {
        /*
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerData.dat"); //you can call it anything you want, including the extension. The directories have to exist though.

        bf.Serialize(file, saveGame);
        file.Close();
        Debug.Log("Saved Game: " + Application.persistentDataPath + "/playerData.dat" );
        */

        saveGameData saveData = new saveGameData();
        saveData.testData = 50;

        string jsonData = JsonMapper.ToJson(saveData);

        File.WriteAllText(path, jsonData);
        Debug.Log("successed");

    }

    public static saveGameData Load()
    {
        /*
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);
            saveGameData loadedGame = (saveGameData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Loaded Game: ");
            return loadedGame;
        }
        else
        {
            Debug.Log("File doesn't exist!");
            return null;
        }
        */

        string jsonData = File.ReadAllText(path);

        saveGameData saveData = JsonMapper.ToObject<saveGameData>(jsonData);

        Debug.Log("loaded");
        return saveData;
    }
}


    