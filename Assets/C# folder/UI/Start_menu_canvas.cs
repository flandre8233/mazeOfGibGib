using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_menu_canvas : MonoBehaviour {

    // Use this for initialization
    void Start () {
        backgeoundMusicScript.staticBackgeound.myAudio.Stop();
        backgeoundMusicScript.staticBackgeound.ambientAudio.Stop();
        soundEffectManager.staticSoundEffect.myAudio.Stop();

        backgeoundMusicScript.staticBackgeound.play_gamestart_background();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Start_exit()
    {
        soundEffectManager.staticSoundEffect.play_TapToStart();
        SceneManager.LoadScene(2);
    }
}
