using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemGenerator : MonoBehaviour {
    public static itemGenerator Static;


    [Header("ProbabilitySetting")]
    [Range(0, 100)]
    public float hpitemProbability;
    [Range(0, 100)]
    public float spitemProbability;
    [Range(0, 100)]
    public float hpmaxitemProbability;
    [Range(0, 100)]
    public float spmaxitemProbability;
    [Range(0, 100)]
    public float coinitemProbability;

    public List<float> ProbabilityArray = new List<float>();

    void Awake() {
        Static = this;
        upDateProbabilityArray();
        checkProbabilityOverflow(2);
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
    void upDateProbabilityVar() {
        hpitemProbability = ProbabilityArray[0];
        spitemProbability = ProbabilityArray[1];
        hpmaxitemProbability = ProbabilityArray[2];
        spmaxitemProbability = ProbabilityArray[3];
        coinitemProbability = ProbabilityArray[4];
    }
    void upDateProbabilityArray() {
        ProbabilityArray.Clear();

        ProbabilityArray.Add(hpitemProbability);
        ProbabilityArray.Add(spitemProbability);
        ProbabilityArray.Add(hpmaxitemProbability);
        ProbabilityArray.Add(spmaxitemProbability);
        ProbabilityArray.Add(coinitemProbability);
    }
    public itemScript.itemType randomSetItemType() {
        int randomNumber = Random.Range(0, 100);
        float sumProbability = 0;
        int type=1;
        for (int i = 0; i < ProbabilityArray.Count ; i++) {
            if (randomNumber <= sumProbability + ProbabilityArray[i] ) {
                break;
            }
            else {
                sumProbability += ProbabilityArray[i];
                type++;
            }
        }
        switch (type) {
            case 1:
                return itemScript.itemType.hpItem;
            case 2:
                return itemScript.itemType.spItem;
            case 3:
                return itemScript.itemType.hpmaxItem;
            case 4:
                return itemScript.itemType.spmaxItem;
            case 5:
                return itemScript.itemType.coinItem;
            default:
                break;
        }

        return itemScript.itemType.hpItem;

        #region legacy randomSystem
        /*
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
        */
        #endregion
    }
}
