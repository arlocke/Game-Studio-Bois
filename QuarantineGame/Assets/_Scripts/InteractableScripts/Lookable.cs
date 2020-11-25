using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookable : MonoBehaviour
{
    public float distanceFromCamera = 2.0f;
    public float mouseRotateSpeed = 5.0f;
    public float dSpeed = 1.0f;
    public float rSpeed = 1.0f;
    public bool isHit = false;

    protected bool initializing = true;
    protected bool beingCarried = false;
    protected bool touched = false;
    public bool arrived = false;
    public bool snapped = false;

    protected Vector3 lastPosition;
    protected Vector3 lastRotation;
    protected Transform carrier;
    protected MouseLook cameraLook = null;
    protected PlayerManager player = null;
    protected Collider self;

    private void Start()
    {
        self = transform.GetComponent<Collider>();
        if(self == null)
        {
            Debug.Log("Self can't find Collider - Lookable");
        }
        lastPosition = transform.position;
        lastRotation = transform.eulerAngles;
    }

    private void Update()
    {
        MoveObject();
        RotateObject();
        if(!arrived && !snapped && !beingCarried && !initializing)
        {
            cameraLook.enabled = true;
            player.enabled = true;
            self.enabled = true;
            initializing = true;
        }
        else if(arrived && snapped && beingCarried)
        {
            MouseRotate();
        }
    }

    public bool PickUp(Transform tran)
    {
        Debug.Log("Picking Up");
        if (isHit)
        {
            transform.parent = tran;
            beingCarried = true;
            carrier = tran;
            if(cameraLook == null)
            {
                cameraLook = carrier.GetComponent<MouseLook>();
            }
            if (player == null && cameraLook != null)
            {
                player = cameraLook.playerBody.GetComponent<PlayerManager>();
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
        initializing = false;
        return isHit;
    }

    public void DropDown()
    {
        Debug.Log("Dropping");
        transform.parent = null;
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, carrier.rotation, rSpeed * Time.deltaTime);
            if(Quaternion.Angle(transform.rotation, carrier.rotation) < 0.5)
            {
                snapped = true;
            }
        }
        else if(!beingCarried && snapped)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(lastRotation), rSpeed * Time.deltaTime);
            if(Quaternion.Angle(transform.rotation, Quaternion.Euler(lastRotation)) < 0.5)
            {
                snapped = false;
            }
        }
    }

    private void MouseRotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseRotateSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseRotateSpeed * Time.deltaTime;

        transform.RotateAround(transform.position, carrier.up, -mouseX);
        transform.RotateAround(transform.position, carrier.right, mouseY);
    }
}
