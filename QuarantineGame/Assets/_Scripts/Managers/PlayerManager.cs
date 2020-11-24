﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Public Gameplay Necessary Variables
    //public bool havePills = false; //boolean for if the player picked up the pills
    //public string currentRoom; //Placeholder string for room the player is currently in

    //Public Variables:
    public float walkSpeed = 8f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float crouchHeight = 1.0f;
    public float pushPower = 2.0f; // Allows for pushing objects with the character controller!

    //Public Classes:
    public CharacterController controller;
    public Camera playerCam;
    public LayerMask groundMask;  //PLEASE SET TO GROUND OR ELSE Y GRAVITY IS GOING TO INFINITELY INCREASE. PLEASE.
    public Transform groundCheck;
    public MouseLook camScript;

    //Private Variables:
    protected float xAxis = 0.0f;
    protected float zAxis = 0.0f;
    protected float yVeloctiy = 0.0f;
    protected float defHeight;
    protected int tutorialStage = 0;
    public bool crouch = false;
    protected bool isGrounded = false;
    protected bool isTutorial = false;
    protected bool tutorialPickup = false;
    protected bool seized = false;

    //Private Classes:
    protected Transform body;
    protected Vector3 CameraOrigin;
    //protected Vector3 velocity = new Vector3(0,0,0);  //No need to add this tbh, if x/z movement won't vary then there's no reason to use a class to hold a float.

    private void Awake()
    {
        //Add Save to Events;
        EventManager.SaveInitiated += Save;
        EventManager.LoadInitiated += Load;
        EventManager.StartTutorial += StartTutorial;
        EventManager.AddQuest += PickedUp;
        EventManager.Seize += seizing;
    }

    void Start()
    {
        body = transform.GetComponent<Transform>();
        if (body == null)
        {
            Debug.Log("Can't find Transform - PlayerMovement Script");
        }
        defHeight = controller.height;
        CameraOrigin = playerCam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Control Input For Movement.
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");

        //Control Input For Crouching.
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CheckCrouch();
        }

        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    EventManager.FastForward(250);
        //}

        if(isTutorial)
        {
            if(tutorialStage > 0)
            {
                if (!seized)
                {
                    Move();
                }
            }
            Tutorial();
        }
        else
        {
            if(!seized)
            {
                Move();
            }
        }
    }

    private void CheckCrouch()
    {
        if (!crouch)
        {
            crouch = true;
            controller.height = crouchHeight;
            controller.enabled = false; //Need to disable controller to adjust position directly.
            body.transform.position -= (Vector3.up * ((defHeight - crouchHeight) / 2));
            controller.enabled = true;
            playerCam.transform.position -= (Vector3.up * ((defHeight - crouchHeight) / 4));
        }
        else
        {
            Ray ray = new Ray();
            RaycastHit hit;
            ray.origin = transform.position;
            ray.direction = Vector3.up;
            if (!Physics.Raycast(ray, out hit, defHeight))
            {
                crouch = false;
                controller.height = defHeight;
                controller.enabled = false;
                body.transform.position += (Vector3.up * ((defHeight - crouchHeight) / 2));
                controller.enabled = true;
                playerCam.transform.localPosition = CameraOrigin;
            }
            else
            {
                Debug.Log("Not enough space to stand up!");
            }
        }
    }

    private void Save()
    {
        SaveLoad.SavePlayer(this);
    }

    private void Load()
    {
        controller.enabled = false;
        PlayerData data = SaveLoad.LoadPlayer();
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        Vector3 rotation;
        rotation.x = data.rotation[0];
        rotation.y = data.rotation[1];
        rotation.z = data.rotation[2];
        camScript.setY(rotation.y);
        transform.position = position;
        controller.enabled = true;
    }

    //This allows the controller to hit things which are rigid bodies!!!
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) { return; }
        if (hit.moveDirection.y < -0.3) { return; }
        var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        //body.velocity = pushDir * pushPower;
        body.AddForce(pushDir * pushPower, ForceMode.Impulse);
    }

    private void OnDestroy()
    {
        EventManager.SaveInitiated -= Save;
        EventManager.LoadInitiated -= Load;
        EventManager.StartTutorial -= StartTutorial;
        EventManager.AddQuest -= PickedUp;
        EventManager.Seize -= seizing;
    }

    private void StartTutorial()
    {
        isTutorial = true;
        tutorialStage = 0;
        if(!crouch)
        {
            CheckCrouch();
        }
        EventManager.OnInnerThoughtInitiated("Ugh I'm so tired... I gotta get outta bed... (Use [LeftCtrl] to get up)", 7.0f);
    }

    private void PickedUp(string filler)
    {
        tutorialPickup = true;
        EventManager.AddQuest -= PickedUp;
    }

    private void Tutorial()
    {
        if(tutorialStage == 2 && tutorialPickup)
        {
            isTutorial = false;
            EventManager.OnInnerThoughtInitiated("Looks like I'm all set for the day. (Tutorial Done)", 10.0f);
            EventManager.OnEndTutorial();
        }
        if(tutorialStage == 1 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)))
        {
            tutorialStage += 1;
            EventManager.OnInnerThoughtInitiated("I better check my bulletin board for my daily tasks... (Click on Sticky Notes to receive quests)", 10.0f);
        }
        if(tutorialStage == 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            tutorialStage += 1;
            EventManager.OnInnerThoughtInitiated("Time to get a move on... (Use WASD to walk around)", 7.0f);
        }
    }

    private void Move()
    {
        //HANDLING X/Z AXIS.
        //Create move to consider X and Z axis.
        Vector3 move = transform.right * xAxis + transform.forward * zAxis;

        //Use Vector 3 to translate movement * speed multiplier. Seperated from Frame Rate.
        controller.Move(move * walkSpeed * Time.deltaTime);

        //HANDLING Y AXIS.
        //Add Gravity Acceleration:
        yVeloctiy += gravity * Time.deltaTime;

        //isGrounded Check. Checks a sphere at feet.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log(isGrounded);

        //if grounded, set y velocity to a more reasonable rate.
        if (isGrounded && yVeloctiy < 0)
        {
            yVeloctiy = -2f;
        }
        //Debug.Log(yVeloctiy);

        //Change move to consider only the Y axis.
        move = transform.up * yVeloctiy;

        //Move on the Y Axis.
        controller.Move(move * Time.deltaTime);
    }

    private void seizing(bool facts)
    {
        seized = facts;
    }
}
