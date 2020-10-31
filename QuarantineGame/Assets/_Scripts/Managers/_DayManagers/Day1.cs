using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1 : MonoBehaviour
{
    public Clock clock;
    public PlayerUI playerUI;

    public float time;
    public float timeExact;

    // Update is called once per frame
    void Update()
    {
        //this rounds the time to a value that has 4 decimal places so you can set events for times in 15 minute intervals
        time = clock.day;
        timeExact = (float)Mathf.Round(time * 10000f) / 10000f;
        //Debug.Log(timeExact);

        if (timeExact == 0.3125)
        {
            StartCoroutine(playerUI.InnerThought("I should check my bulletin board", 10.0f));
        }

    }
}
