using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowableObject : MonoBehaviour
{
    public float throwForce = 10;
    public float angleForgiveness = 5.0f;
    public bool isHit = false;

    protected bool beingCarried = false;
    protected bool touched = false;

    protected Vector3 pickUpPosition;
    protected Vector3 lastCamAngle;
    protected Vector3 lastHitNorm;
    protected Vector3 pinnedPosition;
    protected Rigidbody self;
    protected Transform carrier;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Rigidbody>();
        if(self == null)
        {
            Debug.Log("Can't Find RigidBody - Throwable Object Script");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(carrier != null)
        {
            self.velocity = Vector3.zero;
            self.angularVelocity = Vector3.zero;
            if(!touched)
            {
                transform.localPosition = pickUpPosition;
            }
            else
            {
                transform.position = pinnedPosition;
                if(Vector3.Angle(carrier.forward, lastHitNorm) < Vector3.Angle(lastCamAngle, lastHitNorm))
                {
                    touched = false;
                    transform.parent = carrier;
                }
            }
        }
    }

    //This is for bumping the object into the environment - need help with this
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Ground") && beingCarried)
        {
            touched = true;
            transform.parent = null;
            lastCamAngle = carrier.forward;
            pinnedPosition = transform.position;
            lastHitNorm = collision.GetContact(0).normal;
        }
    }

    public bool PickUp(Transform tran)
    {
        Debug.Log("Picking Up");
        if (isHit)
        {
            transform.parent = tran;
            beingCarried = true;
            self.useGravity = false;
            pickUpPosition = transform.localPosition;
            carrier = tran;
        }
        return isHit;
    }

    public void DropDown()
    {
        Debug.Log("Dropping");
        transform.parent = null;
        beingCarried = false;
        self.useGravity = true;
        //add a throw impulse on drop.
        self.AddForce(carrier.forward * throwForce, ForceMode.Impulse);
        carrier = null;
    }
}

