using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerControlRotation : MonoBehaviour {
    public static fingerControlRotation Static;
    float onPressPrecent = 0.0f;
    float onPressZAngle = 0.0f;
    float closestAngle = 0.0f;
    bool onpress = false;
    bool startLerpMovement = false;
    float startTime = 0.0f;
    // Use this for initialization
    void Start () {
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }
	}

    int i = 0;

	// Update is called once per frame
	void Update () {
        LerpMove();
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        float precent = Input.mousePosition.y / Screen.height;
        if (Input.GetMouseButtonDown(0)) { // - 向上  + 向下
            startLerpMovement = false;
            onPressPrecent = Input.mousePosition.y / Screen.height;
            onPressZAngle = transform.rotation.eulerAngles.z;
            onpress = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            closestAngle = calibration(transform.rotation.eulerAngles.z);
            startLerpMovement = true;
            startTime = Time.time;
            Debug.Log(transform.rotation.eulerAngles.z);
            onPressPrecent = 0.0f;
            onpress = false;
        }
        i++;
        if (onpress) {
            float holdHeightPrecent = onPressPrecent - precent;

            transform.rotation = Quaternion.Euler(0, 0,  onPressZAngle + (360 * holdHeightPrecent));

        }
	}
    
    int calibration(float number) {
        int[] array = { 360,90,180,270,0 };
        float[] DistanceArray = new float[array.Length];

        for (int i = 0; i < array.Length; i++) {
            DistanceArray[i] = Mathf.Abs(number - array[i]);
            //Debug.Log(DistanceArray[i]);
        }

        int smallestNumberIndex = 0;

        for (int i = 0; i < array.Length; i++) {
            if (i != smallestNumberIndex && DistanceArray[smallestNumberIndex] >DistanceArray[i] ) {
                smallestNumberIndex = i ;
            }
        }
        //Debug.Log(smallestNumberIndex + "gg");

        return array[smallestNumberIndex];
    }

    void LerpMove() {
        if (startLerpMovement) {
            transform.rotation = Quaternion.Euler(0, 0,  Mathf.Lerp(transform.rotation.eulerAngles.z, closestAngle, (Time.time - startTime) * 1.75f) );
            if (Mathf.Abs(transform.rotation.eulerAngles.z - closestAngle) <= 5.0f) {
                transform.rotation = Quaternion.Euler(0, 0, closestAngle);
                startLerpMovement = false;
            }
            /*
            else if (Mathf.Abs(Vector3.Distance(transform.position, hitObjectPosition)) <= 0.1f) {
                roundScript.Static.movementProcessingChecker = false;
            }
            */

        }
    }


}
