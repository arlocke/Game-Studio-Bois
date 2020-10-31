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
    public string currentRoom; //Placeholder string for room the player is currently in

    //Public Classes
    public GameManager gameManager;
    public ThrowableObject hitThrowable;
    public Lookable hitLookable;
    public QuestGiver hitQuestGiver;
    public GameObject raycastedObject;
    public Text innerThoughtsUI;
    public PlayerUI playerUI;
   

    //Private Serialized Fields
    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Image uiCrosshair;

    private void Start()
    {
        EventManager.LoadInitiated += DropAll;
    }

    // Update is called once per frame
    void Update()
    {
        //If not carrying, then run raycast.
        if(!isCarrying)
        {
            RaycastHit hit; //Create Temporary raycast variable
            Vector3 fwd = transform.TransformDirection(Vector3.forward); //Take Forward Vector 3

            //Run Raycast
            if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
            {
                raycastedObject = hit.collider.gameObject; //Store hit object.
                CrosshairActive(); //Active if hit object within layer mask.

                //If Throwable
                if (hit.collider.CompareTag("Throwable"))
                {
                    //Debug.Log("Throwable");

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
                else if (hit.collider.CompareTag("Lookable"))
                {
                    hitThrowable = null;

                    var dud = raycastedObject.GetComponent<Lookable>();

                    if (hitLookable != null && hitLookable != dud)
                    {
                        hitLookable.isHit = false;
                    }

                    hitLookable = dud;

                    hitLookable.isHit = true;

                    if (hitLookable != null && uiCActive)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            isCarrying = hitLookable.PickUp(transform);
                        }
                    }
                }
                else if (hit.collider.CompareTag("Objective"))
                {
                    hitLookable = null;
                    hitThrowable = null;

                    CrosshairActive();
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Grabbin Pills");
                        //innerThoughtsUI.text = "Got my pills!!!!";
                        StartCoroutine(playerUI.InnerThought("Here are my pills", 10.0f));
                        // add playerUI script here
                        havePills = true;
                    }
                }
                else if (hit.collider.CompareTag("Bed"))
                {
                    hitLookable = null;
                    hitThrowable = null;

                    CrosshairActive();
                    if (Input.GetMouseButtonDown(0))
                    {
                        gameManager.CompleteDay();
                    }

                }
                else if (hit.collider.CompareTag("QuestGiver"))
                {
                    hitLookable = null;
                    hitThrowable = null;

                    var dud = raycastedObject.GetComponent<QuestGiver>();

                    if (hitQuestGiver != null && hitQuestGiver != dud)
                    {
                        hitQuestGiver.isHit = false;
                    }

                    hitQuestGiver = dud;

                    hitQuestGiver.isHit = true;

                    if (hitQuestGiver != null && uiCActive)
                    {
                        if (Input.GetMouseButton(0) && !hitQuestGiver.activated)
                        {
                            Debug.Log("Give a quest");
                            hitQuestGiver.UpdateQuestLog();
                        }
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
                if(hitLookable != null)
                {
                    hitLookable.isHit = false;
                    hitLookable = null;
                }
                if (hitQuestGiver != null)
                {
                    hitQuestGiver.isHit = false;
                    hitQuestGiver = null;
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

    void DropAll()
    {
        if (hitThrowable != null)
        {
            hitThrowable.DropDown();
        }
        if (hitLookable != null)
        {
            hitLookable.DropDown();
        }
        isCarrying = false;
    }
}
    

