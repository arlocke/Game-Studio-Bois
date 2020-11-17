using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceMail : MonoBehaviour
{
    private void Start()
    {
        EventManager.FirstCall += FirstTalking;
    }

    private void FirstTalking()
    {
        Debug.Log("Hello Hello, I will be talking");
    }

    private void OnDestroy()
    {
        EventManager.FirstCall -= FirstTalking;
    }
}
