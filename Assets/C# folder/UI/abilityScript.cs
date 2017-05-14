﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityScript : MonoBehaviour {
	void Update () {
        //Button Hpmax_btn = Hpmax_add.GetComponent<Button>();
        //Hpmax_btn.onClick.AddListener();
    }

    public void Hpmax_add()
    {
        //playerDataBase.Static.ability_point--;
        if(playerDataBase.Static.POINT >= 1)
        { 
            playerDataBase.Static.HpmaxLevel++;
            playerDataBase.Static.abilityHPMax = playerDataBase.Static.abilityHPMax + 3;
            playerDataBase.Static.MaxHP = playerDataBase.Static.MaxHPInitial + playerDataBase.Static.abilityHPMax;
            //Debug.Log(Hpmax_add);
            playerDataBase.Static.POINT--;
        }
    }

    public void Spmax_add()
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.POINT >= 1)
        {
            playerDataBase.Static.SpmaxLevel++;
            playerDataBase.Static.abilitySPMax = playerDataBase.Static.abilitySPMax + 5;
            playerDataBase.Static.MaxSP = playerDataBase.Static.MaxSPInitial + playerDataBase.Static.abilitySPMax;
            //Debug.Log(Spmax_add);
            playerDataBase.Static.POINT--;
        }
    }

    public void ATK_add()
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.POINT >= 1)
        {
            playerDataBase.Static.ATKlevelpercent++;
            playerDataBase.Static.abilityATKPercent = playerDataBase.Static.abilityATKPercent + 20;
            playerDataBase.Static.ATK = (int)(playerDataBase.Static.ATK / 100f * playerDataBase.Static.abilityATKPercent);
            //Debug.Log(playerDataBase.Static.ATKInitial / 100f * playerDataBase.Static.abilityATKPercent);
            playerDataBase.Static.POINT--;
        }
    }

    public void DEF_add()
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.POINT >= 1)
        {
            playerDataBase.Static.DEFlevelpercent++;
            playerDataBase.Static.abilityDEFPercent = playerDataBase.Static.abilityDEFPercent + 20;
            playerDataBase.Static.DEF = (int)(playerDataBase.Static.DEF / 100f * playerDataBase.Static.abilityDEFPercent);
            //Debug.Log(DEF_add);
            playerDataBase.Static.POINT--;
        }
    }
}
