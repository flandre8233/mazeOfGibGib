using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class max_lv_ui : MonoBehaviour {

    [SerializeField]
    Text maxLevel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        maxLevel.text = "Best Level :" + playerDataBase.Static.maxFloor;
    }
}
