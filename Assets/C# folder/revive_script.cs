using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revive_script : MonoBehaviour {
    public RectTransform crystal_on;
    public RectTransform crystal_off;
    public RectTransform menu_crystal;

    void Update () {
        //Debug.Log(playerDataBase.Static.revive_value);
	}

    public void crystal_menu()
    {
        if (menu_crystal.gameObject.activeSelf == false)
        {
            menu_crystal.gameObject.SetActive(true);
        }
        else
        {
            menu_crystal.gameObject.SetActive(false);
        }
    }

    public void revive_button()
    {
        if (playerDataBase.Static.revive_value == false)
        {
            playerDataBase.Static.revive_value = true;
            crystal_on.gameObject.SetActive(true);
            crystal_off.gameObject.SetActive(false);
        }
        else
        {
            playerDataBase.Static.revive_value = false;
            crystal_on.gameObject.SetActive(false);
            crystal_off.gameObject.SetActive(true);
        }
    }
}
