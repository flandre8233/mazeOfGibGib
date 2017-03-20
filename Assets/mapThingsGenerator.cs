using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapThingsGenerator : MonoBehaviour {
    public static mapThingsGenerator Static;
    public List<GameObject> totalfloorCanBePlaceThings;

    public GameObject enemy;
    public GameObject item;

     bool doOnce = false;
    public int spawnTimes = 5;

    public int itemSpawnProbability;
    public int enemySpawnProbability;

    void Awake () {
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }
	}

    int randomSpawnType() {
        int randomNumberThingsType = Random.Range(0, 100);
        int sumProbability = 0;
        if (randomNumberThingsType <= sumProbability + itemSpawnProbability) {
            return 0;
        }
        else {
            sumProbability += itemSpawnProbability;
            if (randomNumberThingsType <= sumProbability + enemySpawnProbability) {
                return 1;
            }
            
            else {
                sumProbability += enemySpawnProbability;
                return 0;
            }
            
        }
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
            int randomNumberThingsType = randomSpawnType(); //為這次spawn的物品決定出他的種類
            Vector3 randomPosition = new Vector3(totalfloorCanBePlaceThings[randomNumber].transform.position.x, totalfloorCanBePlaceThings[randomNumber].transform.position.y, -2); //放在那裡?
            switch (randomNumberThingsType) { //把結果分類
                case 0:

                        GameObject InstantiateItem = Instantiate(item, randomPosition, Quaternion.identity);
                        InstantiateItem.GetComponent<itemScript>().setItemType();
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


