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

    private bool uiCActive = false;
    private bool isCarrying = false;

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
                var dud = raycastedObject.GetComponent<ThrowableObject>();
                Debug.Log(dud);
                if (hitObject != null && hitObject != dud)
                {
                    hitObject.isHit = false;
                    isCarrying = hitObject.DropDown();
                }
                hitObject = dud;
                hitObject.isHit = true;
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
                isCarrying = hitObject.DropDown();
                hitObject = null;
            }
            CrosshairNormal();
        }

        if(hitObject != null && uiCActive && !isCarrying)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isCarrying = hitObject.PickUp(transform);
            }
        }
        else if(hitObject != null && isCarrying)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isCarrying = hitObject.DropDown();
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
    

