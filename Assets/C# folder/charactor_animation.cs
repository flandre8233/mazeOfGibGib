using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactor_animation : MonoBehaviour {
    public Animator charactor_move;
    private bool run_trigger;
	// Use this for initialization
	void Start () {
        charactor_move = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            //charactor_move.Play("run", -1, 0f);
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            charactor_move.Play("attack", -1, 0f);
        }
        /*if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            run_trigger = true;
            //charactor_move.Play("run",-1,0f); 
        }
        else
        {
            run_trigger = false;
        }

        charactor_move.
        ("run", run_trigger);*/
    }
}
