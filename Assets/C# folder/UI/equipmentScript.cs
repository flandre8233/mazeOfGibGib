using System.Collections;
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
            playerDataBase.Static.equipment_ATKcost = playerDataBase.Static.equipment_ATKcost + 500;
            playerDataBase.Static.ATKLevel++;
            playerDataBase.Static.ATK += nextAtk();
        }
    }

    public int  nextAtk()
    {
        return playerDataBase.Static.ATK+1;
    }

    public void DEF_equ()
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.COIN >= playerDataBase.Static.equipment_DEFcost)
        {
            playerDataBase.Static.COIN -= playerDataBase.Static.equipment_DEFcost;
            playerDataBase.Static.equipment_DEFcost = playerDataBase.Static.equipment_DEFcost + 500;
            playerDataBase.Static.DEFLevel++;
            playerDataBase.Static.DEF+= nextDEF();
        }
    }

    public int nextDEF()
    {
        return playerDataBase.Static.DEF + 1;
    }


}
