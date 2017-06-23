using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class loadingTest : MonoBehaviour {
    public static loadingTest Static;
    //public GameObject loadingScene;

    public RectTransform level_pass;
    public bool trigger_pass=false;


    private void Awake()
    {
        if (Static != null)
        {
            Destroy(this);
        }
        else
        {
            Static = this;
        }
    }


    public void startLoading()
    {
        //StartCoroutine(playLoadingScene());
       /* trigger_pass = true;
        Debug.Log("in");
        if (trigger_pass == true)
        {
            level_pass.gameObject.SetActive(true);
        }*/
    }

    public void closeLoading()
    {
        //loadingScene.SetActive(false);
        //Debug.Log("out");
        //level_pass.gameObject.SetActive(false);

    }


}
