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
    }

    void upDateProbabilityArray() {
        ProbabilityArray.Clear();

        ProbabilityArray.Add(normalProbability);
        ProbabilityArray.Add(patrolbability);
        ProbabilityArray.Add(masksmanProbability);
        ProbabilityArray.Add(tankProbability);
    }

    public GameObject selectType() {
        Debug.Log("dddsddddaghit");
        return enemyPrefabArray[itemAndEnemyProcessor.RandomProbabilitySystem(ref ProbabilityArray) - 1];
    }

}
