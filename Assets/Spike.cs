using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : groundScript {
    public int perRoundShowUpSpike = 5;

    public bool inShowSpike ;
    /*
    bool InShowSpike {
        get {
            
            return inShowSpike; }
        set { inShowSpike = value; }
    }
    */

    public Transform spikeObjectTransform;


    private void Awake()
    {
        //Debug.Log(transform.GetComponentInChildren<Transform>().gameObject.GetComponentInChildren<Transform>().tag );

        foreach (Transform item in transform)
        {
            if (item.gameObject.tag == "spike")
            {
                spikeObjectTransform = item;
            }
        }

        //serializeSpike();
    }

    void serializeSpike()
    {
        if (inShowSpike)
        {
            spikeObjectTransform.localPosition = Vector3.zero;
        }
        else
        {
            spikeObjectTransform.localPosition = new Vector3(0,0,-0.5f);
        }
    }

}
