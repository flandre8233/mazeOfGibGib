 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleManager : MonoBehaviour {
    public static particleManager Static;


    public GameObject character_item_attack;
    public GameObject character_item_defense;
    public GameObject character_item_food;
    public GameObject character_item_foodtime;
    public GameObject character_run_inside01_heal;
    public GameObject character_run_inside01_hurt;
    public GameObject character_run_inside01_normal;
    public GameObject Hpmax_up;
    public GameObject Spmax_up;
    public GameObject attack01;
    public GameObject attack02;
    public GameObject attack03;
    public GameObject attack04;
    //public GameObject pass_particle;
    public GameObject pass_particle;
    public GameObject click_button_up;
    public GameObject click_button_down;
    public GameObject click_pass;
    public GameObject click_pause;



    private void Awake()
    {
        if (Static!=null)
        {
            Destroy(this);
        }
        else
        {
            Static = this;
        }
    }
}
