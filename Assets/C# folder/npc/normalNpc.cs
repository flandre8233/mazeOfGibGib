using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalNpc : enemyScript {

    public override void SetUp(short monsterLevel) {
        
        //type = enemyType.normal;
        Level = monsterLevel;
        MaxHP = 1 * (1 + Level / 5) + Level; 
        HP = MaxHP;
        ATK = 1 * (1 + Level / 10);
        CD = 2;
        DEF = 0*(1 + Level / 30);
        COIN = 2 * (1 + Level / 5);
    }

}
