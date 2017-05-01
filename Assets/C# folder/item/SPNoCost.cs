using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPNoCost : itemScript
{
    /// <summary>
    ///  fuck u bitch
    /// </summary>
    public override int SPNoCostTime {
        get {
            sPNoCostTime *= level;
            return sPNoCostTime;
        }
    }

    public override void SetUp() {
        itemName = "SPNoCost";

    }
}
