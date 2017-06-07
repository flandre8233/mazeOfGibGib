using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pause_script : MonoBehaviour {

    public RectTransform testing;
    public Image image;

    public Animator pause_animator;
    public RectTransform pause_background;
    public RectTransform sound_on;
    public RectTransform sound_off;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(AudioListener.volume);
	}

    public void pause_timescale()
    {
        Time.timeScale = 0;
    }

    public void pause_menu_popout()
    {
        if (pause_animator.GetBool("pause_bool")==false)
        {
            //Debug.Log(pause_animator.GetBool("pause_bool"));
            pause_animator.SetBool("pause_bool", true);
            pause_background.gameObject.SetActive(true);
        }
        else
        {
            pause_animator.SetBool("pause_bool", false);
            pause_background.gameObject.SetActive(false);
            Time.timeScale = 1.0F;
        }
    }

    public void mute_sound()
    {
            AudioListener.volume = 0.0F;
            sound_off.gameObject.SetActive(true);
            sound_on.gameObject.SetActive(false);
    }

    public void unmute_sound()
    {
        AudioListener.volume = 1;
        sound_off.gameObject.SetActive(false);
        sound_on.gameObject.SetActive(true);
    }
}
