﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookable : MonoBehaviour
{
    public float distanceFromCamera = 2.0f;
    public float dSpeed = 1.0f;
    public float rSpeed = 1.0f;
    public bool isHit = false;

    protected bool beingCarried = false;
    protected bool touched = false;
    protected bool arrived = false;
    protected bool snapped = false;

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

    private void Update()
    {
        MoveObject();
        RotateObject();
        if(!arrived && !snapped && !beingCarried)
        {
            cameraLook.enabled = true;
            player.enabled = true;
            self.enabled = true;
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
        }
        return isHit;
    }

    public void DropDown()
    {
        Debug.Log("Dropping");
        transform.parent = null;
        //transform.position = lastPosition;
        //transform.eulerAngles = lastRotation;
        beingCarried = false;
        carrier = null;
    }

    private void MoveObject()
    {
        if (beingCarried && !arrived)
        {
            var target = carrier.position + (carrier.forward * distanceFromCamera);
            transform.position = Vector3.MoveTowards(transform.position, target, dSpeed * Time.deltaTime);
            if (transform.position == target)
            {
                arrived = true;
            }
        }
        else if (!beingCarried && arrived)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition, dSpeed * Time.deltaTime);
            if (transform.position == lastPosition)
            {
                arrived = false;
                
            }
        }
    }

    private void RotateObject()
    {
        if(beingCarried && !snapped)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, carrier.rotation, rSpeed);
            if(transform.rotation == carrier.rotation)
            {
                snapped = true;
            }
        }
        else if(!beingCarried && snapped)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(lastRotation), rSpeed);
            if(transform.rotation == Quaternion.Euler(lastRotation))
            {
                snapped = false;
            }
        }
    }
}
