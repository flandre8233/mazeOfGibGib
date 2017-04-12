using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankNpc : enemyScript
{

    public override void SetUp(short curLevel) {
        Level = 1;
        MaxHP = 1 * (1 + curLevel / 5) + Level;
        HP = MaxHP;
        ATK = 1 * (1 + curLevel / 10);
        CD = 2;
        DEF = 0 * (1 + curLevel / 30);
        COIN = 2 * (1 + curLevel / 5);
    }

}
