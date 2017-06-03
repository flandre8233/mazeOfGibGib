using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKBuff : itemScript {
    
    public override int AddATK {
        get {
            addATK = ((level + 2) * 10);
            return addATK;
        }
    }

    public override int ContinueRound {
        get {
            continueRound = 4 + level;
            return continueRound;
        }
    }

    public override void SetUp() {
        type = itemType.ATK;
    }
}
