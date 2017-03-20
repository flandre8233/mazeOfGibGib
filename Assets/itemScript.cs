using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    public bool IsAutoSetType = true;
    public enum itemType
    {
        hpItem,
        spItem,
        hpmaxItem,
        spmaxItem,
        coinItem
    };
    public itemType ItemType;

    public int AddHP { get; set; }
    public int AddSP { get; set; }
    public int AddHPMax { get; set; }
    public int AddSPMax { get; set; }
    public int AddCOIN { get; set; }

    void Awake() {
        setItemType();
    }

    public void setItemType() {
        if (IsAutoSetType) {
            ItemType = itemGenerator.Static.randomSetItemType();
        }
        setItemFunction();

    }

    void setItemFunction() {
        switch (ItemType) {
            case itemType.hpItem:
                hpitemSetting();
                break;
            case itemType.spItem:
                spitemSetting();
                break;
            case itemType.hpmaxItem:
                hpmaxitemSetting();
                break;
            case itemType.spmaxItem:
                spmaxitemSetting();
                break;
            case itemType.coinItem:
                coinitemSetting();
                break;
            default:
                defaultSetting();
                break;
        }
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
