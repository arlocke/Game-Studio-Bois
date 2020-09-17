using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowableObject : MonoBehaviour
{

    // This is bad programming
    public Transform player;
    public Transform playerCam;
    // This is bad programming

    public float throwForce = 10;
    bool hasPlayer = false;
    bool beingCarried = false;
    private bool touched = false;
    public bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        // This is bad programming
        this.player = GameObject.FindWithTag("Player").transform;
        this.playerCam = GameObject.FindWithTag("MainCamera").transform;
        // This is bad programming
    }

    // Update is called once per frame
    void Update()
    {
        //float distToPlayer = Vector3.Distance(gameObject.transform.position, player.position);

        //THIS NEEDS TO BE PUT IN PLAYER RAYCAST
        if (Input.GetMouseButtonDown(0) && isHit)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
        }

        
        if (beingCarried)
        {
            //This is for bumping into objects while being carried
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
            }
        }
    }

    //This is for bumping the object into the environment - need help with this
    void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }
}

