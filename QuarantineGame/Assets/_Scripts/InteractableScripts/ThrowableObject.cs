using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowableObject : MonoBehaviour
{
    public float throwForce = 10;
    //public float angleForgiveness = 5.0f;
    public float maxDistance = 1.0f;
    public float maxSpeed = 500.0f;
    public float minSpeed = 0.0f;
    public float rotationSpeed = 100.0f;
    public bool isHit = false;
    public bool returnGravity = true;

    protected float disToCarrier = 0.0f;
    protected float minDisToCarrier = 0.0f;
    protected float maxDisToCarrierExt = 2.5f;
    protected float currentDistance = 0.0f;
    protected float currentSpeed = 0.0f;
    protected bool beingCarried = false;
    //protected bool touched = false;

    //protected Vector3 lastCamAngle;
    //protected Vector3 lastHitNorm;
    //protected Vector3 pinnedPosition;
    protected Rigidbody self;
    protected Transform carrier;
    protected Collider carrierCollider;
    //protected Quaternion lookRot;

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
            disToCarrier += Input.mouseScrollDelta.y;
            if(disToCarrier < minDisToCarrier)
            {
                disToCarrier = minDisToCarrier;
            }
            else if(disToCarrier > (minDisToCarrier + maxDisToCarrierExt))
            {
                disToCarrier = minDisToCarrier + maxDisToCarrierExt;
            }
            Vector3 temp = carrier.position + (carrier.forward * disToCarrier);
            currentDistance = Vector3.Distance(temp, self.position);
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDistance / maxDistance);
            currentSpeed *= Time.fixedDeltaTime;
            Vector3 direction = temp - self.position;
            self.velocity = direction.normalized * currentSpeed;
            self.angularVelocity = Vector3.zero;
            //lookRot = Quaternion.LookRotation(carrier.position - self.position);
            //lookRot = Quaternion.Slerp(carrier.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
            //self.MoveRotation(lookRot);
            //if(!touched)
            //{
            //    transform.localPosition = pickUpPosition;
            //}
            //else
            //{
            //    transform.position = pinnedPosition;
            //    if(Vector3.Angle(carrier.forward, lastHitNorm) < Vector3.Angle(lastCamAngle, lastHitNorm))
            //    {
            //        touched = false;
            //        transform.parent = carrier;
            //    }
            //}
        }
    }

    //This is for bumping the object into the environment - need help with this
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.transform.CompareTag("Ground") && beingCarried)
    //    {
    //        touched = true;
    //        transform.parent = null;
    //        lastCamAngle = carrier.forward;
    //        pinnedPosition = transform.position;
    //        lastHitNorm = collision.GetContact(0).normal;
    //    }
    //}

    public bool PickUp(Transform tran, Collider playCol)
    {
        Debug.Log("Picking Up");
        if (isHit)
        {
            //transform.parent = tran;
            carrier = tran;
            carrierCollider = playCol;
            Physics.IgnoreCollision(carrierCollider, transform.GetComponent<Collider>(), true);
            beingCarried = true;
            self.useGravity = false;
            disToCarrier = Mathf.Abs(Vector3.Distance(carrier.position, transform.position));
            minDisToCarrier = disToCarrier;
        }
        return isHit;
    }

    public void DropDown()
    {
        Physics.IgnoreCollision(carrierCollider, transform.GetComponent<Collider>(), false);
        disToCarrier = 0.0f;
        Debug.Log("Dropping");
        //transform.parent = null;
        beingCarried = false;
        if(returnGravity)
        {
            self.useGravity = true;
        }
        carrier = null;
        carrierCollider = null;
    }

    public void ThrowDown()
    {
        Physics.IgnoreCollision(carrierCollider, transform.GetComponent<Collider>(), false);
        disToCarrier = 0.0f;
        Debug.Log("Dropping");
        //transform.parent = null;
        beingCarried = false;
        self.useGravity = true;
        //add a throw impulse on drop.
        self.AddForce(carrier.forward * throwForce, ForceMode.Impulse);
        carrier = null;
        carrierCollider = null;
    }
}

