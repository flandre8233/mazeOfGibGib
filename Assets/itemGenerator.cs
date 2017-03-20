using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemGenerator : MonoBehaviour {
    public static itemGenerator Static;

    void Awake () {
        Static = this;
	}



    public int hpitemProbability;
    public int spitemProbability;
    public int hpmaxitemProbability;
    public int spmaxitemProbability;
    public int coinitemProbability;

    public itemScript.itemType randomSetItemType() {
        int randomNumber = Random.Range(0, 100);
        int sumProbability = 0;
        if (randomNumber <= sumProbability + hpitemProbability) {
            return itemScript.itemType.hpItem;
        }
        else {
            sumProbability += hpitemProbability;
            if (randomNumber <= sumProbability + spitemProbability) {
                return itemScript.itemType.spItem;
            }
            else {
                sumProbability += spitemProbability;
                if (randomNumber <= sumProbability + hpmaxitemProbability) {
                    return itemScript.itemType.hpmaxItem;
                }
                else {
                    sumProbability += hpmaxitemProbability;
                    if (randomNumber <= sumProbability + spmaxitemProbability) {
                        return itemScript.itemType.spmaxItem;
                    }
                    else {
                        sumProbability += spmaxitemProbability;
                        if (randomNumber <= sumProbability + coinitemProbability) {
                            return itemScript.itemType.coinItem;
                        }
                        else {
                            sumProbability += coinitemProbability;
                            return itemScript.itemType.hpItem;
                        }
                    }
                }
            }
        }
        sumProbability += coinitemProbability;
    }
}
