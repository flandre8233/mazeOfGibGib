using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcSensor : MonoBehaviour {
    public bool isFindPlayer = false;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            isFindPlayer = true;
        }

    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            isFindPlayer = false;
        }

    }

    void OnDestroy() {
        isFindPlayer = false;
    }

}
