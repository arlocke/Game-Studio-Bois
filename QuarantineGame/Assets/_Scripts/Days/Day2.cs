using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day2 : MonoBehaviour
{
    public Clock clock;

    private float time;
    bool Thought1 = false;
    bool Thought2 = false;
    bool Thought4 = false;
    bool Thought5 = false;
    bool Thought6 = false;
    bool Thought10 = false;

    bool Email1 = false;
    bool Email2 = false;
    bool Email3 = false;

    bool timeToWork = false; //activate time

    public Text questLogUI; //make sure to click and drag


    // Update is called once per frame
    void FixedUpdate()
    {
        time = clock.timeRounded;

        if (time == 451 && !Thought1) //7:31
        {
            EventManager.OnInnerThoughtInitiated("Another day.. Wonder how my girlfriend is doing?", 10.0f, 5, false);
            Thought1 = true;
        }

        if (time >= 451 && !Email1) //7:30
        {
            EventManager.OnAddEmailInitiated("\nSender: My Babe <3 " +
                "\n Sent: 4:27 am" +
                "\n Subject: Hello my love! " +
                "\n \n Hope your first day of remote work went well! I miss you so much and I know you're gonna do great this week!" +
                "I just landed in Tokyo and I know my parents are gonna lecture me the entire ride back to their apartment... I just wish they would've let me stay with you!   :(  " +
                "But once this quarantine is over I can finally get my diploma and move right in with you! I love you so much <3 " +
                "\n With love, \n -Maia ");
            Email1 = true;
        }

        if (time >= 480 && !Email2) //8:00
        {
            EventManager.OnAddEmailInitiated("\nSender: Carol from HR  " +
                "\n Sent: 8:00 am " +
                "\n Subject: Dear Mr. Gahara" +
                "\n \n You are invited to an urgent meeting with the rest of the Data Entry Specialist team on:" +
                "\n \n Tuesday, April 20th, 2087 from 9am-12pm" +
                "\n " +
                "\n Thank you, \n -Carol Day (HR Dept, Neo-Flonkerton Data Entry Incorporated)");
            Email2 = true;
        }

        if (time == 540 && !timeToWork) //9:00
        {
            EventManager.OnInnerThoughtInitiated("I guess I can attend the work meeting now...", 5.0f, 5, false);
            EventManager.OnActivateWorkPromptInitiated();
            timeToWork = true;
        }

        if (time == 600 && !Thought4) //10;00
        {
            EventManager.OnInnerThoughtInitiated("I should really attend that work meeting... why am I doing this to myself?! Is " +
                "someone controlling me????", 10.0f, 5, false);
            Thought4 = true;
        }

        if (time == 700 && !Thought5)
        {
            EventManager.OnInnerThoughtInitiated("If I don't attend the meeting I am going to be fired!", 10.0f, 5, false);
            Thought5 = true;
        }

        if (time == 720 && !Thought6 && !EventManager.OnQuestCheck("Work")) //12
        {
            EventManager.OnInnerThoughtInitiated("I missed the meeting.... How could this happen?! What is wrong with my brain??!!", 10.0f, 5, false);
            if (questLogUI.text.Contains("Work") && !questLogUI.text.Contains("Work - Completed"))
            {
                questLogUI.text = questLogUI.text.Replace("Work", "<color=red>Work - Missed</color>");
            }
            else if (!questLogUI.text.Contains("Work"))
            {
                questLogUI.text += "<color=red>Work - Missed</color>\n";
            }
            EventManager.OnRemoveWorkPromptInitiated();
            EventManager.OnAddEmailInitiated("\nSender: The Boss" +
                "\n Sent: 12:00 pm " +
                "\n Subject: THIS IS THE FINAL STRAW " +
                "\n \n yOU MUST BE A REAL COMEDIAN GAHARA!! YOU MUST THINK THIS IS REAL FUNNY! AFTER I STUCK UP FOR YOU THIS ENTIRE TIME?" +
                "I HAVE HAD ENOUGH WITH YOUR SHENANIGANS... \n \n" +
                "I bet the rumors are true.. you really are losing your mind. \n" +
                "don't prove me right Gahara. Show up tomorrow... -Mr. White ");
            Thought6 = true;
        }

        if (time == 900 && !Thought10 && !EventManager.OnQuestCheck("Pills")) //12
        {
            EventManager.OnInnerThoughtInitiated("Seriously where are my pills?", 5.0f, 5, false);
            Thought10 = true;
        }

        if (time == 800 && !Thought2 && !EventManager.OnQuestCheck("Food")) //12
        {
            EventManager.OnInnerThoughtInitiated("I'm so hungry... I gotta eat something.", 5.0f, 5, false);
            Thought2 = true;
        }

        if (time >= 900 && !Email3) //3:00
        {
            EventManager.OnAddEmailInitiated("\nSender:  Mr. Humphree " +
                "\n Sent: 8:00 am " +
                "\n Subject: To all tenants" +
                "\n \n Just a friendly reminder that all penthouse tenants, while you are isolated from B block tenants," +
                " are not allowed outside during the allotted quarantine period. Since there are only a handful of" +
                " you, I also hope you will be able to resolve disputes among yourselves. There have been reports of strange " +
                "noises in the middle of the night. I would like to remind everyone also that there are absolutely" +
                " no pets allowed in the EmailCenter apartment building." +
                "\n " +
                "\n Thank you, \n -Mr Humphree (Chief landlord of the EmailCenter)");
            Email3 = true;
        }

        if (time > 1260 & !EventManager.ending)
        {
            EventManager.ending = true;
            EventManager.OnEnd();
        }
    }
}
