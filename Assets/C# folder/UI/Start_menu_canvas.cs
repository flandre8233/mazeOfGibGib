using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Start_menu_canvas : MonoBehaviour {
    public Text verText;


    // Use this for initialization
    void Start () {
        verText.text = "Ver : " + Application.version;

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

        backgeoundMusicScript.staticBackgeound.myAudio.Stop();
        backgeoundMusicScript.staticBackgeound.ambientAudio.Stop();

        SceneManager.LoadScene(2);
    }
}
