using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgeoundMusicScript : MonoBehaviour {

    public static backgeoundMusicScript staticBackgeound;
    public AudioSource myAudio;
    public AudioSource ambientAudio;
    public List<AudioClip> backgroundAmbient;
    public AudioClip game_background;
    public AudioClip gamestart_background;




    void Awake()
    {
        if (staticBackgeound != null)
        {
            Destroy(gameObject);
        }
        else
        {
            staticBackgeound = this;
        }
        DontDestroyOnLoad(gameObject);

    }

    public void play_backgroundAmbient(int number)
    {
        if (number >= backgroundAmbient.Count)
        {
            return;
        }
        ambientAudio.clip = backgroundAmbient[number];
        ambientAudio.loop = true;
        ambientAudio.Play();
    }
    public void play_game_background()
    {
        myAudio.clip = game_background;
        myAudio.loop = true;
        myAudio.Play();
    }
    public void play_gamestart_background()
    {
        myAudio.clip = gamestart_background;
        myAudio.loop = true;
        myAudio.Play();
    }

}
