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
    public bool DoAttackAniProcessingChecker = false;

    public void RoundProcessingChecker() {
        if (isProcessingRound) {
            if (!movementProcessingChecker && !DoAttackAniProcessingChecker) {
                isProcessingRound = false;
                // Processing is complete
            }
        }

    }

    public short currentArea = 1;
    public void OnEnterNextLevel() { // enter next level
        isInExitLevel = true;
        mapTerrainGenerator.Static.terrainLength++; //新增地形
        chessMovement.Static.model.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        clearLevel();
        //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, -1);
        
        movementProcessingChecker = false;
        playerDataBase.Static.currentFloor++; //目前關卡+1
        if (playerDataBase.Static.currentFloor % 5 == 0) { //到5,10,15,20......關卡
            playerDataBase.Static.POINT += 5;
            currentArea++;
            mapTerrainGenerator.Static.mapLimit.x++;//增大地圖框架
            mapTerrainGenerator.Static.mapLimit.y++;
            if (playerDataBase.Static.currentFloor % 10 == 0) { //到5,10,15,20......關卡
                mapTerrainGenerator.Static.mapLimit.x++; //增大地圖框架
                mapTerrainGenerator.Static.mapLimit.y++;
            }
        }

   

        wallControl.Static.syncBackgroundSize((int)mapTerrainGenerator.Static.mapLimit.x, (int)mapTerrainGenerator.Static.mapLimit.y);
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

            //chessMovement.Static.startLerpMovement = false;
            chessMovement.Static.center = chessMovement.Static.gameObject.transform.position;
            chessMovement.Static.hitObjectPosition = new Vector3(chessMovement.Static.center.x, chessMovement.Static.center.y, -1);

            //chessMovement.Static.gameObject.GetComponentInChildren<Animator>().Play("idle");
            chessMovement.Static.gameObject.GetComponentInChildren<Animator>().SetBool("run", false);
            chessMovement.Static.gameObject.GetComponentInChildren<Animator>().SetBool("idle", true);

            //mapTerrainGenerator.Static.findLeftGround();
            //mapTerrainGenerator.Static.findRightGround();
            //mapTerrainGenerator.Static.findCenter();
            isInExitLevel = false;
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

        wallControl.Static.syncBackgroundSize((int)mapTerrainGenerator.Static.mapLimit.x, (int)mapTerrainGenerator.Static.mapLimit.y);
    }

    public void Start() {
        roundSystem += playerMainScript.Static.subSP;
        roundSystem += playerMainScript.Static.checkLife;

        //roundSystem += OnEnterNextLevel;
        //roundSystem += playerMainScript.getItemSet;
    }

}
