using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP : itemScript {
   public override void SetUp() {
        itemName = "SP";
        AddHP = 0;
        AddSP = 15;
        AddHPMax = 0;
        AddSPMax = 0;
        AddCOIN = 0;
    }
    public override void includeLevelSetUp() {
        
            AddSP = (15 + ((level - 1) * 10));
        Debug.Log("fuck" + AddSP);
        //AddSP = 300;
    }
}
