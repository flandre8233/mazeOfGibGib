using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum groundType
{
    canSpawnThings,
    startPoint,
    ExitGoalPoint,
    isPortFloor,
    isPortExitFloor
}

public class groundScript : MonoBehaviour {
    public int TerrainUID;
    public bool haveSomethingInHere;
    public groundType type;

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
        mapTerrainGenerator.Static.thisLevelAllFloor.Remove(gameObject);
    }

	// Update is called once per frame
	void Update () {
        Vector3 hitPoint = new Vector3(transform.position.x,transform.position.y,-2);
        Collider[] hitColliders = Physics.OverlapSphere(hitPoint, 0.25f);
        if (hitColliders.Length != 0 && Vector2.Distance( ((Vector2)hitColliders[0].transform.position), ((Vector2)transform.position)) <= 0.2f) {
            haveSomethingInHere = true;
        }
        else {
            haveSomethingInHere = false;
        }

    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "floor") {
            if (TerrainUID > other.gameObject.GetComponent<groundScript>().TerrainUID) {
                Destroy(other.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("SADSAD");
    }

}
