using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coin_UI : MonoBehaviour {
    public Text Text_coin;
    void Start () {
        coinUI();
    }
	
	void Update () {
        coinUI();
        //Text_coin.text = x.ToString();

    }

    void OnTriggerEnter(Collider other)
    {
        //coinUI();
    }

    void coinUI()
    {
        Text_coin.text = playerDataBase.Static.COIN.ToString();
    }
}
