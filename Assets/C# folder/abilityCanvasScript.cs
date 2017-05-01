using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityCanvasScript : MonoBehaviour {
    public Text Ability_point;
    public Text ATK_level;
    public Text Hpmax_level;
    public Text Spmax_level;
    public Text DEF_level;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(playerDataBase.Static.ATK);
        Ability_point.text = playerDataBase.Static.ability_point + " Pt";
        ATK_level.text = "Level " + playerDataBase.Static.ATKLevel;
        Hpmax_level.text = "Level " + playerDataBase.Static.HpmaxLevel;
        Spmax_level.text ="Level " + playerDataBase.Static.SpmaxLevel;
        DEF_level.text = "Level " + playerDataBase.Static.DEFLevel;
    }
}
