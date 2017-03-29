using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapThingsGenerator : MonoBehaviour {
    public static mapThingsGenerator Static;

    public List<GameObject> totalfloorCanBePlaceThings;
    public List<GameObject> totalfloorCanBePlaceExit;



    [HideInInspector]
    public GameObject enemy;

    [HideInInspector]
    public GameObject item;

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
        checkProbabilityOverflow(0);
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
    void checkProbabilityOverflow(int SkipCheckProbabilityElementNumber) {
        float sumProbability = 0;
        for (int i = 0; i < ProbabilityArray.Count; i++) {
            if (i != SkipCheckProbabilityElementNumber) {
                sumProbability += ProbabilityArray[i]; //
            }

        }
        if (sumProbability + ProbabilityArray[SkipCheckProbabilityElementNumber] >= 100) { //overflow 100
            sumProbability = (100 - ProbabilityArray[SkipCheckProbabilityElementNumber]);
            for (int i = 0; i < ProbabilityArray.Count; i++) {
                if (i != SkipCheckProbabilityElementNumber) {
                    ProbabilityArray[i] = sumProbability / (ProbabilityArray.Count - 1);
                }

            }
            upDateProbabilityVar();
        }
    }
    public int randomSetItemType() {
        int randomNumber = Random.Range(0, 100);
        float sumProbability = 0;
        int type = 0;
        for (int i = 0; i < ProbabilityArray.Count; i++) {
            if (randomNumber <= sumProbability + ProbabilityArray[i]) {
                return type;
            }
            else {
                sumProbability += ProbabilityArray[i];
                type++;
            }
        }
        return 0;
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
                int canPlaceThingsFloorNumber = totalfloorCanBePlaceThings.Count ;
                int randomNumber = Random.Range(0, canPlaceThingsFloorNumber ); //在可放置東西的地板array上選出一個數字
            int randomNumberThingsType = randomSetItemType(); //為這次spawn的物品決定出他的種類
            Vector3 randomPosition = new Vector3(totalfloorCanBePlaceThings[randomNumber].transform.position.x, totalfloorCanBePlaceThings[randomNumber].transform.position.y, -1); //放在那裡?
            switch (randomNumberThingsType) { //把結果分類
                case 0:

                        GameObject InstantiateItem = Instantiate(item, randomPosition, Quaternion.identity);
                        InstantiateItem.GetComponent<itemScript>().setItemType();
                    InstantiateItem.name = InstantiateItem.GetComponent<itemScript>().ItemType.ToString();
                        break;

                    case 1:

                        Instantiate(enemy, randomPosition, Quaternion.identity);
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
                if (item.GetComponent<groundScript>().isDeadEnd() && item.GetComponent<groundScript>().TerrainUID != 0) {
                    item.GetComponent<groundScript>().type = groundType.canNOTSpawnThings ;
                    totalfloorCanBePlaceExit.Add(item);
                    Vector3 targetV3 = new Vector3(item.transform.position.x,item.transform.position.y,-1 );
                    Instantiate(exitGoal, targetV3,Quaternion.identity);
                }
            }
        }


    }

    public void SerializePlayerPositionToSpawnPoint() {
        if (mapTerrainGenerator.Static.thisLevelAllFloor.Count != 0) {
            foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor) {
                if (item.GetComponent<groundScript>().type == groundType.startPoint) {
                    Vector3 targetV3 = new Vector3(item.transform.position.x, item.transform.position.y, -1);
                    player.transform.position = targetV3;
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


