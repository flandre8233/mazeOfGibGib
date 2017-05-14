﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopCanvasScript : MonoBehaviour {

    public RectTransform shop_menu;
    //public float x = 0;

    public void canvas_equ()
    {
        if (shop_menu.gameObject.activeInHierarchy == false)
        {
            shop_menu.gameObject.SetActive(true);
        }
        else
        {
            shop_menu.gameObject.SetActive(false);
        }
    }

    public void canvas_equ_off()
    {
        if (shop_menu.gameObject.activeInHierarchy == true)
        {
            shop_menu.gameObject.SetActive(false);
        }
        else
        {
            shop_menu.gameObject.SetActive(true);
        }
    }

    public void fifty_coin()
    {
        //Debug.Log("50");
        playerDataBase.Static.COIN = playerDataBase.Static.fifty + playerDataBase.Static.COIN;
    }

    public void hundred_coin()
    {
        //Debug.Log("100");
        playerDataBase.Static.COIN = playerDataBase.Static.hundred + playerDataBase.Static.COIN;
    }

    public void two_hundred_coin()
    {
        playerDataBase.Static.COIN = playerDataBase.Static.two_hundred + playerDataBase.Static.COIN;
        //Debug.Log("200");
    }

    public void three_hundred_coin()
    {
        playerDataBase.Static.COIN = playerDataBase.Static.three_hundred + playerDataBase.Static.COIN;
        //Debug.Log("300");
    }
}
