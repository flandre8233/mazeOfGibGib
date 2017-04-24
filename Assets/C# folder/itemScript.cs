using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    public string itemName { get; set; }
    public int level = 1;
    public int AddHP { get; set; }
    public int AddSP { get; set; }
    public int AddHPMax { get; set; }
    public int AddSPMax { get; set; }
    public int AddCOIN { get; set; }
    public int AddATK { get; set; }
    public int AddDEF { get; set; }
    public int SPNoCostTime { get; set; }
    public int continueRound { get; set; }

    void Start() {
        SetUp();
        includeLevelSetUp();
    }

    public virtual void SetUp() {
        AddHP = 0;
        AddSP = 0;
        AddHPMax = 0;
        AddSPMax = 0;
        AddCOIN = 0;
    }

    public virtual void includeLevelSetUp() {
    }

}
