using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day5 : MonoBehaviour
{
    public Clock clock;

    private float time;
    bool Thought1 = false;
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
            EventManager.OnInnerThoughtInitiated("My head is throbbing", 10.0f, 5, false);
            Thought1 = true;
        }

        if (time >= 451 && !Email1) //7:30
        {
            EventManager.OnAddEmailInitiated("\nSender: <color=blue> UR HACKER FRIEND </color> " +
                "\n Sent: 6:66 am" +
                "\n Subject:  <color=magenta> THE FATE OF THE WORLD Ver 2.0 </color>" +
                "\n \n   <color=cyan> I'm sure you have questions and I'll answer them all in due time. All you need to know now, is your DNA is special and I need a sample of it" +
                " to save the human race. I have sent you a </color><color=red>DNA VIAL</color><color=cyan> that will extract a skin sample upon touch. Simply send that through the mail and I will answer more of your" +
                " questions later. Just know you are a special individual Gahara. The world is depending on you. I must sign off now, someone seems to be busting down my door." +
                "Just remember... Don't trust anyone, don't trust your girlfriend, don't trust the government and whatever " +
                "you do </color> <color=red>AVOID DRONES AT ALL COSTS!!!</color> " +
                "\n - ANON");
            Email1 = true;
        }

        if (time >= 550 && !Email2) //8:00
        {
            EventManager.OnAddEmailInitiated("\nSender: Carol from HR" +
                "\n Sent: 9:10 am " +
                "\n Subject: Notice of termination " +
                "\n \n This courtesy email has been sent to inform you of your termination from Data Entry Incorporated. We are sorry to see you go but your time with us" +
                " has been *MISSING ADJECTIVE* and *MISSING ADDITIONAL ADJECTIVE*. Please inform *MISSING FRIEND FILE* that you will not be attending work as I'm sure they'll miss you!" +
                " Take care and remember, only you can make Neo-Flonkerton great again!  \n \n END OF AUTOMATED FIRING EMAIL");
            Email2 = true;
        }

        if (time == 540 && !timeToWork) //9:00
        {
            EventManager.OnInnerThoughtInitiated("I guess I can attend the work meeting now...", 5.0f, 5, false);
            timeToWork = true;
        }

        if (time == 720 && !Thought6 && !EventManager.OnQuestCheck("Food")) //12
        {
            EventManager.OnInnerThoughtInitiated("I'm so hungry, but I don't want anymore cereal...", 10.0f, 5, false);
            Thought6 = true;
        }

        if (time == 850 && !Thought7 && !EventManager.OnQuestCheck("Unlock Hall")) //3
        {
            EventManager.OnInnerThoughtInitiated("I need to unlock the hallway.. I gotta get to the mail box", 5.0f, 5, false);
            Thought7 = true;
        }

        if (time == 900 && !Thought8 && !EventManager.OnQuestCheck("Unlock Bar")) //4
        {
            EventManager.OnInnerThoughtInitiated("I gotta find my ID card... I bet it's in the bar", 5.0f, 5, false);
            Thought8 = true;
        }

        if (time == 1020 && !Thought9 && !EventManager.OnQuestCheck("Mail DNA")) //5
        {
            EventManager.OnInnerThoughtInitiated("I'm running out of time to mail the DNA vial!", 5.0f, 5, false);
            Thought9 = true;
        }

        if (time == 1100 && !Thought10 && !EventManager.OnQuestCheck("Pills")) //3
        {
            EventManager.OnInnerThoughtInitiated("Where are my pills?", 5.0f, 5, false);
            Thought10 = true;
        }

        if (time >= 900 && !Email3) //3:00
        {
            EventManager.OnAddEmailInitiated("\nSender:  Mr. Humphree " +
                "\n Sent: 3:00 pm " +
                "\n Subject: Mr Gahara - Eviction Notice" +
                "\n \n This email is to notify you of your immediate eviction due to breach of quarantine. Upon investigation by the Bureau of Health and Safety, you have been" +
                "classified as a low level of citizen according to societal worth. Therefore, you will be escorted from your penthouse apartment and sent" +
                "to a Z-Block hygiene camp where you will be processed. Per the request of your landlord, Mr White, we have been instructed to remove you " +
                "from your apartment on March 25th at 3am . Please be ready and we look forward to processing you!" +
                "\n " +
                "\n Thank you, \n -Officer Vinny \n Neo-Flonkerton Viral Outbreak Task Force");
            Email3 = true;
        }

        if (time >= 660 && !Email4) //11:00
        {
            EventManager.OnAddEmailInitiated("\nSender: My Love <3  " +
                "\n Sent: 11:00 am " +
                "\n Subject: It's over" +
                "\n \n Never call me again. This is the last message I will ever send to you. After this I will block you on all social media and sever" +
                "all our connections. I never want to see you again... You have ruined my family and my entire adult life... I can never forgive you for " +
                "what you did. I fell in love with the man you used to be. Now you've become a monster... I hope you die." +
                "\n\n\n" +
                "\n " +
                "\n -Maia (your girlfriend if you still remember)");
            Email4 = true;
        }

        if (time > 1260 & !EventManager.ending)
        {
            EventManager.ending = true;
            EventManager.OnEnd();
        }
    }
}


