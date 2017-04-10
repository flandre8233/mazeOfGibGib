using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEFBuff : itemScript {
    public override void SetUp() {
        AddHP = 0;
        AddSP = 0;
        AddHPMax = 0;
        AddSPMax = 0;
        AddCOIN = 0;
        
    }

    public override void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") { //hit Player
            Debug.Log("playerAHit");
            continueRound = 5;
            AddDEF = 8;
            playerMainScript.Static.DEFBuffSetUp(continueRound,AddDEF);
        }
    }
}
