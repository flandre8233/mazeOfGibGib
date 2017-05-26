using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_script : MonoBehaviour {
    public RectTransform gameover_show;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameover_menu();

    }

    public void gameover_menu()
    {
        if(playerDataBase.Static.HP <=0)
        {
            gameover_show.gameObject.SetActive(true);
        }
    }

    public void reset_level()
    {
        playerDataBase.Static.restart_data();
    }
}
