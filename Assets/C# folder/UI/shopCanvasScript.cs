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
        playerDataBase.Static.COIN += timesCoins(playerDataBase.Static.MonsterLevelSettingArray[3].coin);
        playerDataBase.Static.POINT += 0;
        Debug.Log("100");
    }

    public void two_hundred_coin()
    {
        playerDataBase.Static.COIN += timesCoins(playerDataBase.Static.MonsterLevelSettingArray[3].coin*2);
        playerDataBase.Static.POINT += 0;
        Debug.Log("200");
    }

    public void five_hundred_coin()
    {
        playerDataBase.Static.COIN += timesCoins(playerDataBase.Static.MonsterLevelSettingArray[3].coin+playerDataBase.Static.MonsterLevelSettingArray[4].coin);
        playerDataBase.Static.POINT += 0;
        Debug.Log("500");
    }

    public void one_thousand_coin()
    {
        playerDataBase.Static.COIN += timesCoins(playerDataBase.Static.MonsterLevelSettingArray[4].coin*3);
        playerDataBase.Static.POINT += 1;
        Debug.Log("1000");
    }

     int timesCoins(int coin)
    {
        int number = (int)(coin * (1 - playerDataBase.Static.currentFloor / 5.0f));
        return  number;
    }

}
