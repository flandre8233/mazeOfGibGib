using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class buttonHold : MonoBehaviour , IPointerDownHandler
, IPointerUpHandler
{

    public bool mouseDown = false;
    float timeMouseDown = 0.0f;
    public string dir = "";

    private void Awake()
    {

    }

    public void OnPointerDown(PointerEventData eventData) {
        if (chessMovement.Static.inOpenChest)
        {
            mouseDown = false;
            return;
        }
        mouseDown = true;
        chessMovement.Static.isInAutoMovement = mouseDown;

        chessMovement.Static.faceDirection = dir;
        chessMovement.Static.MovementPart(dir);

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
            if (chessMovement.Static.inOpenChest)
            {
                return;
            }

            timeMouseDown += Time.deltaTime;

            chessMovement.Static.faceDirection = dir;
            chessMovement.Static.autoMovement(timeMouseDown, dir);
        }

    }
}
