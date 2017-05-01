using System.Collections.Generic;
using UnityEngine;

public class playerDataBaseJson {

    public int HP = 0;
    public int SP = 0;
    public int MaxHP = 10;
    public int MaxSP = 20;
    public int MaxHPInitial = 10;
    public int MaxSPInitial = 20;

    public int ATK = 5;
    public int DEF = 0;
    public int ATKInitial = 5;
    public int DEFInitial = 0;

    public int ATKLevel = 1;
    public int DEFLevel = 1;
    public int HpmaxLevel = 1;
    public int SpmaxLevel = 1;

    public float COIN = 0;
    public int COINBounsPercent = 100;
    public int POINT = 5;

    public int HPItemBounsPercent = 100;
    public int SPItemBounsPercent = 100;
    //public int 

    public int ResetTimes = 0;
    public int currentFloor = 1;
    public int maxFloor = 1;
    public int ability_point = 0;

    public float fifty = 50;
    public float hundred = 100;
    public float two_hundred = 200;
    public float three_hundred = 300;

    public int abilityHPMax = 0;
    public int abilitySPMax = 0;
    public float abilityATKPercent = 100;
    public float abilityDEFPercent = 100;

    public float idle_time = 0.0f;

    public float VolSet = 1.0f;

    public string toJson()
    {
        return JsonUtility.ToJson(this);
    }

}
