﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadlockScript : MonoBehaviour
{
    public string Key;
    public Text Display;
    public Transform Top;
    public Rigidbody TopsBody;
    private bool safeToUse = false;
    private bool locked = true;
    private bool checking = false;

    [SerializeField]
    private CanvasGroup Padlock;

    // Start is called before the first frame update
    void Start()
    {
        if(Key != null && Top != null)
        {
            safeToUse = true;
        }
        else
        {
            Debug.Log("Lock is missing important components to function");
        }
    }

    public void buttonPressed(int num)
    {
        if(safeToUse && locked && !checking)
        {
            if (Display.text != "####")
            {
                Display.text += num;
            }
            else
            {
                Display.text = num.ToString();
            }
            if(Display.text.Length >= Key.Length)
            {
                if(Display.text == Key)
                {
                    checking = true;
                    locked = false;
                    StartCoroutine(winlose(true));
                }
                else
                {
                    checking = true;
                    StartCoroutine(winlose(false));
                }
            }
        }
    }

    private IEnumerator winlose(bool condition)
    {
        for(int i = 0; i < 5; i++)
        {
            Display.text = "####";
            yield return new WaitForSecondsRealtime(0.5f);
            Display.text = "";
            yield return new WaitForSecondsRealtime(0.5f);
        }
        if (condition)
        {
            Display.text = "Unlocked";
            Top.parent = null;
            TopsBody.isKinematic = false;
            TopsBody.useGravity = true;
            yield return new WaitForSecondsRealtime(2.5f);
            Close();
        }
        else
        {
            Display.text = "Incorrect";
            yield return new WaitForSecondsRealtime(2.5f);
            Display.text = "####";
            checking = false;
        }
    }

    public void Open()
    {
        if (locked && Padlock != null && Display != null)
        {
            Time.timeScale = 0;
            Padlock.alpha = 1;
            Padlock.blocksRaycasts = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Close()
    {
        if(Padlock != null && Display != null)
        {
            Time.timeScale = 1;
            Padlock.alpha = 0;
            Padlock.blocksRaycasts = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
