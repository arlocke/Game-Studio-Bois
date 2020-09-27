using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookable : MonoBehaviour
{
    public float distanceFromCamera = 2.0f;
    public bool isHit = false;

    protected bool beingCarried = false;
    protected bool touched = false;

    protected Vector3 lastPosition;
    protected Vector3 lastRotation;
    protected Transform carrier;
    protected MouseLook cameraLook = null;
    protected PlayerMovement player = null;
    protected Collider self;

    private void Start()
    {
        self = transform.GetComponent<Collider>();
        if(self == null)
        {
            Debug.Log("Self can't find Collider - Lookable");
        }
    }

    public bool PickUp(Transform tran)
    {
        Debug.Log("Picking Up");
        if (isHit)
        {
            transform.parent = tran;
            beingCarried = true;
            lastPosition = transform.position;
            lastRotation = transform.eulerAngles;
            carrier = tran;
            if(cameraLook == null)
            {
                cameraLook = carrier.GetComponent<MouseLook>();
            }
            if (player == null)
            {
                player = cameraLook.playerBody.GetComponent<PlayerMovement>();
            }
            if(cameraLook != null)
            {
                cameraLook.enabled = false;
            }
            if(player != null)
            {
                player.enabled = false;
            }
            self.enabled = false;
            transform.position = carrier.position + (carrier.forward * distanceFromCamera);
        }
        return isHit;
    }

    public void DropDown()
    {
        Debug.Log("Dropping");
        transform.parent = null;
        transform.position = lastPosition;
        transform.eulerAngles = lastRotation;
        beingCarried = false;
        carrier = null;
        cameraLook.enabled = true;
        player.enabled = true;
        self.enabled = true;
    }
}
