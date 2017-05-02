using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityScript : MonoBehaviour {
	void Update () {
        //Button Hpmax_btn = Hpmax_add.GetComponent<Button>();
        //Hpmax_btn.onClick.AddListener();
    }

    public void Hpmax_add(int Hpmax_add)
    {
        //playerDataBase.Static.ability_point--;
        if(playerDataBase.Static.ability_point >= 1)
        { 
            Hpmax_add = playerDataBase.Static.HpmaxLevel++;
            playerDataBase.Static.abilityHPMax = playerDataBase.Static.abilityHPMax + 3;
            playerDataBase.Static.MaxHP = playerDataBase.Static.MaxHPInitial + playerDataBase.Static.abilityHPMax;
            //Debug.Log(Hpmax_add);
            playerDataBase.Static.ability_point--;
        }
    }

    public void Spmax_add(int Spmax_add)
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.ability_point >= 1)
        {
            Spmax_add = playerDataBase.Static.SpmaxLevel++;
            playerDataBase.Static.abilitySPMax = playerDataBase.Static.abilitySPMax + 5;
            playerDataBase.Static.MaxSP = playerDataBase.Static.MaxSPInitial + playerDataBase.Static.abilitySPMax;
            //Debug.Log(Spmax_add);
            playerDataBase.Static.ability_point--;
        }
    }

    public void ATK_add(int ATK_add)
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.ability_point >= 1)
        {
            ATK_add = playerDataBase.Static.ATKLevel++;
            playerDataBase.Static.abilityATKPercent = playerDataBase.Static.abilityATKPercent + 20;
            playerDataBase.Static.ATK = (int)(playerDataBase.Static.ATKInitial / 100f * playerDataBase.Static.abilityATKPercent);
            //Debug.Log(playerDataBase.Static.ATK);
            //Debug.Log(playerDataBase.Static.ATKInitial / 100f * playerDataBase.Static.abilityATKPercent);
            playerDataBase.Static.ability_point--;
        }
    }

    public void DEF_add(int DEF_add)
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.ability_point >= 1)
        {
            DEF_add = playerDataBase.Static.DEFLevel++;
            playerDataBase.Static.abilityDEFPercent = playerDataBase.Static.abilityDEFPercent + 20;
            playerDataBase.Static.DEF = (int)(playerDataBase.Static.DEFInitial / 100f * playerDataBase.Static.abilityDEFPercent);
            //Debug.Log(DEF_add);
            playerDataBase.Static.ability_point--;
        }
    }
}
