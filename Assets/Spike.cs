using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : groundScript {
    public int perRoundShowUpSpike = 5;

    private void Start()
    {

        Debug.Log(  roundScript.Static.round  + "  "+roundScript.Static.round % perRoundShowUpSpike);
    }
}
