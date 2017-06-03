using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP : itemScript {

    public override int AddSP {
        get {
            addSP = (15 + ((level - 1) * 10));
            return addSP;
        }
    }

    public override void SetUp()
    {
        type = itemType.SP;

    }
}
