using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class testSaveLoad : MonoBehaviour {
    public Text text;
    public saveGameData mydata = new saveGameData();
    // Use this for initialization
    void Start ()
    {
        string Platform = "";
#if UNITY_EDITOR
        Platform = "EDITOR";
#elif UNITY_IPHONE
		Platform = "IPHONE";
#elif UNITY_ANDROID
		Platform = "ANDROID";
#endif
        Debug.Log(Platform);
        Debug.Log("dataPath: " + Application.dataPath);
        Debug.Log("persistentDataPath: " + Application.persistentDataPath);
        Debug.Log("streamingAssetsPath: " + Application.streamingAssetsPath);
        Debug.Log("temporaryCachePath: " + Application.temporaryCachePath);


        mydata = saveLoadManager.Load();
        text.text = mydata.testData.ToString();


    }
	
    public void buttonDown()
    {
        mydata.testData += 10;
        saveLoadManager.Save(mydata);
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.C))
        {
            mydata.testData += 10;
            saveLoadManager.Save(mydata);
        }

	}
}
