using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coin_UI : MonoBehaviour {
    public static coin_UI Static;

    public Text Text_coin;
    public Text Text_equ_coin;
    public Text Text_shop_coin;

    public int displayCoin;

    public float lerpSpeed = 1;

    public float timeLerp;

    private void Awake()
    {
        if (Static != null)
        {
            Destroy(this);
        }
        else
        {
            Static = this;
        }

    }

    void Start () {
        timeLerp = Time.time;
        //coinUI_equ();
    }

    int LerpMove( float startTime, float lerpSpeed)
    {
        return  (int)Mathf.Lerp ( displayCoin , playerDataBase.Static.COIN , (Time.time - startTime) * lerpSpeed);
    }
    void Update () {
        coinUI();

        displayCoin = LerpMove(timeLerp,lerpSpeed);

        //Text_coin.text = x.ToString();

    }

    public void updateCoinDisplay()
    {
        timeLerp = Time.time;
    }

    void OnTriggerEnter(Collider other)
    {
        //coinUI();
    }

    public void coinUI()
    {
        Text_coin.text = displayCoin.ToString();
        Text_equ_coin.text = displayCoin.ToString();
        //Text_shop_coin.text = playerDataBase.Static.COIN.ToString();
    }

}
