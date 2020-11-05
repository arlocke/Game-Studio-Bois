using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public Text timeText; //Drag drop timeText in editor

    private const float REAL_SECONDS_WHILE_AWAKE = 990f; //16.5 minutes (7:30am-12am)
    private const float REAL_SECONDS_PER_INGAME_DAY = 1440f; //24 minutes (1440 seconds)
    public float day; 
    private float hoursPerDay = 24f; 
    private float minutesPerHour = 60f;

    string hoursString;
    string minutesString;

    bool called = false;


    // Start is called before the first frame update
    void Start()
    {
        day += .3125f; //has the player start at 7:30am (7.5/24 = .3125)
    }

    // Update is called once per frame
    void Update()
    {
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY; // this needs to be fixed
        //Debug.Log(day);

        float dayNormalized = day % 1f;

        hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("0"); 
        minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        timeText.text = hoursString + ":" + minutesString;
        //Debug.Log(timeText.text);

        if(day >= 0.32 && !called)
        {
            called = true;
            EventManager.OnInnerThoughtInitiated("hello I want to do this", 5.0f);
        }
    }
}
