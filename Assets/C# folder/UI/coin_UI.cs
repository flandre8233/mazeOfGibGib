using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coin_UI : MonoBehaviour {
    public Text Text_coin;
    public Text Text_equ_coin;
    void Start () {
        coinUI();
        coinUI_equ();
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

    void coinUI_equ()
    {
        Text_equ_coin.text = playerDataBase.Static.COIN.ToString();
    }
}
