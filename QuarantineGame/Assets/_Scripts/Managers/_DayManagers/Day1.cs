using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text questLogUI; //make sure to click and drag

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
                "\n Sent: 8:00 am " +
                "\n Subject: Hey Gahara!" +
                "\n \n Make sure you attend the remote work meeting today from 9:00 - 12:00!!! I'm counting on you to be the star " +
                "employee to show the other guys we can make a smooth transition during this pandemic. Don't let me down! " +
                "\n \n -The Boss");
            Email1 = true;
        }

        if (time >= 530 && !Email2) //8:50
        {
            EventManager.OnAddEmailInitiated("Sender: The Boss" +
                "\n Sent: 8:50 am " +
                "\n Subject: SERIOUS REMINDER!" +
                "\n \n Also FYI... Don't mention anything that about the virus in the meeting... We have to keep morale up and I " +
                "know you're someone who easily goes off the rails. Don't let me down! " +
                "\n \n -The Boss");
            Email2 = true;
        }

        if (time == 530 && !Thought3)
        {
            EventManager.OnInnerThoughtInitiated("Did I just get another email...?", 10.0f);
            Thought3 = true;
        }

        if (time == 540 && !timeToWork) //9:00
        {
            EventManager.OnInnerThoughtInitiated("I guess I can attend the work meeting now...", 5.0f);
            EventManager.OnActivateWorkPromptInitiated();
            timeToWork = true;
        }

        if (time == 600 && !Thought4) //10;00
        {
            EventManager.OnInnerThoughtInitiated("I should really attend that work meeting... why am I doing this to myself?! Is " +
                "someone controlling me?", 10.0f);
            Thought4 = true;
        }

        if (time >= 780 && !Email3) //1:00 pm
        {
            EventManager.OnAddEmailInitiated("Sender: Doc " +
                "\n Sent: 1:00 pm" +
                "\n Subject: Hello Mr. Gahara! " +
                "\n \n Thanks for participating in our clinical vaccine trial. Your help will go a long way towards beating this " +
                "pandemic together. We're counting on you! " +
                "\n \n -Your friendly neighborhood doctor");
            Email3 = true;
        }

        if (time == 700 && !Thought6)
        {
            EventManager.OnInnerThoughtInitiated("THIS IS MY LAST CHANCE TO ATTEND MY WORK MEETING!!", 10.0f);
            Thought6 = true;
        }

        if (time == 720 && !Thought5 && !EventManager.OnQuestCheck("Work")) //12
        {
            EventManager.OnInnerThoughtInitiated("Oh my God!! How did I miss my work meeting?!? WTF is wrong with me?!", 10.0f);
            if(questLogUI.text.Contains("Work") && !questLogUI.text.Contains("Work - Completed"))
            {
            questLogUI.text = questLogUI.text.Replace("Work", "<color=red>Work - Completed...?</color>");
            }
            EventManager.OnRemoveWorkPromptInitiated();
            Thought5 = true;

            EventManager.OnAddEmailInitiated("Sender: The Boss" +
                "\n Sent: 12:00 pm " +
                "\n Subject: WHAT THE ABSOLUTE FUCK GAHARA " +
                "\n \n IS THIS A JOKE?????? ARE YOU TRYING TO LOSE YOUR JOB??? IT WAS THE LITERAL FIRST MEETING OF THE QUARTER!!!! YOU ARE THE " +
                "LITERAL SENIOR DATA ANALYST!!! THIS IS WHY WE PAY YOU 7 FIGURES!!! YOU'RE ON THIN ICE JACKASS!!!");
        }

       
    }
}
