using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class uiScript : MonoBehaviour {
    [SerializeField]
    Text testonlyText;
    [SerializeField]
    Text currentFloor;
    [SerializeField]
    Image HPpic;
    [SerializeField]
    Image SPpic;

    float HPBar;
    float SPBar;



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
        HPBAR_script();
        SPBAR_script();


        testonlyText.text = "HP : " + playerDataBase.Static.HP + " / " + playerDataBase.Static.MaxHP + "\n" + "SP : " +
            playerDataBase.Static.SP + " / " + playerDataBase.Static.MaxSP;
        //+
        //    playerDataBase.Static.currentFloor + "\n" ;

        currentFloor.text ="-Stage"+playerDataBase.Static.currentFloor+"-";


    }

    void HPBAR_script()
    {
        HPBar = (1.0f / playerDataBase.Static.MaxHP) * playerDataBase.Static.HP;
        HPpic.fillAmount = Mathf.Lerp(HPpic.fillAmount, HPBar, 0.05f);
    }

    void SPBAR_script()
    {
        SPBar = (1.0f / playerDataBase.Static.MaxSP) * playerDataBase.Static.SP;
        SPpic.fillAmount = Mathf.Lerp(SPpic.fillAmount, SPBar, 0.05f);
    }
}
