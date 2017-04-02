using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDataBase : MonoBehaviour {
    public static playerDataBase Static;


    public int HP { get; set; }
    public int SP { get; set; }
    public int MaxHP { get; set; }
    public int MaxSP { get; set; }
    public int ATK { get; set; }
    public int DEF { get; set; }

    public int ATKLevel { get; set; }
    public int DEFLevel { get; set; }

    public float COIN { get; set; }
    public int COINBounsPercent { get; set; }
    public int POINT { get; set; }

    public int HPItemBounsPercent { get; set; }
    public int SPItemBounsPercent { get; set; }
    //public int 

    public int ResetTimes { get; set; }
    public int currentFloor { get; set; }
    public int maxFloor { get; set; }

    public void testOnlySetUp() {
        //MaxHP = 10;
        //MaxSP = 7;
        MaxHP = 32;
        MaxSP = 30;
        HP = MaxHP;
        SP = MaxSP;
        ATK = 5;
        DEF = 0;
        ATKLevel = 1;
        DEFLevel = 1;
        COIN = 40;
        COINBounsPercent = 100;
        POINT = 20;
        ResetTimes = 0;
        currentFloor = 1;
        maxFloor = 1;
    }



    void Awake() {
        //Debug.Log(GetComponent<Transform>().name);
        testOnlySetUp();
        DontDestroyOnLoad(transform.gameObject);
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }
    }

}
