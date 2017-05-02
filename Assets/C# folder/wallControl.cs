using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallControl : MonoBehaviour {
    public static wallControl Static; 

    public Transform floorBackground;

    public Transform upWall;
    public Transform leftWall;
    public Transform rightWall;
    public Transform downWall;

    public void syncBackgroundSize(int mapWidth,int mapHeight) {
        upWall.transform.position = new Vector3(0, mapHeight + 2, 5);
        leftWall.transform.position = new Vector3( -(mapWidth + 2), 0, 5);
        rightWall.transform.position = new Vector3(mapWidth +2, 0, 5);
        downWall.transform.position = new Vector3(0,  -(mapHeight + 2), 5);

        upWall.localScale = new Vector3(2*(mapHeight + 1), 1, 20);
        leftWall.localScale = new Vector3(2*(mapWidth + 1), 1, 20);
        rightWall.localScale = new Vector3(2*(mapWidth + 1), 1, 20);
        downWall.localScale = new Vector3(2*(mapHeight + 1), 1, 20);

        floorBackground.localScale = new Vector3(2 * (mapWidth + 2), 4 * (mapHeight + 2), 1); ;

    }
    
    void Awake () {
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
