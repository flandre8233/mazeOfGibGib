using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour {
    public static tutorial Static;

    public GameObject tutorialObject;

    private void Awake()
    {
        if (Static == null)
        {
            Static = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void turnOnTutorial()
    {
        if (playerDataBase.Static.isReadFromSaveFile)
        {
            return;
        }
        tutorialObject.SetActive(true);
    }

}
