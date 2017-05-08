using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxChange : MonoBehaviour {
    public Material box2;


	// Use this for initialization
	void Start () {
        RenderSettings.skybox = box2;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
