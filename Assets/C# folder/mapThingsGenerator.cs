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

    [HideInInspector]
    public GameObject exitGoal;

    public GameObject player;

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

        upDateProbabilityArray();
        itemAndEnemyProcessor.checkProbabilityOverflow(0, ref ProbabilityArray);
        upDateProbabilityVar();
        
    }

    void upDateProbabilityArray() {
        ProbabilityArray.Clear();

        ProbabilityArray.Add(itemSpawnProbability);
        ProbabilityArray.Add(enemySpawnProbability);
    } //becareful
    void upDateProbabilityVar() {
        itemSpawnProbability = ProbabilityArray[0];
        enemySpawnProbability = ProbabilityArray[1];
    }

    public GameObject[] itemArray;
    public GameObject hpmaxPrefab;
    public GameObject spmaxPrefab;

    public GameObject  selectType() {
        return itemArray[itemAndEnemyProcessor.randomSetThingsType(itemGenerator.Static.ProbabilityArray)-1];

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
            Vector3 randomPosition = new Vector3(totalfloorCanBePlaceThings[randomNumber].transform.position.x, totalfloorCanBePlaceThings[randomNumber].transform.position.y, -1); //放在那裡?
            totalfloorCanBePlaceThings[randomNumber].GetComponent<groundScript>().haveSomethingInHere = true;
            totalfloorCanBePlaceThings.Remove(totalfloorCanBePlaceThings[randomNumber]);

            switch (type)
            { //把結果分類
                case "item":

                    GameObject InstantiateItem = Instantiate(selectType(), randomPosition, Quaternion.Euler(-90, 0, 0));
                    InstantiateItem.transform.position = new Vector3(InstantiateItem.transform.position.x, InstantiateItem.transform.position.y, -0.2f);
                    //InstantiateItem.name = InstantiateItem.GetComponent<itemScript>().ItemType.ToString();
                    //selectType(InstantiateItem);
                    return;

                case "hpmax":

                    GameObject InstantiateHpmax = Instantiate(hpmaxPrefab, randomPosition, Quaternion.Euler(0, 0, 0));
                    InstantiateHpmax.transform.position = new Vector3(InstantiateHpmax.transform.position.x, InstantiateHpmax.transform.position.y, -0.75f);
                    //InstantiateItem.name = InstantiateItem.GetComponent<itemScript>().ItemType.ToString();
                    //selectType(InstantiateItem);
                    return;
                case "spmax":

                    GameObject InstantiateSpmax = Instantiate(spmaxPrefab, randomPosition, Quaternion.Euler(0, 0, 0));
                    InstantiateSpmax.transform.position = new Vector3(InstantiateSpmax.transform.position.x, InstantiateSpmax.transform.position.y, -0.75f);
                    //InstantiateItem.name = InstantiateItem.GetComponent<itemScript>().ItemType.ToString();
                    //selectType(InstantiateItem);
                    return;

                case "enemy":
                    GameObject InstantiateEnemy = Instantiate(enemyGenerator.Static.selectType(), randomPosition, Quaternion.identity);
                    InstantiateEnemy.GetComponent<enemyDataBase>().UID = allEnemyArray.Count;
                    allEnemyArray.Add(InstantiateEnemy);
                    return;
                default:
                    return;
            }
        }
    }

    public void spawnItemAndEnemy()
    {
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
        if (number <= 100)
        {
            return true;
        }
        return false;
    }

    public void spawnExitPoint() {
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
        if (roundScript.Static.isEnterCheckPoint() ) { //到5,10,15,20......關卡  休息關重生點
            Vector3 targetV3 = new Vector3(1,1 -1);
            player.transform.position = targetV3;
            return;
        }

        if (mapTerrainGenerator.Static.thisLevelAllFloor.Count != 0) {
            Debug.Log(mapTerrainGenerator.Static.thisLevelAllFloor[0] );
                    Vector3 targetV3 = new Vector3(mapTerrainGenerator.Static.thisLevelAllFloor[0].transform.position.x, mapTerrainGenerator.Static.thisLevelAllFloor[0].transform.position.y, -1);
                    player.transform.position = targetV3;
            Debug.Log(player.transform.position);
                    return;
        }
    }

    void LateUpdate() {
        if (!doOnce) {
            doOnce = true;
            spawnExitPoint();
            SerializePlayerPositionToSpawnPoint();
            spawnItemAndEnemy();
            //mapTerrainGenerator.Static.findLeftGround();
            //mapTerrainGenerator.Static.findRightGround();
            //mapTerrainGenerator.Static.findCenter();
        }
    }
}


