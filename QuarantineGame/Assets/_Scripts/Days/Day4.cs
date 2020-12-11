using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day4 : MonoBehaviour
{
    public Clock clock;

    private float time;
    bool Thought1 = false;
    bool Thought2 = false;
    bool Thought4 = false;
    bool Thought5 = false;
    bool Thought6 = false;
    bool Thought7 = false;
    bool Thought8 = false;
    bool Thought9 = false;
    bool Thought10 = false;
    //bool Thought3 = false;

    bool Email1 = false;
    bool Email2 = false;
    bool Email3 = false;
    bool Email4 = false;

    bool timeToWork = false; //activate time

    public Text questLogUI; //make sure to click and drag


    // Update is called once per frame
    void FixedUpdate()
    {
        time = clock.timeRounded;

        if (time == 451 && !Thought1) //7:31
        {
            EventManager.OnInnerThoughtInitiated("Oh god I feel ill... Did I clog the toilet?", 10.0f, 5, false);
            Thought1 = true;
        }

        if (time >= 451 && !Email1) //7:30
        {
            EventManager.OnAddEmailInitiated("\nSender: <color=blue> HACKERZ FRUM HELL </color> " +
                "\n Sent: 6:66 am" +
                "\n Subject:  <color=magenta> THE FATE OF THE WORLD </color>" +
                "\n \n   <color=cyan> Thx for sending that <color=red>COMPACT DISC</color> bruh. I can't meet u irl as imma being watched... U get it right? Lolz XD anyways I've set up dis" +
                " private connection so we can work together without interference from the man!!! Imma send you the tools to save the world soon. I hafta move constantly to avoid capture" +
                ". Much difficulty... Stay safe and whatever u do..</color> <color=red>AVOID DRONES AT ALL COSTS!!!</color><color=cyan> The fate of the world is in ur hands Gahara.  </color> " +
                "\n - ANON");
            Email1 = true;
        }

        if (time >= 480 && !Email2) //8:00
        {
            EventManager.OnAddEmailInitiated("\nSender: Carol from HR  " +
                "\n Sent: 8:00 am " +
                "\n Subject: Dear Mr. Gahara" +
                "\n \n You are invited to an urgent restructure meeting with the rest of the Data Entry Specialist team on:" +
                "\n \n Tuesday, April 22nd, 2087 from 9am-12pm" +
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
            EventManager.OnInnerThoughtInitiated("I should really attend that work meeting... my boss will literally fire me.", 10.0f, 5, false);
            Thought4 = true;
        }

        if (time == 700 && !Thought5)
        {
            EventManager.OnInnerThoughtInitiated("If I don't attend the meeting I am actually going to be fired!", 10.0f, 5, false);
            Thought5 = true;
        }

        if (time == 720 && !Thought6 && !EventManager.OnQuestCheck("Work")) //12
        {
            EventManager.OnInnerThoughtInitiated("I missed the meeting.... I'm fired", 10.0f, 5, false);
            if (questLogUI.text.Contains("Work") && !questLogUI.text.Contains("Work - Completed"))
            {
                questLogUI.text = questLogUI.text.Replace("Work", "<color=red>Work - Missed</color>");
            }
            else if (!questLogUI.text.Contains("Work"))
            {
                questLogUI.text += "<color=red>Work - Missed</color>\n";
            }
            EventManager.OnRemoveWorkPromptInitiated();
            EventManager.OnAddEmailInitiated("\nSender: Carol from HR" +
                "\n Sent: 12:00 pm " +
                "\n Subject: Notice of termination " +
                "\n \n This curtousy email has been sent to inform you of your termination from Data Entry Incorporated. We are sorry to see you go but your time with us" +
                " has been *MISSING ADJECTIVE* and *MISSING ADDITIONAL ADJECTIVE*. Please inform *MISSING FRIEND FILE* that you will not be attending work as I'm sure they'll miss you!" +
                " Take care and remember, only you can make Neo-Flonkerton great again!  \n \n END OF AUTOMATED FIRING EMAIL");
            Thought6 = true;
        }

        if (time == 900 && !Thought7 && !EventManager.OnQuestCheck("Unlock Cabinet")) //3
        {
            EventManager.OnInnerThoughtInitiated("I need to unlock that cabinet! I bet there's something in there to help me", 5.0f, 5, false);
            Thought7 = true;
        }

        if (time == 960 && !Thought8 && !EventManager.OnQuestCheck("Unlock Wardrobe")) //4
        {
            EventManager.OnInnerThoughtInitiated("I need to unlock that wardrobe! I bet the plunger is in there.", 5.0f, 5, false);
            Thought8 = true;
        }

        if (time == 1020 && !Thought9 && !EventManager.OnQuestCheck("Unclog Toilet")) //5
        {
            EventManager.OnInnerThoughtInitiated("I need to unclog the toilet! My landlord will be pissed!", 5.0f, 5, false);
            Thought9 = true;
        }

        if (time == 800 && !Thought10 && !EventManager.OnQuestCheck("Pills")) //3
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
                "\n Subject: Mr Gahara - Noise Complaint" +
                "\n \n This is my final warning. We have numerous reports of violent noises coming from your penthouse apartment from the hours of midnight to 6 am. If there's an animal" +
                " or you've invited Glorthanks into your apartment, you will be evicted immediately. I have half a mind to send an investigation crew to your apartment. This is your final warning. " +
                "\n " +
                "\n Thank you, \n -Mr Humphree (Chief landlord of the EmailCenter)");
            Email3 = true;
        }

        if (time >= 600 && !Email4) //10:00
        {
            EventManager.OnAddEmailInitiated("\nSender: My Love <3  " +
                "\n Sent: 10:00 am " +
                "\n Subject: Babe?" +
                "\n \n My father said you sent threatening messages last night.... What is happening babe? Why aren't you calling me back? And why do you keep leaving hour long" +
                " voicemails of you just breathing at 3 am? I don't understand are you mad about something? Just talk to me! :*(" +
                "\n " +
                "\n -Maia");
            Email4 = true;
        }

        if (time > 1260 & !EventManager.ending)
        {
            EventManager.ending = true;
            EventManager.OnEnd();
        }
    }
}

