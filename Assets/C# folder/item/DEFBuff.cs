using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEFBuff : itemScript {
    public override void SetUp() {
        itemName = "DEFBuff";
        AddHP = 0;
        AddSP = 0;
        AddHPMax = 0;
        AddSPMax = 0;
        AddCOIN = 0;
        continueRound = 5;
        AddDEF = 30;
    }

    public override void includeLevelSetUp() {
            AddDEF = (level + 2) * 10;
            continueRound = 4 + level;
    }
}
