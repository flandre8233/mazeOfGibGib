using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class roundScript : MonoBehaviour {
    public static roundScript Static;
    public delegate void roundSystemFunction();
    public roundSystemFunction roundSystem;
    public roundSystemFunction enemyMovement;
    public roundSystemFunction enemyAttack;
    [Range(1, 100)]
    public short checkPoint;

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
        sortEnemyList();
        sortEnemyMove();
        if (roundSystem != null)
        {
            roundSystem.Invoke();
        }
        if (enemyMovement != null)
        {
            enemyMovement.Invoke();
        }
        if (enemyAttack != null)
        {
            Debug.Log("do");
            StartCoroutine(waitPlayerMove() );
            //enemyAttack.Invoke();
        }
        playerMainScript.Static.deadAliveCheck();
    }

    private IEnumerator waitPlayerMove( )
    {
        do
        {
            yield return null;
        } while (movementProcessingChecker);
            enemyAttack.Invoke();
    }
    

    public void sortEnemyList()
    {
        GameObject save;
        for (int i = 0; i < mapThingsGenerator.Static.allEnemyArray.Count; i++)
        {
            for (int j = 0; j < mapThingsGenerator.Static.allEnemyArray.Count - 1; j++)
            {

                //Vector3.Distance(chessMovement.Static.transform.position,mapThingsGenerator.Static.allEnemyArray[j].transform.position)
                if (Vector3.Distance(chessMovement.Static.transform.position, mapThingsGenerator.Static.allEnemyArray[j].transform.position) > Vector3.Distance(chessMovement.Static.transform.position, mapThingsGenerator.Static.allEnemyArray[j + 1].transform.position) && i != j)
                {
                    save = mapThingsGenerator.Static.allEnemyArray[j];
                    mapThingsGenerator.Static.allEnemyArray[j] = mapThingsGenerator.Static.allEnemyArray[j + 1];
                    mapThingsGenerator.Static.allEnemyArray[j + 1] = save;
                }
            }

        }

        for (int i = 0; i < mapThingsGenerator.Static.allEnemyArray.Count; i++) //重發uid
        {
            mapThingsGenerator.Static.allEnemyArray[i].GetComponent<enemyScript>().UID = i;
        }

    }

    public void sortEnemyMove() //work
    {
        enemyMovement = null;

        foreach (var item in mapThingsGenerator.Static.allEnemyArray)
        {
            if (item.GetComponent<enemyScript>().Level == 2)
            {
                enemyMovement += item.GetComponent<enemyScript>().move;
            }
        }

    }

    public bool movementProcessingChecker = false;
    public bool DoAttackAniProcessingChecker = false;

    public void RoundProcessingChecker() {
        Debug.Log(!movementProcessingChecker +" "+ !DoAttackAniProcessingChecker + " " + checkallEnemy());
        if (isProcessingRound) {
            if (!movementProcessingChecker && !DoAttackAniProcessingChecker && checkallEnemy() ) {
                isProcessingRound = false;
                // Processing is complete
            }
        }

    }

    bool checkallEnemy()
    {
        foreach (var item in mapThingsGenerator.Static.allEnemyArray)
        {
            if (item.GetComponent<enemyScript>().startLerpMovement)
            {
                return false;
            }
        }
        return true;
    }



    public short currentArea = 1;
    public void OnEnterNextLevel() { // enter next level
        isInExitLevel = true;
        mapTerrainGenerator.Static.terrainLength = 7 + playerDataBase.Static.currentFloor; //新增地形
        chessMovement.Static.model.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        clearLevel();
        //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, -1);
        
        //
        playerDataBase.Static.currentFloor++; //目前關卡+1
        if (playerDataBase.Static.currentFloor % (checkPoint/2) == 0) { //到5,10,15,20......關卡
            playerDataBase.Static.POINT += 5;
            currentArea++;

        }

        mapTerrainGenerator.Static.mapLimit.x = 5+((playerDataBase.Static.currentFloor / checkPoint) + (playerDataBase.Static.currentFloor / (checkPoint / 2)));
        mapTerrainGenerator.Static.mapLimit.y = 5+((playerDataBase.Static.currentFloor / checkPoint) + (playerDataBase.Static.currentFloor / (checkPoint / 2)));

        if (wallControl.Static != null)
            wallControl.Static.syncBackgroundSize((int)mapTerrainGenerator.Static.mapLimit.x, (int)mapTerrainGenerator.Static.mapLimit.y);

        enterLevel();


        NeedGenertorThings = true;
        
    }
    
    public void enterLevel()
    {
        if (isEnterStartPoint())
        {
            playerDataBase.Static.fullHPSP();
            mapTerrainGenerator.Static.startPointTerrain();
            return;
        }

        //playerDataBase.Static.currentFloor % 10 == 0 || 
        if (isEnterCheckPoint())
        { //到5,10,15,20......關卡

            playerDataBase.Static.fullHPSP();
            mapTerrainGenerator.Static.checkPointTerrain();
        }
        else
        {
            mapTerrainGenerator.Static.resetTerrain();
        }


    }

    public bool isEnterStartPoint()
    {
        if (playerDataBase.Static.currentFloor <= 0)//到  休息關重生點
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isEnterCheckPoint()
    {
        if (playerDataBase.Static.currentFloor % checkPoint == 0)//到  休息關重生點
        { 
            return true;
        }
        else
        {
            return false;
        }


    }

    public void clearLevel()
    {
        mapTerrainGenerator.Static.thisLevelAllFloor.Clear();
        foreach (var item in GameObject.FindGameObjectsWithTag("floor"))
        { //看來GameObject.FindGameObjectsWithTag("floor")不太靈活
            //item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + 5);
            Destroy(item);
            //Debug.Log(GameObject.FindGameObjectsWithTag("floor").Length);
            //item.GetComponent<groundScript>().haveSomethingInHere = false;
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("item"))
        {
            //item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + 3);
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("enemy"))
        {
            //item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + 3);
            item.GetComponent<enemyScript>().delEnemy();
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("exit"))
        {
            //item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + 5);
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("chest"))
        {
            //item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + 5);
            Destroy(item);
        }

        /*
        foreach (var item in GameObject.FindGameObjectsWithTag("enemy")) {
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("enemy")) {
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("enemy")) {
            Destroy(item);
        }
        */


    }
    public int selectionX, selectionY;
    int nextFrameLock = 0;
    public void Update() {
        playerMainScript.Static.inATKBuffStatus = playerMainScript.Static.ATKBuff();
        playerMainScript.Static.inDEFBuffStatus = playerMainScript.Static.DEFBuff();


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
            mapThingsGenerator.Static.spawnItemAndEnemy();
            mapThingsGenerator.Static.SerializePlayerPositionToSpawnPoint();

            chessMovement.Static.center = new Vector3(chessMovement.Static.gameObject.transform.position.x, chessMovement.Static.gameObject.transform.position.y, 0);
            chessMovement.Static.moveCheck();
            chessMovement.Static.startLerpMovement = true;

            //chessMovement.Static.gameObject.GetComponentInChildren<Animator>().Play("idle");
            chessMovement.Static.gameObject.GetComponentInChildren<Animator>().SetBool("run", false);
            chessMovement.Static.gameObject.GetComponentInChildren<Animator>().SetBool("idle", true);
            movementProcessingChecker = false;

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

        if (IsDead) {//dead
            playerMainScript.Static.GetComponent<chessMovement>().enabled = false;
            Debug.Log("dead");
            //chessMovement.Static.gameObject.GetComponentInChildren<Animator>().SetBool("dead_bool", true);
            chessMovement.Static.charactor_move.SetTrigger("dead");
            IsDead = false;
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

        if (wallControl.Static != null)  wallControl.Static.syncBackgroundSize((int)mapTerrainGenerator.Static.mapLimit.x, (int)mapTerrainGenerator.Static.mapLimit.y);
    }

    public void Start() {
        roundSystem += playerMainScript.Static.subSP;
        roundSystem += playerMainScript.Static.checkLife;
        enterLevel();
        //roundSystem += RoundProcessingChecker;
        //roundSystem += OnEnterNextLevel;
        //roundSystem += playerMainScript.getItemSet;
    }

}
