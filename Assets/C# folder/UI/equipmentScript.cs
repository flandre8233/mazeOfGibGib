using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipmentScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ATK_equ()
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.COIN >= playerDataBase.Static.equipment_ATKcost)
        {
            playerDataBase.Static.COIN -= playerDataBase.Static.equipment_ATKcost;
            playerDataBase.Static.equipment_ATKcost = playerDataBase.Static.equipment_ATKcost + 500;
            playerDataBase.Static.ATKLevel++;
            playerDataBase.Static.ATKInitial++;
        }
    }

    public void DEF_equ()
    {
        //playerDataBase.Static.ability_point--;
        if (playerDataBase.Static.COIN >= playerDataBase.Static.equipment_DEFcost)
        {
            playerDataBase.Static.COIN -= playerDataBase.Static.equipment_DEFcost;
            playerDataBase.Static.equipment_DEFcost = playerDataBase.Static.equipment_DEFcost + 500;
            playerDataBase.Static.DEFLevel++;
            playerDataBase.Static.DEFInitial++;
        }
    }
}
