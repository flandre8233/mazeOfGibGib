using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapThingsGenerator : MonoBehaviour {
    public static mapThingsGenerator Static;

    public List<GameObject> totalfloorCanBePlaceThings;
    public List<GameObject> totalfloorCanBePlaceExit;


    public List<GameObject> allEnemyArray = new List<GameObject>();

    public GameObject enemy;

    //[HideInInspector]
    //public GameObject item;
    
    public GameObject exitGoal;

    GameObject player;

    bool doOnce = false;
    //public int spawnTimes = 15;
    //public int thisLevelspawnTimes = 5;

    public int levelSpawnItemTimes {
        get {
            return 1 + (playerDataBase.Static.currentFloor / 4);
        }
    }
    public int levelSpawnEnemyTimes {
        get {
            return 1 + (playerDataBase.Static.currentFloor / 4);
        }
    }

    [Header("ProbabilitySetting")]
    [Range(0,100)]
    public float itemSpawnProbability;
    [Range(0, 100)]
    public float enemySpawnProbability;
    
    public List<float> ProbabilityArray = new List<float>();

    void Awake () {
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }

        player = GameObject.FindGameObjectWithTag("Player");

        upDateProbabilityArray();
    }

    void upDateProbabilityArray() {
        ProbabilityArray.Clear();

        ProbabilityArray.Add(itemSpawnProbability);
        ProbabilityArray.Add(enemySpawnProbability);
    } //becareful

    public GameObject[] itemArray;
    public GameObject hpmaxPrefab;
    public GameObject spmaxPrefab;

    public GameObject  selectType() {
        return itemArray[itemAndEnemyProcessor.RandomProbabilitySystem(ref itemGenerator.Static.ProbabilityArray) -1];

        /*
        switch () {
            case 1:
                
                //item.AddComponent<HP>();
                break;
            case 2:
                //item.AddComponent<SP>();
                break;
            case 3:
                //item.AddComponent<HPMax>();
                break;
            case 4:
                //item.AddComponent<SpMax>();
                break;
            case 5:
                //item.AddComponent<Coin>();
                break;
            case 6:
                //item.AddComponent<ATKBuff>();
                break;
            case 7:
                //item.AddComponent<DEFBuff>();
                break;
            case 8:
                //item.AddComponent<SPNoCost>();
                break;
            default:
                //item.AddComponent<HP>();
                break;
        }
        */

    }

    public GameObject selectType(itemType type)
    {
        switch (type)
        {
            case itemType.HP:
                return itemArray[ 0 ];
            case itemType.SP:
                return itemArray[ 2 ];
            case itemType.SPNoCost:
                return itemArray[ 3 ];
            case itemType.ATK:
                return itemArray[ 0 ];
            case itemType.DEF:
                return itemArray[ 1 ];
            case itemType.HPMAX:
                return hpmaxPrefab;
            case itemType.SPMAX:
                return spmaxPrefab;
            case itemType.COIN:
                return itemArray[ 0 ];
        }

        return itemArray[ 0 ];
    }

    void StartGeneratorTheThings(int times, string type)
    {

        if (mapTerrainGenerator.Static.thisLevelAllFloor.Count == 0)
        {
            return;
        }

        totalfloorCanBePlaceThings.Clear();

        foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor)
        { // count 有幾多個地版可以spawn物件
            if (item.GetComponent<groundScript>().type == groundType.canSpawnThings && !item.GetComponent<groundScript>().haveSomethingInHere)
            {
                totalfloorCanBePlaceThings.Add(item);
            }
        }
        if (times > totalfloorCanBePlaceThings.Count)
        { //鎖住spawntimes上限 別超出上限
            times = totalfloorCanBePlaceThings.Count;
        }
        

        for (int i = 0; i < times; i++)
        {
            int canPlaceThingsFloorNumber = totalfloorCanBePlaceThings.Count;
            int randomNumber = Random.Range(0, canPlaceThingsFloorNumber); //在可放置東西的地板array上選出一個數字
            //int randomNumberThingsType = itemAndEnemyProcessor.randomSetThingsType(ProbabilityArray); //為這次spawn的物品決定出他的種類
            Vector3 canBePlaceThingsPos = totalfloorCanBePlaceThings[randomNumber].transform.position;
            Vector3 randomPosition = new Vector3(canBePlaceThingsPos.x, canBePlaceThingsPos.y, -1); //放在那裡?
            totalfloorCanBePlaceThings[randomNumber].GetComponent<groundScript>().haveSomethingInHere = true;
            totalfloorCanBePlaceThings.Remove(totalfloorCanBePlaceThings[randomNumber]);

            switch (type)
            { //把結果分類
                case "item":

                    GameObject InstantiateItem = Instantiate(selectType(), randomPosition, Quaternion.Euler(-90, 0, 0));
                    InstantiateItem.transform.position = new Vector3(InstantiateItem.transform.position.x, InstantiateItem.transform.position.y, -0.2f);
                    //InstantiateItem.name = InstantiateItem.GetComponent<itemScript>().ItemType.ToString();
                    //selectType(InstantiateItem);
                    break;

                case "hpmax":

                    GameObject InstantiateHpmax = Instantiate(hpmaxPrefab, randomPosition, Quaternion.Euler(0, 0, 0));
                    InstantiateHpmax.transform.position = new Vector3(InstantiateHpmax.transform.position.x, InstantiateHpmax.transform.position.y, -0.75f);
                    //InstantiateItem.name = InstantiateItem.GetComponent<itemScript>().ItemType.ToString();
                    //selectType(InstantiateItem);
                    break;
                case "spmax":

                    GameObject InstantiateSpmax = Instantiate(spmaxPrefab, randomPosition, Quaternion.Euler(0, 0, 0));
                    InstantiateSpmax.transform.position = new Vector3(InstantiateSpmax.transform.position.x, InstantiateSpmax.transform.position.y, -0.75f);
                    //InstantiateItem.name = InstantiateItem.GetComponent<itemScript>().ItemType.ToString();
                    //selectType(InstantiateItem);
                    break;

                case "enemy":
                    GameObject InstantiateEnemy = Instantiate(enemyGenerator.Static.selectType(), randomPosition, Quaternion.identity);
                    InstantiateEnemy.GetComponent<enemyDataBase>().UID = allEnemyArray.Count;

                    InstantiateEnemy.GetComponent<enemyDataBase>().center = new Vector3(Mathf.Round(randomPosition.x), Mathf.Round(randomPosition.y) ,0); 
                    allEnemyArray.Add(InstantiateEnemy);
                    break;
                default:
                    break;
            }
        }
    }

    public void loadSaveDataSpawnItem(Vector2 pos,itemType type)
    {
        Vector3 spawnPos = new Vector3(pos.x, pos.y, -1);
        //selectType


        if (type == itemType.HPMAX || type == itemType.SPMAX) //果實
        {
            GameObject InstantiateItem = Instantiate(selectType(type), spawnPos, Quaternion.Euler(0, 0, 0));
            InstantiateItem.transform.position = new Vector3(InstantiateItem.transform.position.x, InstantiateItem.transform.position.y, -0.75f);
        }
        else
        {
            GameObject InstantiateItem = Instantiate(selectType(type), spawnPos, Quaternion.Euler(-90, 0, 0));
            InstantiateItem.transform.position = new Vector3(InstantiateItem.transform.position.x, InstantiateItem.transform.position.y, -0.2f);
        }


    }

    public void loadSaveDataSpawnEnemy(Vector2 pos,int type,int hp)
    {
        Vector3 spawnPos = new Vector3(pos.x,pos.y,-1);

        GameObject InstantiateEnemy = Instantiate(enemyGenerator.Static.selectType(type), spawnPos, Quaternion.identity);
        InstantiateEnemy.GetComponent<enemyDataBase>().UID = allEnemyArray.Count;
        InstantiateEnemy.GetComponent<enemyDataBase>().center = new Vector3(spawnPos.x, spawnPos.y, 0);
        InstantiateEnemy.GetComponent<enemyDataBase>().HP = hp;
        allEnemyArray.Add(InstantiateEnemy);
    }

    public void spawnItemAndEnemy()
    {
        if (gamemanager.Static.beLoaded)
        {
            saveGameData saveData = testSaveLoad.Static.mydata;

            foreach (var item in saveData.allItemData)
            {
                Vector2 v2 = new Vector2(item.X,item.Y);
                loadSaveDataSpawnItem(v2,item.type);
            }

            foreach (var item in testSaveLoad.Static.mydata.allChestVector2InMap)
            {
                Vector3 v3 = new Vector3(item.X, item.Y, -0.2f);
                GameObject go = Instantiate(itemArray[4] , v3, Quaternion.identity);
            }

            foreach (var item in saveData.allEnemyData)
            {
                Vector2 v2 = new Vector2(item.X, item.Y);
                loadSaveDataSpawnEnemy(v2, item.levelType,item.HP);
            }

            return;
        }

        if (sampleRandom())
        {
            StartGeneratorTheThings(1, "hpmax");
        }
        if (sampleRandom())
        {
            StartGeneratorTheThings(1, "spmax");
        }

        StartGeneratorTheThings(levelSpawnItemTimes, "item");
        StartGeneratorTheThings(levelSpawnEnemyTimes, "enemy");
    }

    bool sampleRandom()
    {
        int number = Random.Range(0,100) ;
        if (number <= 5)
        {
            return true;
        }
        return false;
    }

    public void spawnExitPoint() {
        if (gamemanager.Static.beLoaded)
        {

            foreach (var item in testSaveLoad.Static.mydata.allExitVector2InExit)
            {
                Vector3 v3 = new Vector3(item.X,item.Y,-1);
                GameObject go = Instantiate(exitGoal, v3, Quaternion.identity);
            }

            return;
        }

        totalfloorCanBePlaceExit.Clear();
        if (mapTerrainGenerator.Static.thisLevelAllFloor.Count != 0) {
            foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor) {
                /*
                if ( (item.GetComponent<groundScript>().TerrainUID == 0 && item.GetComponent<groundScript>().type == groundType.isPortFloor)) {
                    item.GetComponent<groundScript>().type = groundType.startPoint;
                }
                */

                if (item.GetComponent<groundScript>().isDeadEnd() && (item.GetComponent<groundScript>().TerrainUID != 0) || (item.GetComponent<groundScript>().TerrainUID == 0 && item.GetComponent<groundScript>().type == groundType.isPortFloor ) ) {

                    totalfloorCanBePlaceExit.Add(item);
                }
            }
        }

        if (totalfloorCanBePlaceExit.Count == 0) {
            foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor) {

                if (item.GetComponent<groundScript>().passCount == 1) {
                    totalfloorCanBePlaceExit.Add(item);
                    // return;
                }

            }
        }


        if (totalfloorCanBePlaceExit.Count == 0) {
            foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor) {

                if (!item.GetComponent<groundScript>().haveSomethingInHere) {
                    totalfloorCanBePlaceExit.Add(item);
                    break;
                }
            }
        }
        


        for (int i = 0; i < totalfloorCanBePlaceExit.Count; i+=2) {
            createSpawnPoint(totalfloorCanBePlaceExit[i]);
        }


    }

    void createSpawnPoint(GameObject item ) {
        item.GetComponent<groundScript>().type = groundType.canNOTSpawnThings;
        totalfloorCanBePlaceExit.Add(item);
        Vector3 targetV3 = new Vector3(item.transform.position.x, item.transform.position.y, -1);
        Vector3 exitPassV3 = new Vector3(item.GetComponent<groundScript>().passV3.x, item.GetComponent<groundScript>().passV3.y,0);

        GameObject go = Instantiate(exitGoal, targetV3, Quaternion.identity);
        go.transform.LookAt(exitPassV3);
    }

    public void SerializePlayerPositionToSpawnPoint() {
        if (gamemanager.Static.beLoaded)
        {
            chessMovement.Static.CenterGround = chessMovement.Static.getGround();

            return;
        }

        if (roundScript.Static.isEnterStartPoint())
        {
            Vector3 targetV3 = new Vector3(0, -5 , -1);
            player.transform.position = targetV3;

            chessMovement.Static.CenterGround = chessMovement.Static.getGround();
            chessMovement.Static.center = chessMovement.Static.resetCenterV3(chessMovement.Static.CenterGround);
            return;
        }

        if (roundScript.Static.isEnterCheckPoint() ) { //到5,10,15,20......關卡  休息關重生點
            Vector3 targetV3 = new Vector3(1, 1 ,- 1);
            player.transform.position = targetV3;

            chessMovement.Static.CenterGround = chessMovement.Static.getGround();
            chessMovement.Static.center = chessMovement.Static.resetCenterV3(chessMovement.Static.CenterGround); // <--

            return;
        }

        if (mapTerrainGenerator.Static.thisLevelAllFloor.Count != 0)
        {
            ////Debug.Log(mapTerrainGenerator.Static.thisLevelAllFloor[0]);
            Vector3 targetV3 = new Vector3(mapTerrainGenerator.Static.thisLevelAllFloor[0].transform.position.x, mapTerrainGenerator.Static.thisLevelAllFloor[0].transform.position.y, -1);
            player.transform.position = targetV3;
            // //Debug.Log(player.transform.position);
            return;
        }
    }

    void LateUpdate() {
        if (!doOnce) {
            doOnce = true;
            spawnExitPoint();
            SerializePlayerPositionToSpawnPoint();
            chessMovement.Static.moveCheck();
            spawnItemAndEnemy();
            //mapTerrainGenerator.Static.findLeftGround();
            //mapTerrainGenerator.Static.findRightGround();
            //mapTerrainGenerator.Static.findCenter();
            playerMainScript.Static.loadPlayerItem();

            miniMapSpriteManager.Static.startGenIcon();

            if (roundScript.Static.groundCheckSystem != null) // ok
            {
                ////Debug.Log("dllm");
                roundScript.Static.groundCheckSystem.Invoke();
            }

            gamemanager.Static.beLoaded = false;
        }
    }
}


