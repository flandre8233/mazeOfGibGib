using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class enemyScript : enemyDataBase {
    public npcSensor sensor;
    public bool IsAutoSetType = true;
    public bool killTest = false;

    public Transform playerTransform;

    public virtual void SetUp(short monsterLevel) {

    }

    // Use this for initialization
    void Start () {
        //setItemType();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        sensor = GetComponentInChildren<npcSensor>();
        roundScript.Static.roundSystem += enemyAttackPlayerScript;
        roundScript.Static.roundSystem += enemyHPCheck;
        SetUp( (short)playerDataBase.Static.currentFloor );
        includeLevelHPMax();
        includeLevelDEF();
        includeLevelCOIN();
        includeLevelATK();
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

    int includeLevelHPMax() {
        int HPMax = 0;
        switch (Level) {
            case 1:
                HPMax = 3;
                break;
            case 2:
                HPMax = 6;
                break;
            case 3:
                HPMax = 12;
                break;
            case 4:
                HPMax = 18;
                break;

            default:
                break;
        }
        return HPMax;
    }

    int includeLevelATK() {
        int ATK = 0;
        switch (Level) {
            case 1:
                ATK = 1;
                break;
            case 2:
                ATK = 2;
                break;
            case 3:
                ATK = 4;
                break;
            case 4:
                ATK = 6;
                break;

            default:
                break;
        }
        return ATK;
    }

    int includeLevelDEF() {
        int DEF = 0;
        switch (Level) {
            case 1:
                DEF = 0;
                break;
            case 2:
                DEF = 0;
                break;
            case 3:
                DEF = 0;
                break;
            case 4:
                DEF = 0;
                break;

            default:
                break;
        }
        return DEF;
    }

    int includeLevelCOIN() {
        int COIN = 0;
        switch (Level) {
            case 1:
                COIN = 11;
                break;
            case 2:
                COIN = 56;
                break;
            case 3:
                COIN = 304;
                break;
            case 4:
                COIN = 936;
                break;

            default:
                break;
        }
        COIN += (int)(COIN / 100.0f * (Random.Range(0, 40)-20) );
        return COIN;
    }



    public int findPlayerRoundNumber = -1;
    public GameObject damageDisplayObject;

    public void enemyAttackPlayerScript() {
        if (sensor.isFindPlayer) {
            if (findPlayerRoundNumber < 0) {
                findPlayerRoundNumber = roundScript.Static.round;
            }

            if ( (roundScript.Static.round - findPlayerRoundNumber) % CD == 0) {//是攻擊的回合才行動
                if (playerDataBase.Static.DEF < ATK) {
                    GameObject go = Instantiate(chessMovement.Static.damageDisplayObject, chessMovement.Static.gameObject.transform.position, Quaternion.identity);
                    go.GetComponent<damageDisplay>().spawnDamageDisplay(ATK);
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

    private void Update() {
        allwayFaceAtPlayer();
    }

    public Quaternion ImageLookAt2D(Vector3 from, Vector3 to) {
        Vector3 difference = to - from;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = (Quaternion.Euler(0.0f, 0.0f, rotationZ));
        return rotation;
    }

    public void allwayFaceAtPlayer() {
        float Angle = ImageLookAt2D(transform.position, playerTransform.position).eulerAngles.z;
        transform.rotation = ImageLookAt2D(transform.position, playerTransform.position) ;
        //transform.LookAt(playerTransform);
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
