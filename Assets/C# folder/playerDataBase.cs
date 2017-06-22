using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDataBase : MonoBehaviour {
    public static playerDataBase Static;

    public int HP { get; set; }
    public int SP { get; set; }
    public int HPBuff;
    public int SPBuff;
    public int MaxHP {
        get {
            return MaxHPInitial + abilityHPMax + HPBuff;
        }
    }
    public int MaxSP {
        get {
            return MaxSPInitial + abilitySPMax + SPBuff;
        }
    }
    int MaxHPInitial { get; set; }
     int MaxSPInitial { get; set; }

    public int ATK {
        get {
            return ((int)((1 + ATKLevel) / 100f * ((ATKlevelpercent * 20) + 100)))+ATKBuff;
        }
    }
    public int DEF {
        get {
            return (int)((1 + DEFLevel) / 100f * ((DEFlevelpercent * 20) + 100))+DEFBuff;
        }
    }
    public int ATKBuff;
    public int DEFBuff;
    public int ATKInitial = 10;
    public int DEFInitial = 10;

    public int HPAbility = 3;
    public int SPAbility = 5;

    public int ATKlevelpercent { get;set; }
    public int DEFlevelpercent { get; set; }
    public int ATKLevel { get; set; }
    public int DEFLevel { get; set; }
    public int HpmaxLevel { get; set; }
    public int SpmaxLevel { get; set; }

    public int COIN { get; set; }
    public int POINT { get; set; }



    public int reviveTimes = 0;
    public int ResetTimes { get; set; }
    public int currentFloor { get; set; }
    public int currentLifeMaxFloor { get; set; }
    public int maxFloor { get; set; }


    public int abilityHPMax { get; set; }
    public int abilitySPMax { get; set; }
    //public float abilityATKPercent { get; set; }
    //public float abilityDEFPercent { get; set; }

    public List<monsterLevelSetting> MonsterLevelSettingArray;

    [System.Serializable]
    public class monsterLevelSetting
    {
        public int maxhp = 0;
        public int coin = 0;
        public int atk = 0;
        public int def = 0;

    }

    public int equipment_ATKcost {
        get {
            return (ATKLevel+1) * 500;
        }
    }
    public int equipment_DEFcost {
        get {
            return (DEFLevel+1) * 500;
        }
    }
    public int revivCost {
        get {
            switch (reviveTimes)
            {
                case 0:
                    return 500;
                case 1:
                    return 1000;
                case 2:
                    return 4000;
            }
            return 500 * (reviveTimes - 1) ^ 2 ;
        }

    }

    public float idle_time { get; set; }

    public float VolSet { get; set; }

    public int equipment { get; set; }

    public bool revive_value { get; set; }

    public bool check_start { get; set; }

    public void fullHPSP() {
        HP = MaxHP;
        SP = MaxSP;
    }

    public void serializeSetUp() { //玩家起始值
        MaxHPInitial = 10;
        MaxSPInitial = 20;
        ATKInitial = 10;
        DEFInitial = 10;
        
        
        abilityHPMax = 0;
        abilitySPMax = 0;

        HP = MaxHP;
        SP = MaxSP;

        ATKlevelpercent = 0;
        DEFlevelpercent = 0;
        ATKLevel = 0;
        DEFLevel = 0;
        HpmaxLevel = 0;
        SpmaxLevel = 0;

        COIN = 1000;
        POINT = 5;
        reviveTimes = 0;
        ResetTimes = 0;
        currentFloor = 0;
        maxFloor = 0;
        currentLifeMaxFloor = 0;

        idle_time = 10;




        revive_value = false;
        check_start = true;
    }



    void Awake() {

        serializeSetUp();
        DontDestroyOnLoad(transform.gameObject);
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }
    }



    public void restart_data()
    { //玩家起始值
        //ability_point keep
        MaxHPInitial = 10;
        MaxSPInitial = 20;
        ATKInitial = 10;
        DEFInitial = 10;

        //MaxHP = 10;
        //MaxSP = 7;

        abilityHPMax = 0;
        abilitySPMax = 0;

        HP = MaxHP;
        SP = MaxSP;
        //ATK = ATKInitial + (int)(ATKInitial * (100/abilityATKPercent));
        //DEF = DEFInitial + (int)(DEFInitial * (100/abilityDEFPercent));

        ATKlevelpercent = 0;
        DEFlevelpercent = 0;
        ATKLevel = 0;
        DEFLevel = 0;
        HpmaxLevel = 0;
        SpmaxLevel = 0;

        COIN = 1000;
        POINT = 5;
        currentFloor = 0;
        currentLifeMaxFloor = 0;
        reviveTimes = 0;
        revive_value = false;
        //check_start = false;
    }
}
