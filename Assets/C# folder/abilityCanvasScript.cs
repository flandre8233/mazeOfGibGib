using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityCanvasScript : MonoBehaviour {
    public Text ATK;
    public Text Hpmax;
    public Text Spmax;
    public Text Coin;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(playerDataBase.Static.ATK);
        ATK.text = "ATK+" + playerDataBase.Static.ATK;
        Hpmax.text = "HPmax:" + playerDataBase.Static.MaxHP;
        Spmax.text = "Spmax:" + playerDataBase.Static.MaxSP;
        Coin.text = "Coin+" + playerDataBase.Static.COIN;
    }
}
