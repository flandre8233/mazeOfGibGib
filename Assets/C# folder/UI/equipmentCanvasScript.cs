using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class equipmentCanvasScript : MonoBehaviour {
    public Text ATK;
    public Text DEF;

    public Text ATKLevel;
    public Text DEFLevel;

    public Text ATKnext;
    public Text DEFnext;

    public Text ATK_money;
    public Text DEF_money;
    //public Text ATKCostText;
    //public Text DEFCostText;
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
        ATK.text = playerDataBase.Static.ATK + "";
        ATKLevel.text = "Level" + playerDataBase.Static.ATKLevel;
        ATK_money.text = playerDataBase.Static.equipment_ATKcost + "";
        ATKnext.text = playerDataBase.Static.ATK + "";
    }

    public void DEFupdate()
    {
        DEF.text = playerDataBase.Static.DEF + "";
        DEFLevel.text = "Level" + playerDataBase.Static.DEFLevel;
        DEF_money.text = playerDataBase.Static.equipment_DEFcost + "";
        DEFnext.text = playerDataBase.Static.DEF + "";
    }

}

