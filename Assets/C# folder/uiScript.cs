using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class uiScript : MonoBehaviour {
    [SerializeField]
    Text testonlyText;

    void Awake() {
    }

    // Use this for initialization
    void Start () {

	}

    public int addAllNumber(int startNumber, int number) {
        int y = 0;
        for (int i = startNumber; i <= number; i++) {
            y += i;
        }
        return y;
        
    }

    // Update is called once per frame
    void Update () {
        testonlyText.text = "HP : " + playerDataBase.Static.HP + " / " + playerDataBase.Static.MaxHP + "\n" + "SP : " +
            playerDataBase.Static.SP + " / " + playerDataBase.Static.MaxSP + "\n" + "COIN : " + playerDataBase.Static.COIN + "\n" + "currentFloor : " +
            playerDataBase.Static.currentFloor + "\n" ;


    }
}
