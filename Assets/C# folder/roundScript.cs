using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class roundScript : MonoBehaviour {
    public static roundScript Static;
    public delegate void roundSystemFunction();
    public roundSystemFunction roundSystem;

    public int round = 0;
    public bool IsDead = false;
    public bool isExitTouchPlayer = false;
    public bool isInExitLevel = false;
    bool NeedGenertorThings = false;

    public bool isProcessingRound = false;

    public List<GameObject> enemyList; 

    public void pastRound() {
        isProcessingRound = true;
        round++;
        roundSystem.Invoke();
        playerMainScript.Static.deadAliveCheck();
    }


    public bool movementProcessingChecker = false;


    public void RoundProcessingChecker() {
        if (isProcessingRound) {
            if (!movementProcessingChecker) {
                isProcessingRound = false;
                // Processing is complete
            }
        }

    }

    public short currentArea = 1;
    public void OnEnterNextLevel() { // enter next level
        mapTerrainGenerator.Static.terrainLength++;
        chessMovement.Static.model.transform.rotation = Quaternion.Euler(0, 0, 0);
        clearLevel();
        //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, -1);
        chessMovement.Static.startLerpMovement = false;
        movementProcessingChecker = false;
        playerDataBase.Static.currentFloor++;
        if (playerDataBase.Static.currentFloor % 5 == 0) { //到5,10,15,20......關卡
            playerDataBase.Static.POINT += 5;
            currentArea++;
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
        playerMainScript.Static.inATKBuff = playerMainScript.Static.ATKBuff();
        playerMainScript.Static.inDEFBuff = playerMainScript.Static.DEFBuff();


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
            playerMainScript.Static.GetComponent<chessMovement>().enabled = false;
            
            Debug.Log("lkdsalkjdalslk");
        }
        RoundProcessingChecker();
    }

    public void Awake() {
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }
        
    }

    public void Start() {
        roundSystem += playerMainScript.Static.subSP;
        roundSystem += playerMainScript.Static.checkLife;

        //roundSystem += OnEnterNextLevel;
        //roundSystem += playerMainScript.getItemSet;
    }

}
