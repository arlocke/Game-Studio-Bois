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

    /// The Many Bools

    bool Thought1 = false;
    bool Thought2 = false;
    bool Thought3 = false;
    bool Thought4 = false;
    bool Thought5 = false;

    bool Email1 = false;
    bool Email2 = false;
    bool Email3 = false;

    /// 


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


            if (timeRounded == 455 && !Thought1)
            {
                Debug.Log("Doing 1st thought");
                EventManager.OnInnerThoughtInitiated("Ugh I'm so tired... I should check my bulletin board for my daily tasks", 10.0f);
                Thought1 = true;
            }

            if (timeRounded == 470 && !Thought2)
            {
                EventManager.OnInnerThoughtInitiated("Oh crap... I bet my boss emailed me", 10.0f);
                Thought2 = true;
            }

            if (timeRounded == 470 && !Email1)
            {
                EventManager.OnAddEmailInitiated("Sender: The Boss \n \n Hey Gahara! \n Make sure you attend the remote work meeting today." +
                    " I'm counting on you to be the star employee to show the other guys we can transition to remote working smoothly. Don't let me down! \n -The Boss");
                Email1 = true;
            }

            if (timeRounded == 490 && !Email2)
            {
                EventManager.OnAddEmailInitiated("Sender: The Boss \n \n Hey Gahara! \n Also FYI... Don't do anything crazy in the meeting... The team is a little worried about you \n -The Boss");
                Email2 = true;
            }

            /*
            if(timeRounded == 455)
            {
                if(lastTime < 454)
                {
                    lastTime = time;
                }
                if(lastTime == time)
                {
                    Debug.Log("First Thought");
                    EventManager.OnInnerThoughtInitiated("hello I want to do this", 5.0f);
                }
            }

            if(timeRounded == 465)
            {
                if(lastTime < 464)
                {
                    lastTime = time;
                }
                if(lastTime == time)
                {
                    Debug.Log("Second Thought");
                    EventManager.OnInnerThoughtInitiated("this is the second thing Im thinking of", 5.0f);
                }
            }

            if(timeRounded == 480)
            {
                if(lastTime < 480)
                {
                    lastTime = time;
                }
                if(lastTime == time)
                {
                    EventManager.OnAddEmailInitiated("What is up ladies and gentleman");
                }
            }*/

        if(timeRounded > 525 & !EventManager.ending)
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
