using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchScript : MonoBehaviour
{
    public Transform position;
    public Transform position2;
    public bool Day5;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody dud = other.transform.GetComponent<Rigidbody>();
        if (Day5)
        {
            if(other.name == "Crowbar")
            {
                if (dud != null)
                {
                    dud.velocity = Vector3.zero;
                }
                other.transform.position = position2.position;
            }
            else
            {
                if (dud != null)
                {
                    dud.velocity = Vector3.zero;
                }
                other.transform.position = position.position;
            }
        }
        else
        {
            if (dud != null)
            {
                dud.velocity = Vector3.zero;
            }
            other.transform.position = position.position;
        }
    }
}
