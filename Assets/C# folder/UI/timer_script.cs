using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class timer_script : MonoBehaviour {
    public Text default_timer;
    TimeSpan Sec_convert;
    string Conv_hours;
    string Conv_minutes;
    string Conv_seconds;

    void Start () {
        //start_time = DateTime.Now;
        //default_timer.text = start_time.ToString();
    }

    void Update () {
        //counting_time = DateTime.Now;
        //counting_timer.text = counting_time.ToString();

        //TimeSpan timeSpan = counting_time.Subtract(start_time);
        //playerDataBase.Static.runTimeDouble = timeSpan.TotalSeconds;
        //result_timer.text = timeSpan.ToString();

        playerDataBase.Static.runTimeDouble += Time.deltaTime;
        TimeSpan ts1 = new TimeSpan();
        ts1 = TimeSpan.FromSeconds(playerDataBase.Static.runTimeDouble);
        //default_timer.text = ts1.Seconds.ToString();
        //Sec_convert = TimeSpan.FromSeconds(playerDataBase.Static.runTimeDouble);

        if(ts1.Hours<10)
        {
            Conv_hours = "0"+ts1.Hours.ToString();
        }
        else
        {
            Conv_hours = ts1.Hours.ToString();
        }

        if(ts1.Minutes<10)
        {
            Conv_minutes = "0" + ts1.Minutes.ToString();
        }
        else
        {
            Conv_minutes = ts1.Minutes.ToString();
        }

        if (ts1.Seconds<10)
        {
            Conv_seconds = "0" + ts1.Seconds.ToString();
        }
        else
        {
            Conv_seconds = ts1.Seconds.ToString();
        }
        default_timer.text = Conv_hours + ":" + Conv_minutes + ":" + Conv_seconds;

    }


}
