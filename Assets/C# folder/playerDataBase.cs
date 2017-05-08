using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDataBase : MonoBehaviour {
    public static playerDataBase Static;

    public playerDataBaseJson playerDataJson = new playerDataBaseJson();

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

    public int ATKlevelpercent { get;set; }
    public int DEFlevelpercent { get; set; }
    public int ATKLevel { get; set; }
    public int DEFLevel { get; set; }
    public int HpmaxLevel { get; set; }
    public int SpmaxLevel { get; set; }

    public float COIN { get; set; }
    public int COINBounsPercent { get; set; }
    public int POINT { get; set; }

    public int HPItemBounsPercent { get; set; }
    public int SPItemBounsPercent { get; set; }
    //public int 

    public int ResetTimes { get; set; }
    public int currentFloor { get; set; }
    public int maxFloor { get; set; }
    public int ability_point { get; set; }

    public float fifty { get; set; }
    public float hundred { get; set; }
    public float two_hundred { get; set; }
    public float three_hundred { get; set; }

    public int abilityHPMax { get; set; }
    public int abilitySPMax { get; set; }
    public float abilityATKPercent { get; set; }
    public float abilityDEFPercent { get; set; }

    public int equipment_ATKcost { get; set; }
    public int equipment_DEFcost { get; set; }

    public float idle_time { get; set; }

    public float VolSet { get; set; }

    public int equipment { get; set; }

    public void fullHPSP() {
        HP = MaxHP;
        SP = MaxSP;
    }

    public void serializeSetUp() { //玩家起始值
        MaxHPInitial = 1000;
        MaxSPInitial = 2000;
        ATKInitial = 10;
        DEFInitial = 10;

        //MaxHP = 10;
        //MaxSP = 7;
        
        abilityHPMax = 0;
        abilitySPMax = 0;

        MaxHP = MaxHPInitial + abilityHPMax;
        MaxSP = MaxSPInitial + abilitySPMax;
        HP = MaxHP;
        SP = MaxSP;
        abilityATKPercent = 100;
        abilityDEFPercent = 100;
        //ATK = ATKInitial + (int)(ATKInitial * (100/abilityATKPercent));
        ATK = 0;
        DEF = 1000;
        //DEF = DEFInitial + (int)(DEFInitial * (100/abilityDEFPercent));

        ATKlevelpercent = 0;
        DEFlevelpercent = 0;
        ATKLevel = 0;
        DEFLevel = 0;
        HpmaxLevel = 0;
        SpmaxLevel = 0;

        COIN = 10000;
        COINBounsPercent = 100;
        POINT = 1;
        ResetTimes = 0;
        currentFloor = 0;
        maxFloor = 0;

        idle_time = 10;

        fifty = 50;
        hundred = 100;
        two_hundred = 200;
        three_hundred = 300;

        equipment_ATKcost = 500;
        equipment_DEFcost = 500;
    }



    void Awake() {
        //Debug.Log(GetComponent<Transform>().name);

        string jsonString = playerDataJson.toJson();

        playerDataJson = JsonUtility.FromJson<playerDataBaseJson>(jsonString);

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
