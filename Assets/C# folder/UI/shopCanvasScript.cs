using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopCanvasScript : MonoBehaviour {

    public RectTransform shop_menu;
    //public float x = 0;

    public List<Text> buyMenuCoinDisplay;
    public Text adsCoinDisplay;
    public List<int> buyMenuCoinNumber;
    public int adsCoinNumber;

    public Button adsButton; 

    void updateCoin()
    {
        buyMenuCoinNumber[1] = timesCoins(playerDataBase.Static.MonsterLevelSettingArray[3].coin);
        buyMenuCoinNumber[2] = timesCoins(playerDataBase.Static.MonsterLevelSettingArray[3].coin * 2);
        buyMenuCoinNumber[3] = timesCoins(playerDataBase.Static.MonsterLevelSettingArray[3].coin + playerDataBase.Static.MonsterLevelSettingArray[4].coin);
        buyMenuCoinNumber[4] = timesCoins(playerDataBase.Static.MonsterLevelSettingArray[4].coin * 3);

        adsCoinNumber = timesCoins(playerDataBase.Static.MonsterLevelSettingArray[4].coin);
    }

    public void canvas_equ()
    {

        if (playerDataBase.Static.currentAlyreadyWatchAdsLevel < playerDataBase.Static.currentFloor)
        {
            adsButton.interactable = true;
        }
        else
        {
            adsButton.interactable = false;
        }

        if (shop_menu.gameObject.activeInHierarchy == false)
        {
            updateCoin();
            buyMenuCoinDisplay[1].text = buyMenuCoinNumber[1].ToString();
            buyMenuCoinDisplay[2].text = buyMenuCoinNumber[2].ToString();
            buyMenuCoinDisplay[3].text = buyMenuCoinNumber[3].ToString();
            buyMenuCoinDisplay[4].text = buyMenuCoinNumber[4].ToString();
            adsCoinDisplay.text = adsCoinNumber.ToString();
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
        playerDataBase.Static.currentAlyreadyWatchAdsLevel = playerDataBase.Static.currentFloor;
        adsButton.interactable = false;
        playerDataBase.Static.COIN += adsCoinNumber;
        Debug.Log("free_Coin");
        //playerDataBase.Static.COIN = playerDataBase.Static.fifty + playerDataBase.Static.COIN;
    }

    public void hundred_coin()
    {
        playerDataBase.Static.COIN += buyMenuCoinNumber[1];
        playerDataBase.Static.POINT += 0;
        Debug.Log("100");
    }

    public void two_hundred_coin()
    {
        playerDataBase.Static.COIN += buyMenuCoinNumber[2];
        playerDataBase.Static.POINT += 0;
        Debug.Log("200");
    }

    public void five_hundred_coin()
    {
        playerDataBase.Static.COIN += buyMenuCoinNumber[3];
        playerDataBase.Static.POINT += 0;
        Debug.Log("500");
    }

    public void one_thousand_coin()
    {
        playerDataBase.Static.COIN += buyMenuCoinNumber[4];
        playerDataBase.Static.POINT += 1;
        Debug.Log("1000");
    }

     int timesCoins(int coin)
    {
        int number = (int)(coin * (  1+playerDataBase.Static.currentFloor / 5.0f));
        return  number;
    }

}
