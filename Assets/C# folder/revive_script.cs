using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class revive_script : MonoBehaviour {
    public static revive_script Static;

    public RectTransform crystal_on;
    public RectTransform crystal_off;
    public GameObject crystal_particle_on;
    public GameObject crystal_particle_off;
    public RectTransform menu_crystal;
    public RectTransform yn_show;

    public Text priceText;

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

    void Update () {
	}

    public void crystal_menu()
    {
        menu_crystal.gameObject.SetActive( !menu_crystal.gameObject.activeSelf);

        roundScript.Static.popUpMenuChecker = false;
    }

    public Button revive_Button;
    public void yn_menu()
    {
        yn_show.gameObject.SetActive(!yn_show.gameObject.activeSelf);
        if (playerDataBase.Static.COIN - playerDataBase.Static.revivCost >= 0)
        {
            revive_Button.interactable = true;
        }
        else
        {
            revive_Button.interactable = false;
        }
        priceText.text = playerDataBase.Static.revivCost.ToString();
    }

    public void no_menu()
    {
        yn_show.gameObject.SetActive(false);
    }
    public void crystal_menu_press()
    {
        menu_crystal.gameObject.SetActive(!menu_crystal.gameObject.activeSelf);
        roundScript.Static.popUpMenuChecker = false;
        //yn_show.gameObject.SetActive(!yn_show.gameObject.activeSelf);
    }

    public void revive_button()
    {
        if (playerDataBase.Static.revive_value == false && playerDataBase.Static.COIN - playerDataBase.Static.revivCost >= 0)
        {
            playerDataBase.Static.COIN -= playerDataBase.Static.revivCost;
            //here

            playerDataBase.Static.reviveTimes++;
            playerDataBase.Static.revive_value = true;
            crystal_particle_off.gameObject.SetActive(false);
            crystal_on.gameObject.SetActive(true);
            crystal_particle_on.gameObject.SetActive(true);
            crystal_off.gameObject.SetActive(false);
            yn_show.gameObject.SetActive(false);
            soundEffectManager.staticSoundEffect.play_crystal_light();
        }
        /*else
        {
            playerDataBase.Static.revive_value = false;
            crystal_on.gameObject.SetActive(false);
            crystal_particle_on.gameObject.SetActive(false);
            crystal_off.gameObject.SetActive(true);
            crystal_particle_off.gameObject.SetActive(true);
        }*/
    }
}
