using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadlockScript : MonoBehaviour
{
    public string Key;
    public string QuestKey;
    public Text Display;
    public Rigidbody DoorsBody;
    private bool safeToUse = false;
    private bool locked = true;
    private bool checking = false;
    private string QuestName = "";

    [SerializeField]
    private CanvasGroup Padlock;

    // Start is called before the first frame update
    void Start()
    {
        if (Key != null && DoorsBody != null)
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
            DoorsBody.isKinematic = false;
            yield return new WaitForSecondsRealtime(2.5f);
            QuestName = EventManager.NameFromLoader(QuestKey);
            if(QuestName != "")
            {
                EventManager.OnAddQuestInitiated(QuestName);
                EventManager.OnCompleteQuestInitiated(QuestName);
            }
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
