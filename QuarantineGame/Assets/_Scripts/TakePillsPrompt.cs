using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakePillsPrompt : MonoBehaviour
{
    public GameManager gameManager;
    public CanvasGroup self;

    public void Yes()
    {
        self.alpha = 0;
        self.blocksRaycasts = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(7);
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
