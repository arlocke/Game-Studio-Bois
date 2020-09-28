using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    //Public Variables
    public bool uiCActive = false; //Currently always true?
    public bool isCarrying = false; //Is currently carrying an object!
    public bool havePills = false; //boolean for if the player picked up the pills

    //Public Classes
    public GameManager gameManager;
    public ThrowableObject hitThrowable;
    public Lookable hitLookable;
    public GameObject raycastedObject;
    public Text innerThoughtsUI;

    //Private Serialized Fields
    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Image uiCrosshair;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; //Create Temporary raycast variable
        Vector3 fwd = transform.TransformDirection(Vector3.forward); //Take Forward Vector 3

        //If not carrying, then run raycast.
        if(!isCarrying)
        {
            //Run Raycast
            if(Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
            {
                raycastedObject = hit.collider.gameObject; //Store hit object.
                CrosshairActive(); //Active if hit object within layer mask.

                //If Throwable
                if(hit.collider.CompareTag("Throwable"))
                {
                    Debug.Log("Throwable");

                    hitLookable = null;

                    //Grab throwable script.
                    var dud = raycastedObject.GetComponent<ThrowableObject>();

                    //If you already have hit a throwable, and you are looking at a different throwable, the last throwable seen is no longer hit.
                    if (hitThrowable != null && hitThrowable != dud)
                    {
                        hitThrowable.isHit = false;
                        //isCarrying = hitObject.DropDown();
                    }

                    //Save the hit throwable.
                    hitThrowable = dud;

                    //Tell throwable it is hit.
                    hitThrowable.isHit = true;

                    //If you have hit a throwable (Preventing bugs) and uiCActive is true (Which it always is...)
                    if (hitThrowable != null && uiCActive)
                    {
                        //Pick up object.
                        if (Input.GetMouseButtonDown(0))
                        {
                            isCarrying = hitThrowable.PickUp(transform);
                        }
                    }
                }
                else if(hit.collider.CompareTag("Lookable"))
                {
                    hitThrowable = null;

                    var dud = raycastedObject.GetComponent<Lookable>();

                    if(hitLookable != null && hitLookable != dud)
                    {
                        hitLookable.isHit = false;
                    }

                    hitLookable = dud;

                    hitLookable.isHit = true;

                    if(hitLookable != null && uiCActive)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            isCarrying = hitLookable.PickUp(transform);
                        }
                    }
                }
                else if(hit.collider.CompareTag("Objective"))
                {
                    CrosshairActive();
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Grabbin Pills");
                        innerThoughtsUI.text = "Got my pills!!!!";
                        havePills = true;
                    }
                }
                else if (hit.collider.CompareTag("Bed"))
                {
                    CrosshairActive();
                    if (Input.GetMouseButtonDown(0))
                    {
                        gameManager.CompleteDay();
                    }

                }
            }
            else
            {
                if (hitThrowable != null)
                {
                    hitThrowable.isHit = false;
                    //isCarrying = hitObject.DropDown();
                    hitThrowable = null;
                }
                CrosshairNormal();
            }
        }
        else //If carrying object, see if you want to drop it and disable raycast until dropped.
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (hitThrowable != null)
                {
                    hitThrowable.ThrowDown();
                    isCarrying = false;
                }
            }
            if(Input.GetMouseButtonDown(1))
            {
                if(hitThrowable != null)
                {
                    hitThrowable.DropDown();
                }
                if(hitLookable != null)
                {
                    hitLookable.DropDown();
                }
                isCarrying = false;
            }
        }
    }

    void CrosshairActive()
    {
        uiCActive = true;
        uiCrosshair.color = Color.cyan;
    }

    void CrosshairNormal()
    {
        uiCActive = true;
        uiCrosshair.color = Color.white;
    }
}
    

