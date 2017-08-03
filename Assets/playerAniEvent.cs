using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAniEvent : MonoBehaviour {

    public void attackAnimationUseOnlyFunction()
    {
        if (chessMovement.Static.touchEnemy != null)
        {
            chessMovement.Static.touchEnemy.GetComponent<enemyScript>().NpcTakeDamage(playerDataBase.Static.ATK);
        }
        chessMovement.Static.thisFrameMoved = true;
    }
}
