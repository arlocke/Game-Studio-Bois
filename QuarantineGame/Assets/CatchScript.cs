using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchScript : MonoBehaviour
{
    public Transform position;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody dud = other.transform.GetComponent<Rigidbody>();
        if(dud != null)
        {
            dud.velocity = Vector3.zero;
        }
        other.transform.position = position.position;
    }
}
