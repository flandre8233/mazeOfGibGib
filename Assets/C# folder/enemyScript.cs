using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class enemyScript : MonoBehaviour {
    public npcSensor sensor;
    enemyDataBase DataBase;
    public bool IsAutoSetType = true;
    public bool killTest = false;

    // Use this for initialization
    void Start () {
       DataBase = GetComponent<enemyDataBase>();
        DataBase.testOnlySetUp(roundScript.Static.currentArea);
        setItemType();
        roundScript.Static.roundSystem += enemyAttackPlayerScript;
        roundScript.Static.roundSystem += enemyHPCheck;
    }

    public void setItemType() {
        if (IsAutoSetType) {
            DataBase.type = enemyGenerator.Static.selectType();
        }
        //setItemFunction();

    }

    public void enemyAttackPlayerScript() {
        if (sensor.isFindPlayer) {
            if (playerDataBase.Static.DEF < DataBase.ATK ) {
                Debug.Log("hit");
                playerDataBase.Static.HP -= (DataBase.ATK - playerDataBase.Static.DEF);
            }
        }
    }

    public void enemyHPCheck() {
        if (DataBase.HP <= 0 || killTest) {
            roundScript.Static.roundSystem -= enemyAttackPlayerScript;
            roundScript.Static.roundSystem -= enemyHPCheck;
            //roundScript.Static.roundSystem -= roundScript.Static.enemyList[DataBase.UID].GetComponent<enemyScript>().enemyAttackPlayerScript;
            //roundScript.Static.roundSystem -= roundScript.Static.enemyList[DataBase.UID].GetComponent<enemyScript>().enemyHPCheck;

            playerDataBase.Static.COIN += (DataBase.COIN * (playerDataBase.Static.COINBounsPercent/100) );
            Destroy(gameObject);
        }

    }

}
