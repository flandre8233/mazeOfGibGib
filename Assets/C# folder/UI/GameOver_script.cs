using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOver_script : MonoBehaviour {
    public RectTransform gameover_show;
    [SerializeField]
    Text Best_Stage;
    [SerializeField]
    Text Current_Stage;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerDataBase.Static.check_start);
        gameover_menu();
        //Debug.Log(playerDataBase.Static.maxFloor + "max");
        //Debug.Log(playerDataBase.Static.currentFloor+"current");
        Current_Stage.text = playerDataBase.Static.currentFloor + "";
        Best_Stage.text = playerDataBase.Static.maxFloor + "";
        if (playerDataBase.Static.currentFloor < playerDataBase.Static.maxFloor)
        {
            playerDataBase.Static.maxFloor = playerDataBase.Static.maxFloor;
        }
        else
        {
            playerDataBase.Static.maxFloor = playerDataBase.Static.currentFloor;
        }
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
        Time.timeScale = 1.0F;
        playerDataBase.Static.check_start = false;
        playerDataBase.Static.restart_data();
        //gameover_show.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
        //Start_menu_canvas.Static.start_menu.gameObject.SetActive(false);
    }

    public void exit_to_start()
    {
        Time.timeScale = 1.0F;
        playerDataBase.Static.check_start = true;
        playerDataBase.Static.restart_data();
        SceneManager.LoadScene(0);
    }
}
