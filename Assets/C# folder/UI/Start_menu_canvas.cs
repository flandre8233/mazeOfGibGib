using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_menu_canvas : MonoBehaviour {
    public static Start_menu_canvas Static;
    public RectTransform start_menu;

	// Use this for initialization
	void Start () {
        if (playerDataBase.Static.check_start == true)
        {
            start_menu.gameObject.SetActive(true);
        }
        else
        {
            start_menu.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
    
    }

    public void Start_exit()
    {
        start_menu.gameObject.SetActive(false);
    }
}
