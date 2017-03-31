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
    //public int DEF { get; set; }

    public float COIN { get; set; }

    public void testOnlySetUp(short monsterLevel) {
        type = enemyType.normal;
        Level = monsterLevel;
        MaxHP = 20 + (int)( (Level-1) * 1.5f) ;
        HP = MaxHP;
        ATK = 2 + (int)((Level - 1) * 1.2f); ;
        //DEF = 1;
        COIN = 2;
    }

    void Awake() {
    }

}
