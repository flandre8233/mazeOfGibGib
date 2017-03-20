using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundScript : MonoBehaviour {
    public int UID;
    public bool haveSomethingInHere;
    public bool canSpawnThings;
    public bool startPoint;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 hitPoint = new Vector3(transform.position.x,transform.position.y,-2);
        Collider[] hitColliders = Physics.OverlapSphere(hitPoint, 0.25f);
        if (hitColliders.Length != 0) {
            haveSomethingInHere = true;
        }
        else {
            haveSomethingInHere = false;
        }

    }

    void OnTriggerEnter(Collider other) {
    }

}
