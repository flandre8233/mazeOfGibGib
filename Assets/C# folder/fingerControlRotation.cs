using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerControlRotation : MonoBehaviour
{
    public static fingerControlRotation Static;
    GameObject[] goArray;
    public GameObject cameraGameObject;
    public Transform miniCameraTransform;
    float onPressZAngle = 0.0f;
    float closestAngle = 0.0f;
    bool onpress = false;
    bool startLerpMovement = false;
    float startTime = 0.0f;
    // Use this for initialization
    void Start () {
        goArray = GameObject.FindGameObjectsWithTag("wall");
        hideWall();
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }
	}

    Vector2 testOne;
    Vector2 testTwo;

    float onPressFloat;

	// Update is called once per frame
	void Update () {
        LerpMove();
        miniCameraTransform.rotation = transform.rotation;
        /*
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width * 0.8)
        { // - 向上  + 向下
        }
        */
        if (Input.GetMouseButtonDown(0))
        {
            startLerpMovement = false;
            testOne = new Vector2(Input.mousePosition.x, 0);
            testTwo = new Vector2(0, Input.mousePosition.y);
            onPressFloat = (Vector2.Distance(testOne, Input.mousePosition) + Vector2.Distance(testTwo, Input.mousePosition) ) / 2;
            onPressZAngle = transform.rotation.eulerAngles.z;
            onpress = true;
        }

        if (Input.GetMouseButtonUp(0) ) {
            closestAngle = calibration(transform.rotation.eulerAngles.z);
            startLerpMovement = true;
            startTime = Time.time;
            onpress = false;
        }
        if (onpress) {
            hideWall();
            float dis = Vector2.Distance(testOne, Input.mousePosition);

            float dis2 = Vector2.Distance(testTwo, Input.mousePosition);

            transform.rotation = Quaternion.Euler(0, 0, onPressZAngle + (onPressFloat -((dis + dis2)/2) ) );

            //transform.rotation = Quaternion.Euler(0, 0,  onPressZAngle + (360 * holdHeightPrecent));
        }
	}
    
    int calibration(float number) {
        int[] array = { 360,90,180,270,0 };
        float[] DistanceArray = new float[array.Length];

        for (int i = 0; i < array.Length; i++) {
            DistanceArray[i] = Mathf.Abs(number - array[i]);
        }

        int smallestNumberIndex = 0;

        for (int i = 0; i < array.Length; i++) {
            if (i != smallestNumberIndex && DistanceArray[smallestNumberIndex] >DistanceArray[i] ) {
                smallestNumberIndex = i ;
            }
        }

        return array[smallestNumberIndex];
    }
    void LerpMove() {
        if (startLerpMovement) {
            transform.rotation = Quaternion.Euler(0, 0,  Mathf.Lerp(transform.rotation.eulerAngles.z, closestAngle, (Time.time - startTime) * 1.75f) );
            if (Mathf.Abs(transform.rotation.eulerAngles.z - closestAngle) <= 5.0f) {
                transform.rotation = Quaternion.Euler(0, 0, closestAngle);
                startLerpMovement = false;
            }

        }
    }

    void hideWall() {
        
        int smallestNumberIndex = 0;

        
        float[] DistanceArray = new float[goArray.Length];

        for (int i = 0; i < goArray.Length; i++) {
            DistanceArray[i] = Mathf.Abs(Vector3.Distance(goArray[i].transform.position, cameraGameObject.transform.position) );
            //Debug.Log(DistanceArray[i]);
        }
        

        for (int i = 0; i < goArray.Length; i++) {
            if (i != smallestNumberIndex && DistanceArray[smallestNumberIndex] > DistanceArray[i]) {
                smallestNumberIndex = i;
            }
        }

        for (int i = 0; i < goArray.Length; i++) {
            if (i == smallestNumberIndex) {
                goArray[i].SetActive(false);
            }
            else {
                goArray[i].SetActive(true);

            }
        }
    
    }

}
