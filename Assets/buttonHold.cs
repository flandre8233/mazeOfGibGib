﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class buttonHold : MonoBehaviour , IPointerDownHandler
, IPointerUpHandler
{
    bool mouseDown = false;
    float timeMouseDown = 0.0f;
    public string dir = "";

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("hitDOWNhit");
        mouseDown = true;
    }
    public void OnPointerUp(PointerEventData eventData) {
        Debug.Log("hitUPhit");
        mouseDown = false;
        Debug.Log(timeMouseDown);
        timeMouseDown = 0;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (mouseDown) {
            timeMouseDown += Time.deltaTime;
            chessMovement.Static.autoMovement(timeMouseDown, dir);
        }

    }
}
