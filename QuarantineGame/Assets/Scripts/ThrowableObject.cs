using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowableObject : MonoBehaviour
{
    public float throwForce = 10;
    public bool isHit = false;

    protected bool beingCarried = false;
    protected bool touched = false;

    protected Rigidbody self;

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
    void Update()
    {
    }

    //This is for bumping the object into the environment - need help with this
    void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
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
        }
        return isHit;
    }

    public bool DropDown()
    {
        Debug.Log("Dropping");
        transform.parent = null;
        beingCarried = false;
        self.useGravity = true;
        return false;
    }
}

