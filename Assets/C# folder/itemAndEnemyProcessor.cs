using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class itemAndEnemyProcessor {

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

    public static short randomSetThingsType(List<float> ProbabilityArray) {
        int randomNumber = Random.Range(0, 100);
        float sumProbability = 0;
        short type = 1;
        for (int i = 0; i < ProbabilityArray.Count; i++) {
            if (randomNumber <= sumProbability + ProbabilityArray[i]) {
                break;
            }
            else {
                sumProbability += ProbabilityArray[i];
                type++;
            }
        }
        return type;
    }

}
