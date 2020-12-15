using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day6 : MonoBehaviour
{
    public Clock clock;

    private void Start()
    {
        clock.time += 660;
    }
}
