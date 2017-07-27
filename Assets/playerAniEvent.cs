using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAniEvent : MonoBehaviour {

    public void attackAnimationUseOnlyFunction()
    {
        
        chessMovement.Static.attackNpc(chessMovement.Static.touchEnemy);
    }
}
