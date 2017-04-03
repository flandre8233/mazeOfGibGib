using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour {
    public static enemyGenerator Static;


    [Header("ProbabilitySetting")]
    [Range(0, 100)]
    public float normalProbability;
    [Range(0, 100)]
    public float tankProbability;
    [Range(0, 100)]
    public float patrolbability;
    [Range(0, 100)]
    public float masksmanProbability;

    public List<float> ProbabilityArray = new List<float>();

    void Awake() {
        Static = this;
        upDateProbabilityArray();
        itemAndEnemyProcessor.checkProbabilityOverflow(2,ref ProbabilityArray);
        upDateProbabilityVar();
    }

   void  upDateProbabilityVar() {
        normalProbability = ProbabilityArray[0];
        tankProbability = ProbabilityArray[1];
        patrolbability = ProbabilityArray[2];
        masksmanProbability = ProbabilityArray[3];
    }
    void upDateProbabilityArray() {
        ProbabilityArray.Clear();

        ProbabilityArray.Add(normalProbability);
        ProbabilityArray.Add(tankProbability);
        ProbabilityArray.Add(patrolbability);
        ProbabilityArray.Add(masksmanProbability);
    }

    public void selectType(GameObject enemy) {
        short type = itemAndEnemyProcessor.randomSetThingsType(ProbabilityArray);
        switch (type) {
            case 1:
                enemy.AddComponent<normalNpc>();
                break;
                //return enemyType.normal;
            case 2:
                enemy.AddComponent<tankNpc>();
                break;
            //return enemyType.tank;
            case 3:
                enemy.AddComponent<patrolNpc>();
                break;
            //return enemyType.patrol;
            case 4:
                enemy.AddComponent<masksmanNpc>();
                break;
            //return enemyType.masksman;
            default:
                break;
        }
        
    }

}
