using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMainScript : MonoBehaviour {
    

	// Use this for initialization
	void Awake () {
	}

    public void subSP() {
        if (playerDataBase.Static.SP > 0 && !roundScript.Static.isExitTouchPlayer) {
            playerDataBase.Static.SP--;
        }
    }



    public void checkLife() {
        if (!roundScript.Static.isExitTouchPlayer) {
            if (playerDataBase.Static.SP > 0) {
                if (playerDataBase.Static.HP < playerDataBase.Static.MaxHP) {
                    if ( (int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 5.0f)) > 1) {
                        playerDataBase.Static.HP += (int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 5.0f));
                    }
                    else {
                        playerDataBase.Static.HP++;
                    }
                }
            }
            else {
                if (playerDataBase.Static.HP > 0) {
                    if ((int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 10.0f)) > 1) {
                        playerDataBase.Static.HP -= (int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 10.0f));
                    }
                    else {
                        playerDataBase.Static.HP--;
                    }
                }
            }
        }

        //deadAliveCheck();

    }

    public void OnPlayerDead() {
        if (playerDataBase.Static.maxFloor < playerDataBase.Static.currentFloor) {
            playerDataBase.Static.maxFloor = playerDataBase.Static.currentFloor;
        }

    }

    public void deadAliveCheck() {
        /*
        if (playerDataBase.Static.HP <= 0) {
            if (playerDataBase.Static.SP <= 0) {

            }
        }
        */

        if (playerDataBase.Static.HP <= 0 ) {
            //event : hp = 0  gameover
            playerDataBase.Static.HP = 0;
            roundScript.Static.IsDead = true;
            OnPlayerDead();
            //GetComponent<chessMovement>().enabled = false; //youdead
        } 
        else {
            roundScript.Static.IsDead = false;
            //GetComponent<chessMovement>().enabled = true;
        }

        if (playerDataBase.Static.SP == 0) {
            //event : sp = 0
        }
    }

    public void getItemSet() {
        if (hitItem != null) {
            playerDataBase.Static.HP += hitItem.gameObject.GetComponent<itemScript>().AddHP;
            playerDataBase.Static.SP += hitItem.gameObject.GetComponent<itemScript>().AddSP;
            playerDataBase.Static.MaxHP += hitItem.gameObject.GetComponent<itemScript>().AddHPMax;
            playerDataBase.Static.MaxSP += hitItem.gameObject.GetComponent<itemScript>().AddSPMax;
            playerDataBase.Static.COIN += hitItem.gameObject.GetComponent<itemScript>().AddCOIN;

            if (playerDataBase.Static.HP >= playerDataBase.Static.MaxHP) { //max check
                playerDataBase.Static.HP = playerDataBase.Static.MaxHP;
            }
            if (playerDataBase.Static.SP >= playerDataBase.Static.MaxSP) {
                playerDataBase.Static.SP = playerDataBase.Static.MaxSP;
            }
            Destroy(hitItem);
            hitItem = null;
        }

    }

    public GameObject hitItem;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "item") { //hit item
            hitItem = other.gameObject;
        }
    }

}
