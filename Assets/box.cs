using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Quaternion ImageLookAt2D(Vector3 from, Vector3 to)
    {
        Vector3 difference = to - from;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = (Quaternion.Euler(0.0f, 0.0f, rotationZ));
        return rotation;
    }

    public void allwayFaceAtPlayer()
    {
        float Angle = ImageLookAt2D(transform.position, chessMovement.Static.transform.position).eulerAngles.z;
        transform.rotation = ImageLookAt2D(transform.position, chessMovement.Static.transform.position);
        //transform.LookAt(playerTransform);
    }

}
