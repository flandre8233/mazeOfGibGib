using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitScript : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") { //hit item
            roundScript.Static.isExitTouchPlayer = true;
            roundScript.Static.OnEnterNextLevel();
        }
    }

}
