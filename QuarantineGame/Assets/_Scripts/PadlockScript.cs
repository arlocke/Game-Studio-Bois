using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadlockScript : MonoBehaviour
{
    public string Key;
    public Text Display;
    private bool safeToUse = false;
    private bool locked = true;

    [SerializeField]
    private CanvasGroup Padlock;

    // Start is called before the first frame update
    void Start()
    {
        if(Key != null)
        {
            safeToUse = true;
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            if(Padlock.alpha == 0)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }

    public void buttonPressed(int num)
    {
        if(safeToUse && locked)
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
                    locked = false;
                    StartCoroutine(winlose());
                }
                else
                {
                    Display.text = "####";
                }
            }
        }
    }

    private IEnumerator winlose()
    {
        for(int i = 0; i < 5; i++)
        {
            Display.text = "####";
            yield return new WaitForSeconds(0.5f);
            Display.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        Display.text = "Unlocked";
        yield return new WaitForSeconds(2.5f);
        Close();
    }

    public void Open()
    {
        if (locked && Padlock != null)
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
        if(Padlock != null)
        {
            Time.timeScale = 1;
            Padlock.alpha = 0;
            Padlock.blocksRaycasts = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
