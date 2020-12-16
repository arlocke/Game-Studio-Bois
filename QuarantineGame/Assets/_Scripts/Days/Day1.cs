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
    bool Thought4 = false;
    bool Thought5 = false;
    bool Thought6 = false;
    bool Thought10 = false;
    bool Thought11 = false;
    bool Thought12 = false;
    // bool Thought13 = false;

    bool bedThought = false;

    bool Email1 = false;
    bool Email2 = false;
    bool Email3 = false;
    bool Email4 = false;

    bool timeToWork = false; //

    public Text questLogUI; //make sure to click and drag

    private void Awake()
    {
        EventManager.LoadInitiated += Load;
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("Load", 0) == 0)
        {
            EventManager.OnStartTutorial();
        }
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
        else if(time > 720)
        {
            if(dud.QuestList.Contains("Missed"))
            {
                EventManager.OnAddEmailInitiated("\nSender: The Boss" +
                "\n Sent: 12:00 pm " +
                "\n Subject: WHAT THE ABSOLUTE F**K GAHARA " +
                "\n \n IS THIS A JOKE?????? ARE YOU TRYING TO LOSE YOUR JOB??? IT WAS LITERALLY THE FIRST MEETING OF THE QUARTER!!!! YOU ARE THE " +
                "SENIOR DATA ANALYST!!! THIS IS WHY WE PAY YOU 7 FIGURES!!! YOU'RE ON THIN ICE JACKASS!!!");
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

        if(time == 451 && !Thought1) //7:31
        {
            EventManager.OnInnerThoughtInitiated("Looks like I'm all set... Just gotta get through today's tasks and then I can go back to bed!", 5.0f, 5, true);
            Thought1 = true;
        }

        if (time >= 480 && !Email1) //8:00
        {
            EventManager.OnAddEmailInitiated("\nSender: The Boss " +
                "\n Sent: 8:00 am " +
                "\n Subject: Hey Gahara!" +
                "\n \n Make sure you attend the remote work meeting today from 9:00 - 12:00!!! I'm counting on you to be the star " +
                "employee to show the other guys we can make a smooth transition during this pandemic. Don't let me down! " +
                "\n \n -The Boss");
            Email1 = true;
        }

        if (time == 490 && !Thought2) //8:00
        {
            EventManager.OnInnerThoughtInitiated("I should check my emails before my work meeting. [Left Click on the Bedroom PC]", 7.0f, 5, false);
            Thought2 = true;
        }

        if (time >= 530 && !Email2) //8:50
        {
            EventManager.OnAddEmailInitiated("\nSender: The Boss" +
                "\n Sent: 8:50 am " +
                "\n Subject: SERIOUS REMINDER!" +
                "\n \n Also FYI... Don't mention anything about the virus in the meeting... We have to keep morale up and I " +
                "know you're someone who easily goes off the rails. Don't let me down! " +
                "\n \n -The Boss");
            Email2 = true;
        }

        if (time == 540 && !timeToWork) //9:00
        {
            EventManager.OnInnerThoughtInitiated("I guess I can go to work now...", 5.0f, 5, false);
            EventManager.OnActivateWorkPromptInitiated();
            timeToWork = true;
        }

        if (time == 600 && !Thought4) //10;00
        {
            EventManager.OnInnerThoughtInitiated("I should really attend that work meeting... why am I doing this to myself?! Is " +
                "something wrong with me?", 10.0f, 5, false);
            Thought4 = true;
        }

        if (time == 700 && !Thought5)
        {
            EventManager.OnInnerThoughtInitiated("This is my last chance to attend the work meeting!!", 10.0f, 5, false);
            Thought5 = true;
        }

        if (time == 720 && !Thought6 && !EventManager.OnQuestCheck("Work")) //12
        {
            EventManager.OnInnerThoughtInitiated("Oh my God!! How did I miss my work meeting?!? WTF is wrong with me?!", 10.0f, 5, false);
            if(questLogUI.text.Contains("Work") && !questLogUI.text.Contains("Work - Completed"))
            {
                questLogUI.text = questLogUI.text.Replace("Work", "<color=red>Work - Missed</color>");
            }
            else if(!questLogUI.text.Contains("Work"))
            {
                questLogUI.text += "<color=red>Work - Missed</color>\n";
            }
            EventManager.OnRemoveWorkPromptInitiated();
            EventManager.OnAddEmailInitiated("\nSender: The Boss" +
                "\n Sent: 12:00 pm " +
                "\n Subject: WHAT THE ABSOLUTE F**K GAHARA " +
                "\n \n IS THIS A JOKE?????? ARE YOU TRYING TO LOSE YOUR JOB??? IT WAS LITERALLY THE FIRST MEETING OF THE QUARTER!!!! YOU ARE THE " +
                "SENIOR DATA ANALYST!!! THIS IS WHY WE PAY YOU 7 FIGURES!!! YOU'RE ON THIN ICE JACKASS!!!");
            Thought6 = true;
        }

        if (time >= 780 && !Email3) //1:00 pm
        {
            EventManager.OnAddEmailInitiated("\nSender: Dr. Malkino PhD. " +
                "\n Sent: 1:00 pm" +
                "\n Subject: Good afternoon Mr. Gahara, " +
                "\n \n Thanks for participating in our clinical vaccine trial. Your help will go a long way towards overcoming this " +
                "pandemic together. We're counting on you! " +
                "\n \n -Your friendly neighborhood doctor");
            Email3 = true;
        }

        if (time >= 745 && !Email4 && EventManager.OnQuestCheck("Work")) //1:30
        {
            EventManager.OnAddEmailInitiated("\nSender: The Boss" +
                "\n Sent: 1:30 pm " +
                "\n Subject: Nice Work Gahara " +
                "\n \n Thanks for attending the meeting today and being a good sport about it. I made a good decision hiring you 5 years ago." +
                "I know it's stressful since we're all in Quarantine and you're cooped up in that penthouse apartment of yours alone. We're gonna get through this together!" +
                "\n \n - Mr White");
            Email4 = true;
        }

        if (time == 900 && !Thought10 && !EventManager.OnQuestCheck("Pills")) //12
        {
            EventManager.OnInnerThoughtInitiated("Dang, where did I put my pills?", 5.0f, 5, false);
            Thought10 = true;
        }

        if (time == 900 && !Thought12 && !EventManager.OnQuestCheck("Food")) //12
        {
            EventManager.OnInnerThoughtInitiated("I'm so hungry I have to eat something..", 5.0f, 5, false);
            Thought12 = true;
        }

        if (!bedThought && EventManager.OnQuestCheck("Pills") && EventManager.OnQuestCheck("Work") && EventManager.OnQuestCheck("Food"))
        {
            EventManager.OnInnerThoughtInitiated("Now that I've done all my tasks I can go back to sleep! [Left Click on bed to advance]", 10.0f, 5, false);
            bedThought = true;
        }

        if (time == 990 && !Thought11) // 1:30
        {
            EventManager.OnInnerThoughtInitiated("I'm so bored, I could just go back to sleep now...", 10.0f, 5, false);
            Thought11 = true;
        }

        if (time > 1260 & !EventManager.ending)
        {
            EventManager.ending = true;
            EventManager.OnEnd();
        }
    }
}
