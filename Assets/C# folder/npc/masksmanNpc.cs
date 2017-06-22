using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masksmanNpc : enemyScript {
    public override void SetUp( ) {
        Level = 3;
    }

    public override void enemyAttackPlayerScript()
    {
        if (HP <= 0)
        {
            return;
        }

        if (NumberOfActions <= 0)
        {
            return;
        }

        if (!checkPlayerCenterIsInAttackPoint())
        {
            return;
        }

        if (findPlayerRoundNumber < 0)
        {
            findPlayerRoundNumber = roundScript.Static.round;
        }

        if ((roundScript.Static.round - findPlayerRoundNumber) % CD == 0)
        {//是攻擊的回合才行動
            attackFunction();
        }
        else
        {
            if (findPlayerRoundNumber >= 0)
            {
                findPlayerRoundNumber = -1;
            }

        }
    }

}
