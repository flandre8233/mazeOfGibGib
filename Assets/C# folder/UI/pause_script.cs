﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_script : MonoBehaviour {

    public Animator pause_animator;
    public RectTransform pause_background;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pause_menu_popout()
    {
        if (pause_animator.GetBool("pause_bool")==false)
        {
            //Debug.Log(pause_animator.GetBool("pause_bool"));
            pause_animator.SetBool("pause_bool", true);
            pause_background.gameObject.SetActive(true);
        }
        else
        {
            pause_animator.SetBool("pause_bool", false);
            pause_background.gameObject.SetActive(false);
        }

    }
}