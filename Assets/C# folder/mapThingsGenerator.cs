using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapThingsGenerator : MonoBehaviour {
    public static mapThingsGenerator Static;

    public List<GameObject> totalfloorCanBePlaceThings;
    public List<GameObject> totalfloorCanBePlaceExit;



    [HideInInspector]
    public GameObject enemy;

    //[HideInInspector]
    //public GameObject item;

    [HideInInspector]
    public GameObject exitGoal;

    public GameObject player;

    bool doOnce = false;
    public int spawnTimes = 15;
    public int thisLevelspawnTimes = 5;

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

    public void StartGeneratorTheThings() {
        thisLevelspawnTimes = spawnTimes;
        totalfloorCanBePlaceThings.Clear();
        if (mapTerrainGenerator.Static.thisLevelAllFloor.Count != 0) {
            foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor) {
                if (item.GetComponent<groundScript>().type == groundType.canSpawnThings) {
                    totalfloorCanBePlaceThings.Add(item);
                }
            }
        }

        if (thisLevelspawnTimes > totalfloorCanBePlaceThings.Count) { //鎖住spawntimes上限 別超出上限
            thisLevelspawnTimes = totalfloorCanBePlaceThings.Count;
        }


        for (int i = 0; i < thisLevelspawnTimes; i++) {
            int canPlaceThingsFloorNumber = totalfloorCanBePlaceThings.Count;
            int randomNumber = Random.Range(0, canPlaceThingsFloorNumber); //在可放置東西的地板array上選出一個數字
            int randomNumberThingsType = itemAndEnemyProcessor.randomSetThingsType(ProbabilityArray); //為這次spawn的物品決定出他的種類
            Vector3 randomPosition = new Vector3(totalfloorCanBePlaceThings[randomNumber].transform.position.x, totalfloorCanBePlaceThings[randomNumber].transform.position.y, -1); //放在那裡?
            switch (randomNumberThingsType) { //把結果分類
                case 1:

                    GameObject InstantiateItem = Instantiate(selectType() , randomPosition, Quaternion.Euler(-90,0,0) );
                    InstantiateItem.transform.position = new Vector3(InstantiateItem.transform.position.x, InstantiateItem.transform.position.y,-0.2f);
                    //InstantiateItem.name = InstantiateItem.GetComponent<itemScript>().ItemType.ToString();
                    //selectType(InstantiateItem);
                    break;

                case 2:
                    GameObject InstantiateEnemy = Instantiate(enemy, randomPosition, Quaternion.identity);
                    enemyGenerator.Static.selectType(InstantiateEnemy);
                    break;
                default:
                    break;
            }
            totalfloorCanBePlaceThings[randomNumber].GetComponent<groundScript>().haveSomethingInHere = true;
            totalfloorCanBePlaceThings.Remove(totalfloorCanBePlaceThings[randomNumber]);
        }
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
        //Debug.Log(playerDataBase.Static.currentFloor);
        if (playerDataBase.Static.currentFloor % roundScript.Static.checkPoint == 0) { //到5,10,15,20......關卡  休息關重生點
            Vector3 targetV3 = new Vector3(1,1 -1);
            player.transform.position = targetV3;

            return;
        }
        /*
        if (mapTerrainGenerator.Static.thisLevelAllFloor.Count != 0) {
            foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor) {
                if (item.GetComponent<groundScript>().type == groundType.startPoint) {
                    Vector3 targetV3 = new Vector3(item.transform.position.x, item.transform.position.y, -1);
                    player.transform.position = targetV3;
                }
            }
        }
        groundType.isPortFloor
        */

        if (mapTerrainGenerator.Static.thisLevelAllFloor.Count != 0) {
            foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor) {
                if (item.GetComponent<groundScript>().type == groundType.startPoint) {
                    Vector3 targetV3 = new Vector3(item.transform.position.x, item.transform.position.y, -1);
                    player.transform.position = targetV3;
                    return;
                }
            }
        }
    }

    void LateUpdate() {
        if (!doOnce) {
            doOnce = true;
            spawnExitPoint();
            StartGeneratorTheThings();
            SerializePlayerPositionToSpawnPoint();

            //mapTerrainGenerator.Static.findLeftGround();
            //mapTerrainGenerator.Static.findRightGround();
            //mapTerrainGenerator.Static.findCenter();
        }
    }
}


