using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour {
    public static gamemanager Static;
    public GameObject damageDisplayObject;

    public void spawnNumberDisplay(Vector3 where,int number ,int type) {
        GameObject go = Instantiate(damageDisplayObject, where, Quaternion.identity);
        go.GetComponent<damageDisplay>().spawnDamageDisplay(number,type);
    }

	// Use this for initialization
	void Start () {
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }

	}

}
