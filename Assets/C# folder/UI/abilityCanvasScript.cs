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

    public Text Hpmax_now;
    public Text Spmax_now;
    public Text ATK_now;
    public Text DEF_now;

    public Button Hpmax_Button;
    public Button Spmax_Button;
    public Button ATK_Button;
    public Button DEF_Button;

    /*public Text Hpmax_next_pv;
    public Text Spmax_next_pv;
    public Text ATK_next_pv;
    public Text DEF_next_pv;*/

    public int Hpmax_next = 3;
    public int Spmax_next = 5;
    public int ATK_next = 20;
    public int DEF_next = 20;

    public Text Hpmax_result;
    public Text Spmax_result;
    public Text ATK_result;
    public Text DEF_result;

    void Update () {
        Ability_point.text = playerDataBase.Static.POINT + " Pt";
        data_level();
        data_now();
        data_next();
        checkButtonActive();
        //data_previous();
        //.Log(playerDataBase.Static.DEF);

        //data_next_pv();
    }

    void checkButtonActive()
    {
        if (playerDataBase.Static.POINT >= 1)
        {
            Hpmax_Button.interactable = true;
            Spmax_Button.interactable = true;
            ATK_Button.interactable = true;
            DEF_Button.interactable = true;
        }
        else
        {
            Hpmax_Button.interactable = false;
            Spmax_Button.interactable = false;
            ATK_Button.interactable = false;
            DEF_Button.interactable = false;
        }
    }

    public void data_level()
    {
        ATK_level.text = "Level " + playerDataBase.Static.ATKlevelpercent;
        Hpmax_level.text = "Level " + playerDataBase.Static.HpmaxLevel;
        Spmax_level.text ="Level " + playerDataBase.Static.SpmaxLevel;
        DEF_level.text = "Level " + playerDataBase.Static.DEFlevelpercent;
    }
    /*public void data_previous()
    {
        if (playerDataBase.Static.MaxHPInitial != playerDataBase.Static.MaxHP)
        {
            //Debug.Log("yes");
            
        }
        else { //Debug.Log("no");
        }
    }*/
    public void data_now()
    {
        Hpmax_now.text = playerDataBase.Static.MaxHP + "";
        Spmax_now.text = playerDataBase.Static.MaxSP + "";
        ATK_now.text = playerDataBase.Static.ATK + "";
        DEF_now.text = playerDataBase.Static.DEF + "";
    }

    /*public void data_next_pv()
    {
        Hpmax_next_pv.text = playerDataBase.Static.MaxHPInitial + Hpmax_next+"";
        Spmax_next_pv.text = playerDataBase.Static.MaxSPInitial + Spmax_next + "";
        ATK_next_pv.text = playerDataBase.Static.ATKInitial + ATK_next + "";
        DEF_next_pv.text = playerDataBase.Static.DEFInitial + DEF_next + "";
    }*/

    public void data_next()
    {
        
        Hpmax_result.text = (playerDataBase.Static.MaxHP+ playerDataBase.Static.HPAbility) + "";
        Spmax_result.text = (playerDataBase.Static.MaxSP + playerDataBase.Static.SPAbility) + "";

        int nextATK = ((int)((1 + playerDataBase.Static.ATKLevel) / 100f * (((playerDataBase.Static.ATKlevelpercent + 1) * 20) + 100)));
        int nextDEF = ((int)((1 + playerDataBase.Static.DEFLevel) / 100f * (( (playerDataBase.Static.DEFlevelpercent+1) * 20) + 100) ))  ;
        ////Debug.Log(playerDataBase.Static.ATKlevelpercent + "   " + playerDataBase.Static.ATKLevel);
        ATK_result.text = (nextATK) + "";
        DEF_result.text = (nextDEF) + "";
    }
}
