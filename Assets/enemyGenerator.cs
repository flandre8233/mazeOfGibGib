using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour {
    public static enemyGenerator Static;


    [Header("ProbabilitySetting")]
    [Range(0, 100)]
    public float normalProbability;
    [Range(0, 100)]
    public float patrolbability;
    [Range(0, 100)]
    public float masksmanProbability;
    [Range(0, 100)]
    public float tankProbability;

    public List<float> ProbabilityArray = new List<float>();

    public GameObject[] enemyPrefabArray;

    void Awake() {
        Static = this;
        upDateProbabilityArray();
        itemAndEnemyProcessor.checkProbabilityOverflow(2,ref ProbabilityArray);
        upDateProbabilityVar();
    }

   void  upDateProbabilityVar() {
        normalProbability = ProbabilityArray[0];
        patrolbability = ProbabilityArray[1];
        masksmanProbability = ProbabilityArray[2];
        tankProbability = ProbabilityArray[3];
    }
    void upDateProbabilityArray() {
        ProbabilityArray.Clear();

        ProbabilityArray.Add(normalProbability);
        ProbabilityArray.Add(patrolbability);
        ProbabilityArray.Add(masksmanProbability);
        ProbabilityArray.Add(tankProbability);
    }

    public GameObject selectType() {
        short type = itemAndEnemyProcessor.randomSetThingsType(ProbabilityArray);
        return enemyPrefabArray[type - 1];
    }

}
