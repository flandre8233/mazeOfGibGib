using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class roundScript : MonoBehaviour {
    public static roundScript Static;
    public delegate void roundSystemFunction();
    public roundSystemFunction roundSystem;

    public int round = 0;
    public bool IsDead = false;
    public bool isExitTouchPlayer = false;
    public bool isInExitLevel = false;

    public playerMainScript playerMainScript;

    public List<GameObject> enemyList; 

    public void pastRound() {
        round++;
        
        roundSystem.Invoke();
        playerMainScript.deadAliveCheck();
    }

    public void nextLevel() { // enter next level
        if (isExitTouchPlayer) {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, -2);
            playerDataBase.Static.currentFloor++;
            clearLevel();
            mapThingsGenerator.Static.StartGeneratorTheThings();
        }
    }

    public void clearLevel() {
        foreach (var item in GameObject.FindGameObjectsWithTag("item") ) {
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("enemy")) {
            Destroy(item);
        }
    }

    public void Update() {
        if (IsDead) {
            playerMainScript.GetComponent<chessMovement>().enabled = false;
            Debug.Log("lkdsalkjdalslk");
        }
        if (isExitTouchPlayer) {
            isExitTouchPlayer = false;
        }
    }

    public void Awake() {

        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }
        roundSystem += nextLevel;
        roundSystem += playerMainScript.subSP;
        roundSystem += playerMainScript.checkLife;
        roundSystem += playerMainScript.getItemSet;
    }

}
