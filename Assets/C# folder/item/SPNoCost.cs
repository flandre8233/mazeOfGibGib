﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPNoCost : itemScript
{
    public override void SetUp() {
        itemName = "SPNoCost";
        AddHP = 0;
        AddSP = 1;
        AddHPMax = 0;
        AddSPMax = 0;
        AddCOIN = 0;
        SPNoCostTime = 5;

    }

    public override void includeLevelSetUp()
    {
        SPNoCostTime *= level;
    }


}
