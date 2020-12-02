using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject trigger;
    public GameObject door;

    Animator doorAnim;

    public bool isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isLocked)
        {
            SlideDoor(true);
            // play open audio
        }
        else
        {
            //play locked audio
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isLocked)
        {
            SlideDoor(false);
            // play open audio
        }
        else
        {
            //play locked audio
        }
    }

    void SlideDoor(bool state)
    {
        doorAnim.SetBool("slide", state);
    }
}
