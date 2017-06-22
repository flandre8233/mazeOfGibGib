using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockRotation : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (canvasButton.Static.bigMapCamera.activeSelf) { 
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, fingerControlRotation.Static.cameraGameObject.transform.rotation.eulerAngles.z);
        }
	}
}
