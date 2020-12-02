using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public float spinSpeed = 50.0f;

    private void Update()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}

