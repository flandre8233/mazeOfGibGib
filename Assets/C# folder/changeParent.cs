using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeParent : MonoBehaviour {

    [SerializeField]
    GameObject go1;
    [SerializeField]
    GameObject go2;

	// Use this for initialization
	void Start () {
        transform.parent = go1.transform; //change object parent
        transform.localPosition = Vector3.zero;
        transform.localPosition = new Vector3(0,0,-1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
