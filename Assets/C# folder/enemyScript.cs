using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class enemyScript : enemyDataBase {
    public npcSensor sensor;
    public bool IsAutoSetType = true;
    public bool killTest = false;

    public virtual void SetUp(short monsterLevel) {

    }

    // Use this for initialization
    void Start () {
        //setItemType();
        sensor = GetComponentInChildren<npcSensor>();
        roundScript.Static.roundSystem += enemyAttackPlayerScript;
        roundScript.Static.roundSystem += enemyHPCheck;
        SetUp( (short)playerDataBase.Static.currentFloor );
    }

    /*
    public void setItemType() {
        if (IsAutoSetType) {
            DataBase.type = enemyGenerator.Static.selectType();

            switch (DataBase.type) {
                case enemyType.normal:
                    DataBase.normalSetUp(roundScript.Static.currentArea);
                    break;
                case enemyType.tank:
                    DataBase.tankSetUp(roundScript.Static.currentArea);
                    break;
                case enemyType.patrol:
                    DataBase.patrolSetUp(roundScript.Static.currentArea);
                    break;
                case enemyType.masksman:
                    DataBase.masksmanSetUp(roundScript.Static.currentArea);
                    break;
                default:
                    DataBase.normalSetUp(roundScript.Static.currentArea);
                    break;
            }

        }



        //setItemFunction();

    }
    */

    public int findPlayerRoundNumber = -1;
    public void enemyAttackPlayerScript() {
        if (sensor.isFindPlayer) {
            if (findPlayerRoundNumber < 0) {
                findPlayerRoundNumber = roundScript.Static.round;
            }

            if ( (roundScript.Static.round - findPlayerRoundNumber) % CD == 0) {//是攻擊的回合才行動
                if (playerDataBase.Static.DEF < ATK) {
                    playerDataBase.Static.HP -= (ATK - playerDataBase.Static.DEF);
                }
            }


        }
        else {
            if (findPlayerRoundNumber >= 0) {
                findPlayerRoundNumber = -1;
            }

        }
    }
    
    public void enemyHPCheck() {
        if (HP <= 0 || killTest) {
            roundScript.Static.roundSystem -= enemyAttackPlayerScript;
            roundScript.Static.roundSystem -= enemyHPCheck;
            //roundScript.Static.roundSystem -= roundScript.Static.enemyList[DataBase.UID].GetComponent<enemyScript>().enemyAttackPlayerScript;
            //roundScript.Static.roundSystem -= roundScript.Static.enemyList[DataBase.UID].GetComponent<enemyScript>().enemyHPCheck;

            playerDataBase.Static.COIN += (COIN * (playerDataBase.Static.COINBounsPercent/100) );
            Destroy(gameObject);
        }

    }

}
