using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool crouch;
  

    public CharacterController controller;
    public Camera playerCam;

    public float walkSpeed = 8f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        //crouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Crouch controls
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = !crouch;
            CheckCrouch();
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * walkSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void CheckCrouch()
    {
        if(crouch == true)
        {
            playerCam.transform.localPosition = new Vector3(0, 0, 0);
            controller.height = 1f;
        }
        else
        {
            //This should be fixed to just change character controller height and not have them jump up like a dumbass
            playerCam.transform.localPosition = new Vector3(0, 1, 0);
            controller.height = 3.8f;
            controller.Move(new Vector3(0, 3, 0));
        }
    }
}
