using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading_canvas : MonoBehaviour {
    private AsyncOperation async = null;
    public GameObject loadingScene;
    // Use this for initialization
    void Start () {
        StartCoroutine(LoadLevel("testMAPZone"));

    }
	
	// Update is called once per frame
	void Update () {
        if (async != null)
        {

            Destroy(loadingScene);
        }
    }

    public void Start_exit()
    {
        SceneManager.LoadScene(1);
    }

    private IEnumerator LoadLevel(string Level)
    {
        Application.LoadLevelAsync(Level);
        yield return async;

    }

    public void change_scene()
    {
        if (async != null)
        {

            Destroy(loadingScene);
        }
    }
}
