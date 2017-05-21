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
        mouseDown = true;
        chessMovement.Static.isInAutoMovement = mouseDown;

        chessMovement.Static.charactor_move.SetBool("run", true);
        chessMovement.Static.charactor_move.SetBool("idle", false);
    }
    public void OnPointerUp(PointerEventData eventData) {
        mouseDown = false;
        chessMovement.Static.isInAutoMovement = mouseDown;
        timeMouseDown = 0;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (mouseDown && !roundScript.Static.isInExitLevel) {
            timeMouseDown += Time.deltaTime;
            chessMovement.Static.autoMovement(timeMouseDown, dir);
        }

    }
}
