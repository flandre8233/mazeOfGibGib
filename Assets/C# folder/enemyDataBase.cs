using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType
{
    normal,
    tank,
    patrol,
    masksman
};

public class enemyDataBase : MonoBehaviour {
    public enemyType type;
    public int UID = 0;

    public short Level { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int ATK { get; set; }
    public int CD { get; set; }
    //public int DEF { get; set; }

    public float COIN { get; set; }

    public void testOnlySetUp(short monsterLevel) {
        type = enemyType.normal;
        Level = monsterLevel;
        MaxHP = 20 + (int)( (Level-1) * 1.5f) ;
        HP = MaxHP;
        ATK = 2 + (int)((Level - 1) * 1.2f);
        CD = 1;
        //DEF = 1;
        COIN = 2 + (int)((Level - 1) * 1.2f);
    }

    public void normalSetUp(short monsterLevel) {
        type = enemyType.normal;
        Level = monsterLevel;
        MaxHP = 20 + (int)((Level - 1) * 1.5f);
        HP = MaxHP;
        ATK = 2 + (int)((Level - 1) * 1.2f);
        CD = 2;
        //DEF = 1;
        COIN = 2 + (int)((Level - 1) * 1.2f);
    }

    public void tankSetUp(short monsterLevel) {
        type = enemyType.tank;
        Level = monsterLevel;
        MaxHP = 20 + (int)((Level - 1) * 1.5f);
        HP = MaxHP;
        ATK = 2 + (int)((Level - 1) * 1.2f);
        CD = 4;
        //DEF = 1;
        COIN = 2 + (int)((Level - 1) * 1.2f);
    }

    public void patrolSetUp(short monsterLevel) {
        type = enemyType.patrol;
        Level = monsterLevel;
        MaxHP = 20 + (int)((Level - 1) * 1.5f);
        HP = MaxHP;
        ATK = 2 + (int)((Level - 1) * 1.2f);
        CD = 4;
        //DEF = 1;
        COIN = 2 + (int)((Level - 1) * 1.2f);
    }

    public void masksmanSetUp(short monsterLevel) {
        type = enemyType.masksman;
        Level = monsterLevel;
        MaxHP = 20 + (int)((Level - 1) * 1.5f);
        HP = MaxHP;
        ATK = 2 + (int)((Level - 1) * 1.2f);
        CD = 2;
        //DEF = 1;
        COIN = 2 + (int)((Level - 1) * 1.2f);
    }

    void Awake() {
    }

}
