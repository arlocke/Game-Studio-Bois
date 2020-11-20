using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{

    public List<Text> clocksToDisplay = new List<Text>();
    //public Text timeText; //Drag drop timeText in editor

    private const float REAL_SECONDS_WHILE_AWAKE = 990f; //16.5 minutes (7:30am-12am)
    private const float REAL_SECONDS_PER_INGAME_DAY = 1440f; //24 minutes (1440 seconds)
    public float time;
    public float timeRounded;
    private float timeToDisplay;
    private float hoursPerDay = 24f; 
    private float minutesPerHour = 60f;

    string hoursString;
    string minutesString;

    float lastTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        time += 450; //has the player start at 7:30am (60 * 7.5 = 450)
    }

    void FixedUpdate()
    {
        time += Time.deltaTime; // use this when calling events
        timeRounded = Mathf.Ceil(time);
        //Debug.Log(timeRounded);

        timeToDisplay = time / REAL_SECONDS_PER_INGAME_DAY;
        

        float timeNormalized = timeToDisplay % 1f;
        

        hoursString = Mathf.Floor(timeNormalized * hoursPerDay).ToString("0"); 
        minutesString = Mathf.Floor(((timeNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        for (int i = 0; i < clocksToDisplay.Count; i++)
        {
             clocksToDisplay[i].text = hoursString + ":" + minutesString; // is this bad?
        }

        if (timeRounded > 1260 & !EventManager.ending)
        {
            EventManager.ending = true;
            if(EventManager.EndingType())
            {
                EventManager.OnInnerThoughtInitiated("I found my pills and did my work!", 10.0f);
            }
            else
            {
                EventManager.OnInnerThoughtInitiated("I have not found my pills or done my work!", 10.0f);
            }
        }
    }
}
