using System.Collections;
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

    public void free_coin()
    {
        Debug.Log("free_Coin");
        //playerDataBase.Static.COIN = playerDataBase.Static.fifty + playerDataBase.Static.COIN;
    }

    public void hundred_coin()
    {
        Debug.Log("100");
    }

    public void two_hundred_coin()
    {
        Debug.Log("200");
    }

    public void five_hundred_coin()
    {
        Debug.Log("500");
    }

    public void one_thousand_coin()
    {
        Debug.Log("1000");
    }
}
