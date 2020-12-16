using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day6 : MonoBehaviour
{
    public Clock clock;

    bool Email1 = false;

    private void Start()
    {
        clock.time += 660;
    }

    void FixedUpdate()
    {
        if (clock.time >= 451 && !Email1) //7:30
        {
            EventManager.OnAddEmailInitiated("\nSender: <color=blue> UR HACKER FRIEND </color> " +
                "\n Sent: 7pm" +
                "\n Subject:  <color=magenta> We've saved the world.. </color>" +
                "\n \n   <color=pink> We did it Gahara… We’ve stopped the pandemic and saved the world. I didn’t want to tell you this," +
                " but I caught the virus weeks before the quarantine went into place. My life has been on countdown ever since, and I think" +
                " tonight is the night it finally takes me. I made it my goal to create a vaccine from someone with DNA immune from the virus." +
                " You were the only one the government wasn’t able to get to first… That’s why they’ve made you dependent on those damn pills!" +
                " They’ve been trying to cull the population with this virus and so they wiped out everyone with the necessary DNA, except you! " +
                "\n\n   I have leaked all the classified documents exposing the government's crimes and distributed the vaccine across the 3DX printer " +
                "network. However, this has caused the Glorthank population to riot and they have knocked out the printer system in your sector!" +
                "</color><color=cyan> I have " +
                "sent a drone to the roof with the vaccine for you. You must take it before midnight or the pills will kill you!</color> \n \n" +
                "<color=pink> Goodbye Gahara, and thanks for the help. </color>" +
                "\n\n - ANON");
            Email1 = true;
        }
    }
}
