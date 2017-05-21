using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class itemAndEnemyProcessor
{

    /*
    public static void checkProbabilityOverflow(int SkipCheckProbabilityElementNumber, ref List<float> ProbabilityArray) {
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

        }
    }
    */

    /// <summary>
    /// 包含不同機率處理的randomSystem 輸出隨機後的array index數字
    /// </summary>
    /// <param name="listOrlProbability"></param>
    /// <returns>
    /// random物件機率array
    /// </returns>
    /// 
    public static short RandomProbabilitySystem(ref List<float> listOrlProbability)
    {
        return randomSetThingsType(RandomCheckProbabilitySystem(ref listOrlProbability));
    }

     static short randomSetThingsType(List<float> ProbabilityArray)
    {
        int randomNumber = Random.Range(0, 100);
        float sumProbability = 0;
        short type = 1;
        for (int i = 0; i < ProbabilityArray.Count; i++)
        {
            if (randomNumber <= sumProbability + ProbabilityArray[i])
            {
                break;
            }
            else
            {
                sumProbability += ProbabilityArray[i];
                type++;
            }
        }
        return type;
    }

    #region otherRandom
     static short randomSetThingsType(List<GameObject> ObjectArray)
    {
        List<float> ProbabilityArray = new List<float>();
        float avgProbability = 100.0f / ObjectArray.Count;
        foreach (var item in ObjectArray)
        {
            ProbabilityArray.Add(avgProbability);
        }

        return randomSetThingsType(ProbabilityArray);
    }

     static short randomSetThingsType(GameObject[] ObjectArray)
    {
        List<float> ProbabilityArray = new List<float>();
        float avgProbability = 100.0f / ObjectArray.Length;
        foreach (var item in ObjectArray)
        {
            ProbabilityArray.Add(avgProbability);
        }

        return randomSetThingsType(ProbabilityArray);
    }
    #endregion

    /// <summary>
    /// 入既係要random物件機率array 之後會將佢地既所有機率都化成 100%以內 輸出經100%化處理的 機率ARRAY
    /// </summary>
     public static List<float> RandomCheckProbabilitySystem(ref List<float> listOrlProbability)
    {
        List<float> lotteryPro = new List<float>();
        float proCount = 0.0f;
        float proPer = 0.0f;

        foreach (var item in listOrlProbability)
        {
            proCount += item;
        }
        proPer = 100 / proCount;

        foreach (var item in listOrlProbability)
        {
            lotteryPro.Add(item * proPer);
        }

        listOrlProbability = lotteryPro;
        return listOrlProbability;
    }


}
