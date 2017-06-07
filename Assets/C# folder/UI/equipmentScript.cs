﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipmentScript : MonoBehaviour {
    public static equipmentScript Static;

    private void Awake()
    {
        if (Static != null)
        {
            Destroy(this);
        }
        else
        {
            Static = this;
        }    
    }

    public void ATK_equ()
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.COIN >= playerDataBase.Static.equipment_ATKcost)
        {
            playerDataBase.Static.COIN -= playerDataBase.Static.equipment_ATKcost;
            playerDataBase.Static.ATKLevel++;
        }
    }
    public void DEF_equ()
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.COIN >= playerDataBase.Static.equipment_DEFcost)
        {
            playerDataBase.Static.COIN -= playerDataBase.Static.equipment_DEFcost;
            playerDataBase.Static.DEFLevel++;
        }
    }




}
