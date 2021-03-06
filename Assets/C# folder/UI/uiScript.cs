﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class uiScript : MonoBehaviour {
    public static uiScript Static;

    public RectTransform equipment_canvas;
    public RectTransform ability_canvas;
    public RectTransform equipment_canvas_off;
    public RectTransform ability_canvas_off;
    public Text AD_coin;


    [SerializeField]
    Text hpDisplayText;
    [SerializeField]
    Text spDisplayText;
    [SerializeField]
    Text currentFloor;
    [SerializeField]
    Image HPpic;
    [SerializeField]
    Image SPpic;

    float HPBar;
    float SPBar;



    void Awake()
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

        updatePlayerAtkDef();

        hpSpTextDisplay();



        currentFloor.text ="-Stage"+playerDataBase.Static.currentFloor+"-";


    }

    void hpSpTextDisplay()
    {
        hpDisplayText.text = "HP" + " " + playerDataBase.Static.HP + "/" + playerDataBase.Static.MaxHP ;
        spDisplayText.text = "SP" + " " + playerDataBase.Static.SP + "/" + playerDataBase.Static.MaxSP ;
    }

    public Text atkDisplay;
    public Text DefDisplay;
    void updatePlayerAtkDef()
    {
        atkDisplay.text = playerDataBase.Static.ATK.ToString();
        DefDisplay.text = playerDataBase.Static.DEF.ToString();
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

    public void canvas_equ()
    {
        if (equipment_canvas.gameObject.activeInHierarchy == false)
        {
            equipment_canvas.gameObject.SetActive(true);
        }
        else
        {
            equipment_canvas.gameObject.SetActive(false);
        }
    }

    public void canvas_abi()
    {
        if (ability_canvas.gameObject.activeSelf == false)
        {
            ability_canvas.gameObject.SetActive(true);
        }
        else
        {
            ability_canvas.gameObject.SetActive(false);
        }
    }

    public void canvas_equ_off()
    {
        if (equipment_canvas.gameObject.activeInHierarchy == true)
        {
            equipment_canvas.gameObject.SetActive(false);
        }
        else
        {
            equipment_canvas.gameObject.SetActive(true);
        }
    }

    public void canvas_abi_off()
    {
        if (ability_canvas.gameObject.activeSelf == true)
        {
            ability_canvas.gameObject.SetActive(false);
        }
        else
        {
            ability_canvas.gameObject.SetActive(true);
        }
    }
}
