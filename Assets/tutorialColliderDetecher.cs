using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialColliderDetecher : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (tutorial.Static == null)
            {
                return;
            }

            tutorial.Static.turnOnTutorial();

        }

    }
}
