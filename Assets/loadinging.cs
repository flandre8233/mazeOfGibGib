using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadinging : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void end_level_pass_ani()
    {
         loadingTest.Static.level_pass.gameObject.SetActive(false);
    }
}
