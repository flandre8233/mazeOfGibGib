using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgeoundMusicScript : MonoBehaviour {

    public static backgeoundMusicScript staticBackgeound;
    AudioSource myAudio;

    public List<AudioClip> backgroundAmbient;
    public AudioClip game_background;
    public AudioClip gamestart_background;




    void Awake()
    {
        myAudio = GetComponent<AudioSource>();
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
        myAudio.PlayOneShot(backgroundAmbient[number], 1.0f);
    }
    public void play_game_background()
    {
        myAudio.PlayOneShot(game_background, 1.0f);
    }
    public void play_gamestart_background()
    {
        myAudio.PlayOneShot(gamestart_background, 1.0f);
    }

}
