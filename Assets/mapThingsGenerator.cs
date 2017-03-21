using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapThingsGenerator : MonoBehaviour {
    public static mapThingsGenerator Static;
    public List<GameObject> totalfloorCanBePlaceThings;

    [HideInInspector]
    public GameObject enemy;

    [HideInInspector]
    public GameObject item;

     bool doOnce = false;
    public int spawnTimes = 5;

    [Header("ProbabilitySetting")]
    [Range(1,100)]
    public float itemSpawnProbability;
    [Range(1, 100)]
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

            GameObject[] allFloor;
            allFloor = GameObject.FindGameObjectsWithTag("floor");
            for (int i = 0; i < spawnTimes; i++) {

                totalfloorCanBePlaceThings.Clear();
            if (allFloor.Length != 0) {
                foreach (var item in allFloor) {
                    if (item.GetComponent<groundScript>().canSpawnThings && !item.GetComponent<groundScript>().startPoint && !item.GetComponent<groundScript>().haveSomethingInHere) {
                        totalfloorCanBePlaceThings.Add(item);
                    }
                }
            }


                int canPlaceThingsFloorNumber = totalfloorCanBePlaceThings.Count;
                int randomNumber = Random.Range(0, canPlaceThingsFloorNumber ); //在可放置東西的地板array上選出一個數字
            int randomNumberThingsType = randomSetItemType(); //為這次spawn的物品決定出他的種類
            Vector3 randomPosition = new Vector3(totalfloorCanBePlaceThings[randomNumber].transform.position.x, totalfloorCanBePlaceThings[randomNumber].transform.position.y, -2); //放在那裡?
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

            }
    }

    void LateUpdate() {
        if (!doOnce) {
            doOnce = true;
            StartGeneratorTheThings();
        }
    }
}


