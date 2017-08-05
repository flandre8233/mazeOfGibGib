using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class testSaveLoad : MonoBehaviour {
    public static testSaveLoad Static;
    public saveGameData mydata = new saveGameData();
    // Use this for initialization

    public GameObject camera;
    public bool alreadyInStart = false;
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

    void Start ()
    {
        alreadyInStart = true;

        mydata = saveLoadManager.Load();
        if (mydata.define )
        {
            gamemanager.Static.beLoaded = true;
            loadDataToPlayerData(mydata);
        }

    }


    public saveGameData savePlayerDataToJSON(saveGameData mydata)
    {
        mydata = new saveGameData();
        mydata.allFloorVector2 = new List<saveGameData.floor>();
        mydata.allItemData = new List<saveGameData.item>();
        mydata.allEnemyData = new List<saveGameData.enemy>();
        mydata.playerItem = new saveGameData.item[2];
        mydata.allExitVector2InExit = new List<saveGameData.vector2>();
        mydata.allChestVector2InMap = new List<saveGameData.vector2>();

        mydata.define = true;

        playerDataBase PD = playerDataBase.Static;
        mydata.abilityHPMax = PD.abilityHPMax;
        mydata.abilitySPMax = PD.abilitySPMax;
        mydata.ATKBuff = PD.ATKBuff;
        mydata.ATKLevel = PD.ATKLevel;
        mydata.ATKlevelpercent = PD.ATKlevelpercent;
        mydata.COIN = PD.COIN;
        mydata.currentFloor = PD.currentFloor;
        mydata.currentLifeMaxFloor = PD.currentLifeMaxFloor;
        mydata.DEFBuff = PD.DEFBuff;
        mydata.DEFLevel = PD.DEFLevel;
        mydata.DEFlevelpercent = PD.DEFlevelpercent;
        mydata.HP = PD.HP;
        mydata.HPBuff = PD.HPBuff;
        mydata.maxFloor = PD.maxFloor;
        mydata.POINT = PD.POINT;
        mydata.ResetTimes = PD.ResetTimes;
        mydata.SPBuff = PD.SPBuff;
        mydata.SP = PD.SP;
        mydata.reviveTimes = PD.reviveTimes;
        mydata.revive_value = PD.revive_value;
        mydata.currentAlyreadyWatchAdsLevel = PD.currentAlyreadyWatchAdsLevel;

        mydata.playerCenter = saveDataVector((int)chessMovement.Static.center.x, (int)chessMovement.Static.center.y );

        mydata.lookDir = chessMovement.Static.faceDirection;
        mydata.cameraEuler = (int)camera.transform.rotation.eulerAngles.z;

        mydata.runTimeDouble = PD.runTimeDouble;

        if (playerMainScript.Static.itemArrayClone[0] != null)
        {
            mydata.playerItem[0] = playerItemSaveToSaveData(0);
        }
        if (playerMainScript.Static.itemArrayClone[1] != null)
        {
            mydata.playerItem[1] = playerItemSaveToSaveData(1);
        }


        foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor)
        {
            saveGameData.floor v2 = new saveGameData.floor();
            v2.X = (int)item.transform.position.x ;
            v2.Y =(int)item.transform.position.y;

            v2.isSpike = item.GetComponent<groundScript>().isSpike;
            if (v2.isSpike)
            {
                v2.curRoundCountDown = item.GetComponent<Spike>().curRoundCountDown;
            }


            mydata.allFloorVector2.Add(v2);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("exit"))
        {
            saveGameData.vector2 v2 = saveDataVector((int)item.transform.position.x, (int)item.transform.position.y);
            mydata.allExitVector2InExit.Add(v2);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("chest"))
        {
            saveGameData.vector2 v2 = saveDataVector((int)item.transform.position.x, (int)item.transform.position.y);
            mydata.allChestVector2InMap.Add(v2);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("item"))
        {
            saveGameData.item itemData = new saveGameData.item();
            itemData.X = (int)item.transform.position.x;
            itemData.Y = (int)item.transform.position.y;
            itemData.type = item.GetComponent<itemScript>().type;
            mydata.allItemData.Add(itemData);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("enemy"))
        {
            saveGameData.enemy itemData = new saveGameData.enemy();
            itemData.X = (int)item.transform.position.x;
            itemData.Y = (int)item.transform.position.y;
            itemData.levelType = item.GetComponent<enemyDataBase>().Level;
            itemData.HP = item.GetComponent<enemyDataBase>().HP;
            mydata.allEnemyData.Add(itemData);
        }

        return mydata;
    }

    saveGameData.item playerItemSaveToSaveData(int number)
    {
        saveGameData.item item = new saveGameData.item();
        item.level = playerMainScript.Static.itemArrayClone[number].level;
        item.type = playerMainScript.Static.itemArrayClone[number].type;

        return item;
    }

    saveGameData.vector2 saveDataVector(int x,int y)
    {
        saveGameData.vector2 v2 = new saveGameData.vector2();
        v2.X = x;
        v2.Y = y;
        return v2;
    }

    void loadDataToPlayerData(saveGameData mydata)
    {
        playerDataBase PD = playerDataBase.Static;

        PD.isReadFromSaveFile = true;

        PD.abilityHPMax = mydata.abilityHPMax;
        PD.abilitySPMax = mydata.abilitySPMax;
        PD.ATKBuff = mydata.ATKBuff;
        PD.ATKLevel = mydata.ATKLevel;
        PD.ATKlevelpercent = mydata.ATKlevelpercent;
        PD.COIN = mydata.COIN;
        PD.currentFloor = mydata.currentFloor;
        PD.currentLifeMaxFloor = mydata.currentLifeMaxFloor;
        PD.DEFBuff = mydata.DEFBuff;
        PD.DEFLevel = mydata.DEFLevel;
        PD.DEFlevelpercent = mydata.DEFlevelpercent;
        PD.HP = mydata.HP;
        PD.HPBuff = mydata.HPBuff;
        PD.maxFloor = mydata.maxFloor;
        PD.POINT = mydata.POINT;
        PD.ResetTimes = mydata.ResetTimes;
        PD.SPBuff = mydata.SPBuff;
        PD.SP = mydata.SP;
        PD.reviveTimes = mydata.reviveTimes;
        PD.revive_value = mydata.revive_value;
        PD.currentAlyreadyWatchAdsLevel = mydata.currentAlyreadyWatchAdsLevel;

        PD.runTimeDouble = mydata.runTimeDouble;

        chessMovement.Static.faceDirection =  mydata.lookDir;
        chessMovement.Static.charFace(chessMovement.Static.faceDirection);

        camera.transform.rotation = Quaternion.Euler(0,0, mydata.cameraEuler);

        chessMovement.Static.center = new Vector3(mydata.playerCenter.X, mydata.playerCenter.Y,0);
        chessMovement.Static.transform.position = new Vector3(mydata.playerCenter.X, mydata.playerCenter.Y, -1);

        revive_script.Static.reviveLight();
        playerMainScript.Static.displayCloseDeadWarning();
    }
    

    private void OnApplicationQuit()
    {
        mydata = savePlayerDataToJSON(mydata);
        saveLoadManager.Save(mydata);
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (!alreadyInStart)
        {
            return;
        }
        mydata = savePlayerDataToJSON(mydata);
        saveLoadManager.Save(mydata);

        //Debug.Log("OnApplicationFocus");
    }

    void OnApplicationPause(bool pauseStatus)
    {
        //mydata = savePlayerDataToJSON(mydata);
        //saveLoadManager.Save(mydata);
        ////Debug.Log("OnApplicationPause");
    }
    private void OnDestroy()
    {
        //Debug.Log("ondestroy");
    }
}
