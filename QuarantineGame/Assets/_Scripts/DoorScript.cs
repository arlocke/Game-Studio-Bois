using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject trigger;
    public GameObject door;

    public AudioSource doorOpenSFX;
    public AudioSource doorCloseSFX;


    Animator doorAnim;

    public bool isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isLocked)
        {
            SlideDoor(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isLocked)
        {
            doorOpenSFX.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isLocked)
        {
            SlideDoor(false);
            doorCloseSFX.Play();
        }
    }

    void SlideDoor(bool state)
    {
        doorAnim.SetBool("slide", state);
    }
}
