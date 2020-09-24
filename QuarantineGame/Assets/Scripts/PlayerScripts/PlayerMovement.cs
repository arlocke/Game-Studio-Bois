using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Public Variables:
    public float walkSpeed = 8f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float crouchHeight = 1.0f;

    //Public Classes:
    public CharacterController controller;
    public Camera playerCam;
    public LayerMask groundMask;  //PLEASE SET TO GROUND OR ELSE Y GRAVITY IS GOING TO INFINITELY INCREASE. PLEASE.
    public Transform groundCheck;

    //Private Variables:
    protected float xAxis = 0.0f;
    protected float zAxis = 0.0f;
    protected float yVeloctiy = 0.0f;
    protected float defHeight;
    public bool crouch = false;
    protected bool isGrounded = false;

    //Private Classes:
    protected Transform body;
    protected Vector3 CameraOrigin;
    //protected Vector3 velocity = new Vector3(0,0,0);  //No need to add this tbh, if x/z movement won't vary then there's no reason to use a class to hold a float.
    

    void Start()
    {
        body = transform.GetComponent<Transform>();
        if(body == null)
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

    private void CheckCrouch()
    {
        if(!crouch)
        {
            crouch = true;
            controller.height = crouchHeight;
            controller.enabled = false; //Need to disable controller to adjust position directly.
            body.transform.position -= (Vector3.up * ((defHeight-crouchHeight)/2));
            controller.enabled = true;
            playerCam.transform.position -= (Vector3.up * ((defHeight - crouchHeight) / 4));
        }
        else
        {
            Ray ray = new Ray();
            RaycastHit hit;
            ray.origin = transform.position;
            ray.direction = Vector3.up;
            if(!Physics.Raycast(ray, out hit, defHeight))
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
    //void CheckCrouch()
    //{
    //    if(crouch == true)
    //    {
    //        playerCam.transform.localPosition = new Vector3(0, 0, 0);
    //        controller.height = 1f;
    //    }
    //    else
    //    {
    //        //This should be fixed to just change character controller height and not have them jump up like a dumbass
    //        playerCam.transform.localPosition = new Vector3(0, 1, 0);
    //        controller.height = 3.8f;
    //        //controller.Move(new Vector3(0, 3, 0));
    //    }
    //}
}
