﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class roundScript : MonoBehaviour {
    public static roundScript Static;
    public delegate void roundSystemFunction();
    public roundSystemFunction roundSystem;

    public int round = 0;
    public bool IsDead = false;
    public bool isExitTouchPlayer = false;
    public bool isInExitLevel = false;
    bool NeedGenertorThings = false;

    public playerMainScript playerMainScript;

    public List<GameObject> enemyList; 

    public void pastRound() {
        round++;
        roundSystem.Invoke();
        playerMainScript.deadAliveCheck();
    }

    public void OnEnterNextLevel() { // enter next level

        clearLevel();
        //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, -1);
        chessMovement.Static.startLerpMovement = false;
        playerDataBase.Static.currentFloor++;
        if (playerDataBase.Static.currentFloor % 5 == 0) { //到5,10,15,20......關卡
            playerDataBase.Static.POINT += 5;
        }

        mapTerrainGenerator.Static.resetTerrain();
        NeedGenertorThings = true;


    }

    public void clearLevel() {
        mapTerrainGenerator.Static.thisLevelAllFloor.Clear();
        foreach (var item in GameObject.FindGameObjectsWithTag("floor") ) { //看來GameObject.FindGameObjectsWithTag("floor")不太靈活
            Destroy(item);
            //Debug.Log(GameObject.FindGameObjectsWithTag("floor").Length);
            //item.GetComponent<groundScript>().haveSomethingInHere = false;
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("item") ) {
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("enemy")) {
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("exit")) {
            Destroy(item);
        }

    }
    public int selectionX, selectionY;
    int nextFrameLock = 0;
    public void Update() {
        /*
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f )) {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.y;
            Debug.Log(selectionX + " . " + selectionY);
        }
        */
        if (nextFrameLock >= 2) {
            nextFrameLock = 0;
            NeedGenertorThings = false;
            mapThingsGenerator.Static.spawnExitPoint();
            mapThingsGenerator.Static.StartGeneratorTheThings();
            mapThingsGenerator.Static.SerializePlayerPositionToSpawnPoint();
            //mapTerrainGenerator.Static.findLeftGround();
            //mapTerrainGenerator.Static.findRightGround();
            //mapTerrainGenerator.Static.findCenter();
        }
        if (NeedGenertorThings) {
            nextFrameLock ++;
        }
        if (isExitTouchPlayer) {
            isExitTouchPlayer = false;
            OnEnterNextLevel();
        }
        if (IsDead) {
            playerMainScript.GetComponent<chessMovement>().enabled = false;
            Debug.Log("lkdsalkjdalslk");
        }

    }

    public void Awake() {

        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }
        roundSystem += playerMainScript.subSP;
        roundSystem += playerMainScript.checkLife;

        //roundSystem += OnEnterNextLevel;
        //roundSystem += playerMainScript.getItemSet;
    }

}