using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5SnapVolume : MonoBehaviour
{
    public GameObject wood1;
    public GameObject wood2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Wood" && wood1 != null && wood2 != null)
        {
            if(!wood1.activeSelf)
            {
                wood1.SetActive(true);
            }
            else if(!wood2.activeSelf)
            {
                wood2.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
            other.gameObject.SetActive(false);
        }
    }
}
