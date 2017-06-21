﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class OnlineData : MonoBehaviour {

    public string url;

    private void Start()
    {
        StartCoroutine(GetData());
        //StartCoroutine(InsertData());
        //StartCoroutine(UpdateData());
    }

    IEnumerator GetData()
    {
        WWW www = new WWW(url+"GetData.php");
        yield return www;
        Debug.Log(www.text.Trim());
    }

    /*IEnumerator InsertData()
    {
        ShopItemInfo newObject = new ShopItemInfo();
        newObject.Price = 100;
        newObject.HKD_price = 100;
        newObject.Name = "name";
        newObject.Detail = "detail";
        newObject.Give_ability = 0;

        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("Data", JsonMapper.ToJson(newObject));

        WWW www = new WWW(url + "InsertData.php",wwwForm);
        yield return www;
        Debug.Log(www.text.Trim());
    }*/

    /*IEnumerator UpdateData()
    {
        ShopItemInfo updateObject = new ShopItemInfo();
        updateObject.ID = 1;
        updateObject.Price = 250;
        updateObject.HKD_price = 456;
        updateObject.Name = "tryname";
        updateObject.Detail = "trydetail";
        updateObject.Give_ability = 1;

        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("Data", JsonMapper.ToJson(updateObject));

        WWW www = new WWW(url + "UpdateData.php", wwwForm);
        yield return www;
        Debug.Log(www.text.Trim());
    }*/
}
