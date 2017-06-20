using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using LitJson;

public static class saveLoadManager
{

    static string path = Application.persistentDataPath + "/playerData.txt";

    public static void Save(saveGameData saveGame)
    {

        string jsonData = JsonMapper.ToJson(saveGame);

        File.WriteAllText(path, jsonData);
        Debug.Log("successed");

    }

    public static void clearSave( )
    {
        string jsonData = "";
        File.WriteAllText(path, jsonData);
        Debug.Log("successed");
    }

    public static saveGameData Load()
    {


        if (!File.Exists(path))
        {
            Debug.Log("no file detect");



            return new saveGameData();
        }
        

       string jsonData = File.ReadAllText(path);

        saveGameData saveData = JsonMapper.ToObject<saveGameData>(jsonData);

        if (saveData.HP <= 0 || !saveData.define)
        {
            Debug.Log("deadFile");

            saveData.define = false;
            return saveData;
        }

        Debug.Log("loaded");
        return saveData;
    }
}


    