using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class loadingTest : MonoBehaviour {
    public static loadingTest Static;
    public GameObject loadingScene;
    private AsyncOperation async = null;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Static != null)
        {
            Destroy(this);
        }
        else
        {
            Static = this;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.B) )
        {
            StartCoroutine(LoadLevel("testMAPZone"));
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            //StartCoroutine(LoadLevel("Loading"));
            StartCoroutine(playLoadingScene());
        }
    }

    public void startLoading()
    {
        StartCoroutine(playLoadingScene());
    }

    public void closeLoading()
    {
        loadingScene.SetActive(false);
    }

    private IEnumerator LoadLevel(string Level)
    {
        //SceneManager.LoadScene("testMAPZone");
        //SceneManager.LoadSceneAsync(Level);
        yield return async;
    }
    private IEnumerator playLoadingScene( )
    {
        loadingScene.SetActive(true);
        //SceneManager.LoadScene("testMAPZone");
        //SceneManager.LoadSceneAsync(Level);
        yield return null;
    }
}
