using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1 : MonoBehaviour
{
    public Clock clock;

    private float time;
    bool Thought1 = false;
    bool Thought2 = false;
    bool Thought3 = false;
    bool Thought4 = false;
    bool Thought5 = false;
    bool Thought6 = false;
    bool Thought7 = false;
    bool Thought8 = false;
    bool Thought9 = false;
    bool Thought10 = false;
    bool Thought11 = false;
    bool Thought12 = false;
    bool Thought13 = false;

    bool Email1 = false;
    bool Email2 = false;
    bool Email3 = false;

    bool timeToWork = false; //activate time
    bool hasWorked = false; //bool for if you have worked

    private void Start()
    {
        if(PlayerPrefs.GetInt("Load", 0) == 0)
        {
            EventManager.OnStartTutorial();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time = clock.timeRounded;

        Debug.Log(time);

        if(time == 451 && !Thought1) //7:31
        {
            EventManager.OnInnerThoughtInitiated("Looks like I'm all set for the day. (Tutorial Done)", 7.0f);
            Thought1 = true;
        }

        if (time == 480 && !Thought2) //8:00
        {
            EventManager.OnInnerThoughtInitiated("Oh jeez... My boss must've emailed me about my morning meeting...", 7.0f);
            Thought2 = true;
        }

        if (time >= 480 && !Email1) //8:00
        {
            EventManager.OnAddEmailInitiated("Sender: The Boss " +
                "\n Sent: 8:00am " +
                "\n Subject: Hey Gahara! " +
                "\n Make sure you attend the remote work meeting today at 9:00am!!! I'm counting on you to be the star " +
                "employee to show the other guys we can make a smooth transition during this pandemic. Don't let me down! " +
                "\n -The Boss");
            Email1 = true;
        }

        if (time >= 530 && !Email2) //8:50
        {
            EventManager.OnAddEmailInitiated("Sender: The Boss" +
                "\n Sent: 8:50 " +
                "\n Subject SERIOUS REMINDER!" +
                "\n Also FYI... Don't mention anything that about the virus in the meeting... We have to keep morale up and I know you're someone who easily goes off the rails. Don't let me down! \n -The Boss");
            Email2 = true;
        }

        if (time == 530 && !Thought2)
        {
            EventManager.OnInnerThoughtInitiated("Did I just get another email...?", 10.0f);
            Thought5 = true;
        }

        if (time == 540 && !timeToWork) //9:00
        {
            EventManager.OnInnerThoughtInitiated("I guess I can attend the work meeting now...", 5.0f);
            EventManager.OnActivateWorkPromptInitiated();
            timeToWork = true;
        }

        if (time == 600 && !Thought3)
        {
            EventManager.OnInnerThoughtInitiated("I've done my work for the day! I just need to remember to take my pills before I turn in.", 10.0f);
            Thought6 = true;
        }

        if (time >= 650 && !Email3)
        {
            EventManager.OnAddEmailInitiated("Sender: Doc \n \n Hello Mr. Gahara! \n Thanks for participating in our clinical vaccine trial. Your help will go a long way towards beating this pandemic together. We're counting on you! \n -Your friendly neighborhood doctor");
            Email3 = true;
        }

        if (time == 650 && !Thought4)
        {
            EventManager.OnInnerThoughtInitiated("Looks like I got an email from my doctor. Wonder what he wanted to say...", 10.0f);
            Thought7 = true;
        }

        if (time >= 700 && !Thought5)
        {
            EventManager.OnInnerThoughtInitiated("Wow I'm still so tired... I could hit the hay right now!", 10.0f);
            Thought8 = true;
        }
    }
}
