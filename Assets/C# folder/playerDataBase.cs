using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDataBase : MonoBehaviour {
    public static playerDataBase Static;


    public int HP { get; set; }
    public int SP { get; set; }
    public int MaxHP { get; set; }
    public int MaxSP { get; set; }
    public int MaxHPInitial { get; set; }
    public int MaxSPInitial { get; set; }

    public int ATK { get; set; }
    public int DEF { get; set; }
    public int ATKInitial { get; set; }
    public int DEFInitial { get; set; }

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

    public float fifty { get; set; }
    public float hundred { get; set; }
    public float two_hundred { get; set; }
    public float three_hundred { get; set; }

    public int abilityHPMax { get; set; }
    public int abilitySPMax { get; set; }
    public float abilityATKPercent { get; set; }
    public float abilityDEFPercent { get; set; }

    public float VolSet { get; set; }

    public void serializeSetUp() { //玩家起始值
        MaxHPInitial = 10;
        MaxSPInitial = 20;
        ATKInitial = 5;
        DEFInitial = 0;

        //MaxHP = 10;
        //MaxSP = 7;
        
        //abilityHPMax = 0;
        //abilitySPMax = 0;

        MaxHP = MaxHPInitial + abilityHPMax;
        MaxSP = MaxSPInitial + abilitySPMax;
        HP = MaxHP;
        SP = MaxSP;
        ATK = ATKInitial + (int)(ATKInitial * (abilityATKPercent / 100));
        DEF = DEFInitial + (int)(DEFInitial * (abilityDEFPercent / 100));
        ATKLevel = 1;
        DEFLevel = 1;
        COIN = 0;
        COINBounsPercent = 100;
        POINT = 5;
        ResetTimes = 0;
        currentFloor = 1;
        maxFloor = 1;

        fifty = 50;
        hundred = 100;
        two_hundred = 200;
        three_hundred = 300;


        abilityATKPercent = 100;
        abilityDEFPercent = 100;

    }



    void Awake() {
        //Debug.Log(GetComponent<Transform>().name);
        serializeSetUp();
        DontDestroyOnLoad(transform.gameObject);
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }
    }

}
