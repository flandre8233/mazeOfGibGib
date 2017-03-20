﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDataBase : MonoBehaviour {

    public int UID = 0;

    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int ATK { get; set; }
    //public int DEF { get; set; }

    public int COIN { get; set; }

    public void testOnlySetUp() {
        MaxHP = 20;
        HP = MaxHP;
        ATK = 2;
        //DEF = 1;
        COIN = 2;
    }

    void Awake() {
        testOnlySetUp();
    }

}
