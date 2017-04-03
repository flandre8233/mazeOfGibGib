using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalNpc : enemyScript {

    public override void SetUp(short monsterLevel) {


        //type = enemyType.normal;
        Level = monsterLevel;
        MaxHP = 20 + (int)((Level - 1) * 1.5f);
        HP = MaxHP;
        ATK = 2 + (int)((Level - 1) * 1.2f);
        CD = 2;
        //DEF = 1;
        COIN = 2 + (int)((Level - 1) * 1.2f);
    }

}
