using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepPrompt : MonoBehaviour
{
    public GameManager gameManager;
    public CanvasGroup self;

    public void Yes()
    {
        self.alpha = 0;
        self.blocksRaycasts = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (EventManager.OnQuestCheck("Pills") == true)
        {
            if (EventManager.EndingType())
            {
                EventManager.ending = true;
                EventManager.Seize(true);
                EventManager.OnEnd();
            }
            else
            {
                EventManager.OnInnerThoughtInitiated("I've taken my medicine, but I still have stuff to do...", 6.0f, 97, false);
            }
        }
        else
        {
            EventManager.OnInnerThoughtInitiated("I need to take my pills first...", 6.0f, 97, false);
        }
            
    }

    public void YesOnLastDay()
    {
        self.alpha = 0;
        self.blocksRaycasts = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(9);
    }

    public void No()
    {
        self.alpha = 0;
        self.blocksRaycasts = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Open()
    {
        self.alpha = 1;
        self.blocksRaycasts = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
