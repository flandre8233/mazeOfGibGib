using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType
{
    HP,
    SP,
    SPNoCost,
    ATK,
    DEF,
    HPMAX,
    SPMAX,
    COIN
};

public class itemScript : MonoBehaviour
{

    //public string itemName { get; set; }
    public int level { get; set; }
    public itemType type;
    protected int addHP;
    protected int addSP;
    protected int addHPMax;
    protected int addSPMax;
    protected int addCOIN;
    protected int addATK;
    protected int addDEF;
    protected int sPNoCostTime;
    protected int continueRound;

#region item
    public virtual int AddHP {
        get {
            return addHP;
        }
    }
    public virtual int AddSP {
        get {
            return addSP;
        }
    }
    public virtual int AddHPMax {
        get {
            return addHPMax;
        }
    }
    public virtual int AddSPMax {
        get {
            return addSPMax;
        }
    }
    public virtual int AddCOIN {
        get {
            return addCOIN;
        }
    }
    public virtual int AddATK {
        get {
            return addATK;
        }
    }
    public virtual int AddDEF {
        get {
            return addDEF;
        }
    }
    public virtual int SPNoCostTime {
        get {
            return sPNoCostTime;
        }
    }
    public virtual int ContinueRound {
        get {
            return continueRound;
        }
    }
#endregion

    void Start() {
        level = 1;
        SetUp();
    }

    public virtual void SetUp() {

    }


}
