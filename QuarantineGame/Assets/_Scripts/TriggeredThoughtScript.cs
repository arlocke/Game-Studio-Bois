using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredThoughtScript : MonoBehaviour
{
    public string context = "";
    public float time = 5.0f;
    public bool overWritable = true;

    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !triggered)
        {
            triggered = true;
            Debug.Log("Im trying to show a thought");
            EventManager.OnInnerThoughtInitiated(context, time, 95, overWritable);
        }
    }


}
