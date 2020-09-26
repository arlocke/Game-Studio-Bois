using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    public GameManager gameManager;

    public ThrowableObject hitObject;

    public GameObject raycastedObject;

    public Text innerThoughtsUI;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private Image uiCrosshair;

    public bool uiCActive = false;
    public bool isCarrying = false;

    //boolean for if the player picked up the pills
    public bool havePills = false;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            raycastedObject = hit.collider.gameObject;
            CrosshairActive();

            //This is for objects that can be pickedup/thrown but not put into inventory or inspected
            if (hit.collider.CompareTag("Throwable"))
            {
                ////////////////////////////////
                ///I MOVED THIS HERE vvvvvvvvvvvvvvvvvvvvvvv
                ////////////////////////////////
                Debug.Log("Throwable");
                var dud = raycastedObject.GetComponent<ThrowableObject>();
                //Debug.Log(dud);
                if (hitObject != null && hitObject != dud)
                {
                    hitObject.isHit = false;
                    //isCarrying = hitObject.DropDown();
                }
                hitObject = dud;
                hitObject.isHit = true;
                


                
                if (hitObject != null && uiCActive && !isCarrying)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        isCarrying = hitObject.PickUp(transform);
                    }
                }
                else if (hitObject != null && isCarrying)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        isCarrying = hitObject.DropDown();
                    }
                }
                ////////////////////////////////
                ///I MOVED THIS HERE ^^^^^^^^^^^^^^^^^^^^^^^
                ////////////////////////////////
            }

            else if (hit.collider.CompareTag("Objective"))
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
           
            //else  //I CHANGED THIS LEMME KNOW IF THIS IS BAD
            //{
                //CrosshairNormal();
                //hitObject.isHit = false;
            //}
        }
        else
        {
            if (hitObject != null)
            {
                hitObject.isHit = false;
                //isCarrying = hitObject.DropDown();
                hitObject = null;
            }
            CrosshairNormal();
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
    

