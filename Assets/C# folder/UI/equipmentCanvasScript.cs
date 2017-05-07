using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class equipmentCanvasScript : MonoBehaviour {
    public Text ATK;
    public Text ATKLevel;
    public Text ATKnext;
    //public Text ATKCostText;

    public Text DEF;
    public Text DEFLevel;
    public Text DEFnext;
    //public Text DEFCostText;

    public Text ATK_money;
    public Text DEF_money;
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        ATKupdate();
        DEFupdate();
    }

    public void ATKupdate()
    {
        ATKLevel.text = "Level" + playerDataBase.Static.ATKLevel;
        ATK_money.text = playerDataBase.Static.equipment_ATKcost + "";
        ATKnext.text = playerDataBase.Static.ATKInitial + "";
    }

    public void DEFupdate()
    {
        DEFLevel.text = "Level" + playerDataBase.Static.DEFLevel;
        DEF_money.text = playerDataBase.Static.equipment_DEFcost + "";
        DEFnext.text = playerDataBase.Static.ATKInitial + "";
    }
}

