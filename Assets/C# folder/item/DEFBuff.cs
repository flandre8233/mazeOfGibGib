using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEFBuff : itemScript {

    public override int AddDEF {
        get {
            addDEF  = (level + 2) * 10;
            return addDEF;
        }
    }

    public override int ContinueRound {
        get {  
            continueRound = 4 + level;
            return continueRound;
        }
    }


    public override void SetUp() {
        type = itemType.DEF;
    }
}
