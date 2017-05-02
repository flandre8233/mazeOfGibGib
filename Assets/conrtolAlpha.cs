using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class conrtolAlpha : MonoBehaviour {
    [Range (0,100)]
    public float alpha = 0.0f;
    
    private void Awake()
    {
        foreach (var item in gameObject.GetComponentsInChildren<Text>())
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, alpha / 100.0f);
        }
    }

}
