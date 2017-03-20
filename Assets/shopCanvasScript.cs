using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shopCanvasScript : MonoBehaviour {
    public Text ATK;
    public Text ATKLevel;
    public Text ATKCostText;

    public Text DEF;
    public Text DEFLevel;
    public Text DEFCostText;

    public Text money;

    public Button ATKUpgradeButton;
    public Button DEFUpgradeButton;

    public int ATKCost;
    public int DEFCost;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ATKLevel.text = "ATK LEVEL : " + playerDataBase.Static.ATKLevel;
        DEFLevel.text = "DEF  LEVEL : " + playerDataBase.Static.DEFLevel;

        ATK.text = "ATK + " + playerDataBase.Static.ATK;
        DEF.text = "Damage -  " + playerDataBase.Static.DEF;

        ATKCostText.text = "Cost : " + ATKCost;
        DEFCostText.text = "Cost : " + DEFCost;

        money.text = "COIN : " + playerDataBase.Static.COIN;
    }

    public void clickATKButton() {
        if (playerDataBase.Static.COIN - ATKCost >= 0) {
            playerDataBase.Static.COIN -= ATKCost;
            ATKCost++;
            playerDataBase.Static.ATKLevel++;
            playerDataBase.Static.ATK++;
        }

    }
    public void clickDEFButton() {
        if (playerDataBase.Static.COIN - DEFCost >= 0) {
            playerDataBase.Static.COIN -= DEFCost;
            DEFCost++;
            playerDataBase.Static.DEFLevel++;
            playerDataBase.Static.DEF++;
        }

    }



}
