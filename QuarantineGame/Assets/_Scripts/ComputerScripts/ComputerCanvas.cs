﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerCanvas : MonoBehaviour
{
    enum RenderModeStates { camera, world };
    RenderModeStates m_RenderModeStates;

    Canvas computerScreen;
    public Text emailText;
    public int currentEmail = 0;

    public List<string> emailsInInbox = new List<string>();

    private Vector3 originalPos;
    private Quaternion originalRot;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        computerScreen = GetComponent<Canvas>();
        EventManager.AddEmail += StartAddEmail;

        originalPos = transform.position;
        originalScale = transform.localScale;
        originalRot = transform.localRotation;
    }

    public void StartAddEmail(string Email)
    {
        emailsInInbox.Add(Email);
        emailText.text = Email;
        currentEmail = emailsInInbox.Count - 1;
    }

    public void ChangeEmail()
    {
        if(emailsInInbox.Count > 0)
        {
            currentEmail += 1;
            Debug.Log("trying to change email");

            if (currentEmail >= emailsInInbox.Count)
            {
                currentEmail = 0;
            }

            emailText.text = emailsInInbox[currentEmail];
        }
    }

    private void OnDestroy()
    {
        EventManager.AddEmail -= StartAddEmail;
    }

    public void SitAtComputer()
    {
        if(computerScreen.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            computerScreen.renderMode = RenderMode.ScreenSpaceOverlay;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void LeaveComputer()
    {
        if (computerScreen.renderMode != RenderMode.WorldSpace)
        {
            computerScreen.renderMode = RenderMode.WorldSpace;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            transform.position = originalPos;
            transform.localScale = originalScale;
            transform.localRotation = originalRot;
        }
    }

}
