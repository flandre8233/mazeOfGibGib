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
        itemAndEnemyProcessor.checkProbabilityOverflow(2, ref ProbabilityArray);
        upDateProbabilityVar();
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
