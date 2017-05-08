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
        //Debug.Log(playerDataBase.Static.ATK);
        //Debug.Log(playerDataBase.Static.MaxHP);
        Ability_point.text = playerDataBase.Static.POINT + " Pt";
        data_level();
        data_now();
        data_next();
        //data_previous();
        //.Log(playerDataBase.Static.DEF);

        //data_next_pv();
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
            Debug.Log("yes");
            
        }
        else { Debug.Log("no");
        }
    }*/
    public void data_now()
    {
        Hpmax_now.text = playerDataBase.Static.MaxHPInitial + "";
        Spmax_now.text = playerDataBase.Static.MaxSPInitial + "";
        ATK_now.text = playerDataBase.Static.ATKInitial + "";
        DEF_now.text = playerDataBase.Static.DEFInitial + "";
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
        Hpmax_result.text = playerDataBase.Static.MaxHP + "";
        Spmax_result.text = playerDataBase.Static.MaxSP + "";
        ATK_result.text = playerDataBase.Static.ATK + "";
        DEF_result.text = playerDataBase.Static.DEF + "";
    }
}
