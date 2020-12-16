using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day7 : MonoBehaviour
{
    public Clock clock;

    private float time;

    bool Thought1 = false;
    bool Thought2 = false;

    bool Email1 = false;
    bool Email2 = false;
    bool Email3 = false;

    void FixedUpdate()
    {
        time = clock.timeRounded;

        if(time == 451 && !Thought1)
        {
            EventManager.OnInnerThoughtInitiated("What happened... I feel great but I can't remember anything after last night...", 10.0f, 5, false);
            Thought1 = true;
        }

        if (time >= 451 && !Email1) //7:30
        {
            EventManager.OnAddEmailInitiated("\nSender: My Babe <3 " +
                "\n Sent: 7:00 am" +
                "\n Subject: Happy Sunday my love! " +
                "\n \n  Hope your weekend was super fun! Sorry I didn't call or email you at all this week, right after I landed in Tokyo my parents took me to their summer cottage to hold up until the " +
                "pandemic was over. My father forbid us from using social media, you know how paranoid he is… But now that the vaccine is here I can come back to stay with you!!! Yay!!! Did you hear about" +
                " the anonymous hacker duo that distributed the vaccine? They released evidence on the dark net that the government tried to distribute these preventative pill things to people and they" +
                " caused terrible side effects… I read a report that they were only given to elite members of society so I’m sure they got good medical treatment anyways. But yah, crazy right? Anyways" +
                " I’m gonna book a flight home as soon as the semester is over! I only have one more month until finals! I love you and I can’t wait to see you again! <3" +
                "\n \n With love, \n -Maia ");
            Email1 = true;
        }

        if (time >= 495 && !Email2) //7:30
        {
            EventManager.OnAddEmailInitiated("\nSender: The Boss " +
                "\n Sent: 8:15 am" +
                "\n Subject: Promotion Notice " +
                "\n \n Gahara," +
                "\n \n Sorry to bother you over the weekend but I thought you’d like to know your request for a raise came back. In fact the Elite Board of Data Entry has decided to promote you to a Senior " +
                "Management position. I’m bummed to see you leave, but your performance this week was so stellar I know you’re too damn smart to be working in this division. It’s been a pleasure working " +
                "with you Gahara. I wish the best for you." +
                "\n \n -Bob White ");
            Email1 = true;
        }

        if (time >= 540 && !Email3) //7:30
        {
            EventManager.OnAddEmailInitiated("\nSender: NASA: Mars Recruitment Division " +
                "\n Sent: 9:00 am" +
                "\n Subject: Approval of Immigration  " +
                "\n \n Gahara," +
                "\n \n We are pleased to inform you that you have been approved for the next Mars colonization effort that will take place in 2088. You along with a significant other are invited to become " +
                "a part of the Phase 7 wave of immigrants of the New Mars Society. If you agree, you and your partner will renounce your Citizenship of Earth and will join the thousands of other citizens who" +
                " strive to create a society bereft of corruption and manipulation. If you desire a life of free will and prosperity away from the inadequacies of our Earthly government, then please join us " +
                "on the Galaxy’s superior planet. We await your reply Mr. Gahara." +
                "\n \n -Mars Recruitment Division ");
            Email1 = true;
        }

        if (time == 580 && !Thought2)
        {
            EventManager.OnInnerThoughtInitiated("Everything is gonna be just fine... I think I'm gonna have a well deserved nap!", 10.0f, 5, false);
            Thought2 = true;
        }
    }
}
