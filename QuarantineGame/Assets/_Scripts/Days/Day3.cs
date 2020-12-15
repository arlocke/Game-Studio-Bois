using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day3 : MonoBehaviour
{
    public Clock clock;

    private float time;
    bool Thought1 = false;
    bool Thought2 = false;
    bool Thought4 = false;
    bool Thought5 = false;
    bool Thought6 = false;
    bool Thought10 = false;
    bool Thought3 = false;

    bool Email1 = false;
    bool Email2 = false;
    bool Email3 = false;
    bool Email4 = false;

    bool timeToWork = false; //activate time


    public Text questLogUI; //make sure to click and drag

    private void Awake()
    {
        EventManager.LoadInitiated += Load;
    }

    private void Load()
    {
        QuestData dud = SaveLoad.LoadQuests("time_stored");
        time = float.Parse(dud.QuestList);
        dud = SaveLoad.LoadQuests("quest_log");

        if (time > 540 && time < 720)
        {
            EventManager.OnActivateWorkPromptInitiated();
        }
        else if (time > 720)
        {
            Debug.Log("It's Missed Time");
            if (dud.QuestList.Contains("Missed"))
            {
                Debug.Log("You Actually Missed It");
                EventManager.OnAddEmailInitiated("\nSender: The Boss" +
                "\n Sent: 12:00 pm " +
                "\n Subject: THIS IS THE FINAL STRAW " +
                "\n \n I take this as your unofficial notice of resignation. I hope you rot in an impoverished Z block community surrounded by Glorthank sex androids. And by " +
                "the way, we will be pursuing legal action against you for the harrasing emails you send us in the middle of this night. Go F**K yourself Gahara. \n \n -Mr. White ");
                Thought6 = true;
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.LoadInitiated -= Load;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time = clock.timeRounded;

        if (time == 451 && !Thought1) //7:31
        {
            EventManager.OnInnerThoughtInitiated("Where did this CD come from?", 10.0f, 5, false);
            Thought1 = true;
        }

        if (time >= 451 && !Email1) //7:30
        {
            EventManager.OnAddEmailInitiated("\nSender: ANON " +
                "\n Sent: NULL" +
                "\n Subject: NULL " +
                "\n \n   <color=red>We cannot communicate anonymously over email. You must mail us the COMPACT DISC left in your apartment for us to establish a secure line over" +
                " your network. Do not ask questions. We need your help. You are the key to saving the world. We look forward to working with you Gahara.</color> " +
                "\n - ANON");
            Email1 = true;
        }

        if (time >= 480 && !Email2) //8:00
        {
            EventManager.OnAddEmailInitiated("\nSender: Carol from HR  " +
                "\n Sent: 8:00 am " +
                "\n Subject: Dear Mr. Gahara" +
                "\n \n You are invited to an urgent meeting with the rest of the Data Entry Specialist team on:" +
                "\n \n Tuesday, March 21st, 2087 from 9am-12pm" +
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
            EventManager.OnInnerThoughtInitiated("I should really attend that work meeting... my boss will not be happy if I miss it.", 10.0f, 5, false);
            Thought4 = true;
        }

        if (time == 700 && !Thought5)
        {
            EventManager.OnInnerThoughtInitiated("If I don't attend the meeting I am actually going to be fired!", 10.0f, 5, false);
            Thought5 = true;
        }

        if (time == 720 && !Thought6 && !EventManager.OnQuestCheck("Work")) //12
        {
            EventManager.OnInnerThoughtInitiated("I missed the meeting.... I'll probably be fired tomorrow...", 10.0f, 5, false);
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
                "\n \n I take this as your unofficial notice of resignation. I hope you rot in an impoverished Z block community surrounded by Glorthank sex androids. And by " +
                "the way, we will be pursuing legal action against you for the harrasing emails you send us in the middle of this night. Go F**K yourself Gahara. \n \n -Mr. White ");
            Thought6 = true;
        }

        if (time == 800 && !Thought3 && !EventManager.OnQuestCheck("Mail CD")) //12
        {
            EventManager.OnInnerThoughtInitiated("I gotta send this CD... There must be instructions in my email.", 5.0f, 5, false);
            Thought3 = true;
        }

        if (time == 900 && !Thought10 && !EventManager.OnQuestCheck("Pills")) //12
        {
            EventManager.OnInnerThoughtInitiated("Where are my pills?", 5.0f, 5, false);
            Thought10 = true;
        }

        if (time == 860 && !Thought2 && !EventManager.OnQuestCheck("Food")) //12
        {
            EventManager.OnInnerThoughtInitiated("I'm so hungry... I gotta eat something.", 5.0f, 5, false);
            Thought2 = true;
        }

        if (time >= 900 && !Email3) //3:00
        {
            EventManager.OnAddEmailInitiated("\nSender:  Mr. Humphree " +
                "\n Sent: 3:00 pm " +
                "\n Subject: Mr Gahara - ID Card" +
                "\n \n I am sorry to not have informed you earlier but the ID card necessary for using the mail box located in your apartments airlock has been sent via Email and is ready" +
                " for 3D printing. Simply print the .3DX file I just sent and you should find the ID card in your 3D printer. Then tap the ID card on your mailbox and it will unlock and be available for use." +
                " We hope you will find our new mailing system useful during this pandemic." +
                "\n " +
                "\n Thank you, \n -Mr Humphree (Chief landlord of the EmailCenter)");
            Email3 = true;
        }

        if (time >= 600 && !Email4) //10:00
        {
            EventManager.OnAddEmailInitiated("\nSender: My Love <3  " +
                "\n Sent: 10:00 am " +
                "\n Subject: Hey Babe <3" +
                "\n \n You missed my call again last night? I assumed you were sleeping but you sent me those weird emails with those bizarre URL links at 3:33 am last night. What's going on? " +
                "I know you must be stressed from work and quarantining all by yourself but don't go crazy on me! I still love you. My parents are already trying to set me up with a local" +
                " businessman from their church.. I miss you a lot. Stay safe and talk to me if you need to." +
                "\n " +
                "With love, \n -Maia");
            Email4 = true;
        }

        if (time > 1260 & !EventManager.ending)
        {
            EventManager.ending = true;
            EventManager.OnEnd();
        }
    }
}

