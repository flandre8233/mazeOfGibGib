using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolNpc : enemyScript {

    public override void SetUp( ) {
        Level = 2;
    }

    public override void enemyAttackPlayerScript()
    {
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

            if (playerDataBase.Static.DEF <= ATK)
            {
                gamemanager.Static.spawnNumberDisplay(chessMovement.Static.gameObject.transform.position, (ATK - playerDataBase.Static.DEF), 5);
                playerDataBase.Static.HP -= (ATK - playerDataBase.Static.DEF);
            }



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
