using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class uiScript : MonoBehaviour {
    [SerializeField]
    Text testonlyText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        testonlyText.text = "HP : "+playerDataBase.Static.HP + " / " + playerDataBase.Static.MaxHP + "\n" +"SP : "+
            playerDataBase.Static.SP + " / " + playerDataBase.Static.MaxSP + "\n" + "COIN : " + playerDataBase.Static.COIN + "\n" + "currentFloor : " + playerDataBase.Static.currentFloor;


    }
}
