using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKBuff : itemScript {
    public override void SetUp() {
        itemName = "ATKBuff";
        AddHP = 0;
        AddSP = 0;
        AddHPMax = 0;
        AddSPMax = 0;
        AddCOIN = 0;
        continueRound = 5;
        AddATK = 8;
    }
    public override void includeLevelSetUp() {
            AddATK += (int)((level + 2) / 100.0f * 10);
            continueRound += 4 + level;
    }
}
