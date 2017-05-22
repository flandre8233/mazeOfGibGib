﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revive_script : MonoBehaviour {
    public static revive_script Static;

    public RectTransform crystal_on;
    public RectTransform crystal_off;
    public RectTransform menu_crystal;

    private void Awake()
    {
        if (Static != null)
        {
            Destroy(this);
        }
        else
        {
            Static = this;
        }
    }

    void Update () {
        //Debug.Log(playerDataBase.Static.revive_value);
	}

    public void crystal_menu()
    {
        menu_crystal.gameObject.SetActive( !menu_crystal.gameObject.activeSelf);
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