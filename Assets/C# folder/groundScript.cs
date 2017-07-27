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
     public delegate void delegateFunction ();
    public delegateFunction UpdataSystem;


    public int TerrainUID;
    public int passCount;
    public bool haveSomethingInHere;
    public GameObject haveSomethingInHereObject;
    public groundType type;

    public bool delByMapLimit;
    public GameObject myParent;

    public bool alreadyLink;

    public pathDirection pathdirection;
    public bool alreadyFindAllNeighbor;

    public bool isSpike = false;

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


    public Vector3 passV3;

    public bool isDeadEnd() {
        Vector3 left = transform.position + Vector3.left;
        Vector3 right = transform.position + Vector3.right;
        Vector3 up = transform.position + Vector3.up;
        Vector3 down = transform.position + Vector3.down;
        //int passCount = 0;
        if (Physics.OverlapSphere(left, 0.5f).Length > 0) {
            passV3 = left;
            passCount++;
        }
        if (Physics.OverlapSphere(right, 0.5f).Length > 0) {
            passV3 = right;
            passCount++;
        }
        if (Physics.OverlapSphere(up, 0.5f).Length > 0) {
            passV3 = up;
            passCount++;
        }
        if (Physics.OverlapSphere(down, 0.5f).Length > 0) {
            passV3 = down;
            passCount++;
        }

        if (passCount == 1) {
            return true;
        }
        passV3 = Vector3.zero;
        return false;
        
    }

	// Update is called once per frame
	void Update () {
        //groundCollidersCheck(); <-改

        /*
        if (GetComponent<groundScript>().delByMapLimit) {
            if (
            mapTerrainGenerator.Static.thisLevelAllFloor.Remove(gameObject)) {
                mapTerrainGenerator.Static.thisLevelAllFloor.Remove(gameObject);
            }
            Destroy(gameObject);
        }
        */

        if (UpdataSystem != null)
        {
            UpdataSystem.Invoke();
        }
    }

    public void roundSystemUseOnly() {
        groundCollidersCheck();

        ////Debug.Log( "diu?");
    }


    public bool equalVector2(Vector2 v2a, Vector3 v2b)
    { // is ok
        v2a = roundTheVector2(v2a);
        v2b = roundTheVector2(v2b);
        return !((v2a.x != v2b.x) || (v2a.y != v2b.y) );
    }

    public Vector2 roundTheVector2(Vector3 v2)
    {
        return new Vector3(Mathf.Round(v2.x), Mathf.Round(v2.y) );
    }

    public void groundCollidersCheck() //spike not work but normal is work
    {
        ////Debug.Log(gameObject.name);
        Vector3 hitPoint = new Vector3(transform.position.x, transform.position.y, -1f); //問題
        Collider[] hitColliders = Physics.OverlapSphere(hitPoint, 0.5f);

        ////Debug.Log(hitColliders.Length +  "diu");

        if (hitColliders.Length != 0 )
        {
            foreach (var item in hitColliders)
            {
                if (equalVector2(item.transform.position,transform.position) )
                {
                    haveSomethingInHere = true;
                    haveSomethingInHereObject = item.gameObject;
                    return;
                }

                haveSomethingInHere = false;
                haveSomethingInHereObject = null;
                return;
            }

        }

        haveSomethingInHere = false;
        haveSomethingInHereObject = null;
        return;


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
        //Debug.Log("SADSAD");
    }

    public virtual void OnDestroy()
    {
            roundScript.Static.groundCheckSystem -= roundSystemUseOnly;
    }

}
