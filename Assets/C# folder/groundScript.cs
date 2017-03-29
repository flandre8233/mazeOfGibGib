using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum groundType
{
    canSpawnThings,
    canNOTSpawnThings,
    startPoint,
    ExitGoalPoint,
    isPortFloor,
    isPortExitFloor
}

public class groundScript : MonoBehaviour {
    public int TerrainUID;
    public int passCount;
    public bool haveSomethingInHere;
    public groundType type;

    public bool delByMapLimit;
    public GameObject myParent;

    /*
    public bool canSpawnThings;
    public bool startPoint;
    public bool ExitGoalPoint;

    public bool isPortFloor = false;
    public bool isPortExitFloor = false;
    */

    void Start() {
        //mapTerrainGenerator.Static.thisLevelAllFloor.Add(gameObject);
    }

    void OnDestroy() {
        
    }

    public bool isDeadEnd() {
        Vector3 left = transform.position + Vector3.left;
        Vector3 right = transform.position + Vector3.right;
        Vector3 up = transform.position + Vector3.up;
        Vector3 down = transform.position + Vector3.down;
        //int passCount = 0;
        if (Physics.OverlapSphere(left, 0.5f).Length > 0) {
            passCount++;
        }
        if (Physics.OverlapSphere(right, 0.5f).Length > 0) {
            passCount++;
        }
        if (Physics.OverlapSphere(up, 0.5f).Length > 0) {
            passCount++;
        }
        if (Physics.OverlapSphere(down, 0.5f).Length > 0) {
            passCount++;
        }

        if (passCount == 1) {
            return true;
        }
        return false;
        
    }

	// Update is called once per frame
	void Update () {
        Vector3 hitPoint = new Vector3(transform.position.x,transform.position.y,-1);
        Collider[] hitColliders = Physics.OverlapSphere(hitPoint, 0.25f);
        if (hitColliders.Length != 0 && Vector2.Distance( ((Vector2)hitColliders[0].transform.position), ((Vector2)transform.position)) <= 0.2f) {
            haveSomethingInHere = true;
        }
        else {
            haveSomethingInHere = false;
        }
        
        if (GetComponent<groundScript>().delByMapLimit) {
            if (
            mapTerrainGenerator.Static.thisLevelAllFloor.Remove(gameObject)) {
                mapTerrainGenerator.Static.thisLevelAllFloor.Remove(gameObject);
            }
            Destroy(gameObject);
        }
        

        /*
        if (myParent != null) {
            if (myParent.GetComponent<groundScript>().delByMapLimit) {
                Destroy(gameObject);
            }
        }
        */
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "floor") {
            if (TerrainUID < other.gameObject.GetComponent<groundScript>().TerrainUID && !GetComponent<groundScript>().delByMapLimit) {

                mapTerrainGenerator.Static.thisLevelAllFloor.Remove(other.gameObject);
                Destroy(other.gameObject);

            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("SADSAD");
    }

}
