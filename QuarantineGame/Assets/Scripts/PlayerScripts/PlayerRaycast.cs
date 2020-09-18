using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    public ThrowableObject hitObject;

    public GameObject raycastedObject;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private Image uiCrosshair;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {    
            if (hit.collider.CompareTag("Ball"))
            {
                raycastedObject = hit.collider.gameObject;
                CrosshairActive();
                hitObject = raycastedObject.GetComponent<ThrowableObject>();
                hitObject.isHit = true;

                //NEED HELP IMPLEMENTING THIS - was in throwable object script but can't be on every single interactable object in the game
                /*if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    transform.parent = null;
                    beingCarried = false;
                    GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                }*/
            }
            else
            {
                //CrosshairNormal();
                hitObject.isHit = false;
            }
        }
        else
        {
            if(hitObject != null)
            {
                hitObject.isHit = false;
            }
            CrosshairNormal();
        }
    }

    void CrosshairActive()
    {
        uiCrosshair.color = Color.cyan;
    }

    void CrosshairNormal()
    {
        uiCrosshair.color = Color.white;
    }
}
    

