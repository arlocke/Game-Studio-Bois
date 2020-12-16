using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject connectedLight;
    public bool on = false;
    public AudioSource lightSwitchSFX;

    public void SwitchOn()
    {
            //Debug.Log("trying to lightswitch");
            connectedLight.SetActive(true);
            on = true;
            lightSwitchSFX.Play();
    }

    public void SwitchOff()
    {
            //Debug.Log("trying to lightswitch");
            connectedLight.SetActive(false);
            on = false;
            lightSwitchSFX.Play();
    }
}
