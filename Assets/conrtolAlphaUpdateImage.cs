﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class conrtolAlphaUpdateImage : MonoBehaviour {
    [Range (0,100)]
    public float alpha = 0.0f;
    
    private void Update()
    {
        foreach (var item in gameObject.GetComponentsInChildren<Image>())
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, alpha / 100.0f);
        }
    }

}