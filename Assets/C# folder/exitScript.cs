using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitScript : MonoBehaviour {
    public RectTransform leve_pass;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") { //hit item
            roundScript.Static.isExitTouchPlayer = true;
            leve_pass.gameObject.SetActive(!leve_pass.gameObject.activeSelf);
        }
    }

}
