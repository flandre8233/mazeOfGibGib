using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMovementSystem : MonoBehaviour {
    public Vector3 center;
    public groundScript CenterGround;
    public bool startLerpMovement = false;

    public groundScript getGround()
    {
        Collider[] hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), 0.25f);
        if (hitColliders.Length <= 0)
        {
            return null;
        }

        return hitColliders[0].GetComponent<groundScript>();
    }

    public Vector3 resetCenterV3(groundScript centerGround)
    {
        return new Vector3(Mathf.Round(centerGround.transform.position.x), Mathf.Round(centerGround.transform.position.y), 0);
    }

    public bool equalVector3(Vector3 v3a, Vector3 v3b)
    { // is ok
        v3a = roundTheVector3(v3a);
        v3b = roundTheVector3(v3b);
        return !(( v3a.x != v3b.x )  || (v3a.y != v3b.y) || (v3a.z != v3b.z) );
    }

    public Vector3 roundTheVector3(Vector3 v3)
    {
        return new Vector3(Mathf.Round(v3.x), Mathf.Round(v3.y), Mathf.Round(v3.z) );
    }

}
