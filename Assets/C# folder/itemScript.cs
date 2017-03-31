using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType
{
    hpItem,
    spItem,
    hpmaxItem,
    spmaxItem,
    coinItem
};

public class itemScript : MonoBehaviour
{
    public bool IsAutoSetType = true;
    public itemType ItemType;

    public int AddHP { get; set; }
    public int AddSP { get; set; }
    public int AddHPMax { get; set; }
    public int AddSPMax { get; set; }
    public int AddCOIN { get; set; }

    void Start() {
        setItemType();
    }

    public void setItemType() {
        if (IsAutoSetType) {
            ItemType = selectType();
        }

    }

    public itemType selectType() {
        switch (itemAndEnemyProcessor.randomSetThingsType(itemGenerator.Static.ProbabilityArray) ) {
            case 1:
                hpitemSetting();
                return itemType.hpItem;
            case 2:
                spitemSetting();
                return itemType.spItem;
            case 3:
                hpmaxitemSetting();
                return itemType.hpmaxItem;
            case 4:
                spmaxitemSetting();
                return itemType.spmaxItem;
            case 5:
                coinitemSetting();
                return itemType.coinItem;
            default:
                defaultSetting();
                break;
        }

        return itemType.hpItem;
    }

    void defaultSetting() {
        AddHP = 0;
        AddSP = 0;
        AddHPMax = 0;
        AddSPMax = 0;
        AddCOIN = 0;
    }

    void hpitemSetting() {
        AddHP = 10;
        AddSP = 0;
        AddHPMax = 0;
        AddSPMax = 0;
        AddCOIN = 0;
    }

    void spitemSetting() {
        AddHP = 0;
        AddSP = 10;
        AddHPMax = 0;
        AddSPMax = 0;
        AddCOIN = 0;
    }

    void hpmaxitemSetting() {
        AddHP = 0;
        AddSP = 0;
        AddHPMax = 5;
        AddSPMax = 0;
        AddCOIN = 0;
    }

    void spmaxitemSetting() {
        AddHP = 0;
        AddSP = 0;
        AddHPMax = 0;
        AddSPMax = 2;
        AddCOIN = 0;
    }

    void coinitemSetting() {
        AddHP = 0;
        AddSP = 0;
        AddHPMax = 0;
        AddSPMax = 0;
        AddCOIN = 10;
    }

}
