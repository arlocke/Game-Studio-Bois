using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1 : MonoBehaviour
{
    public Clock clock;

    public float time;

    // Update is called once per frame
    void Update()
    {

        if (time >= 440)
        {
            EventManager.OnInnerThoughtInitiated("I should check my bulletin board", 10.0f);
        }

    }
}
