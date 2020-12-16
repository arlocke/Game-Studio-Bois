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
    public DoorScript DoorScript;
    public bool scriptUnlock = false;
    private bool safeToUse = false;
    private bool locked = true;
    private bool checking = false;
    private bool open = false;
    private string QuestName = "";
    public bool audioOnUnlock = false;
    public AudioSource audioPlay;

    [SerializeField]
    private CanvasGroup Padlock;

    public void Awake()
    {
        EventManager.DelayedLoad += DelayedLoad;
    }

    // Start is called before the first frame update
    void Start()
    {
        if ((Key != null && DoorsBody != null) || (DoorScript != null && scriptUnlock))
        {
            safeToUse = true;
        }
        else
        {
            Debug.Log("Lock is missing important components to function");
        }
        if (audioOnUnlock)
        {
            if (audioPlay == null)
            {
                safeToUse = false;
            }
            else
            {
                Debug.Log("Lock is audio based and has required audio source");
            }
        }
    }

    public void buttonPressed(int num)
    {
        if(safeToUse && locked && !checking && open && Display.text != "Unlocked")
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
        for(int i = 0; i < 3; i++)
        {
            Display.text = "####";
            yield return new WaitForSecondsRealtime(0.2f);
            Display.text = "";
            yield return new WaitForSecondsRealtime(0.2f);
        }
        if (condition)
        {
            Display.text = "Unlocked";
            if(!scriptUnlock)
            {
                DoorsBody.isKinematic = false;
            }
            else
            {
                DoorScript.isLocked = false;
            }
            
            if(audioOnUnlock)
            {
                audioPlay.Play();
            }

            yield return new WaitForSecondsRealtime(1f);
            if(QuestKey != "")
            {
                QuestName = EventManager.NameFromLoader(QuestKey);
            }
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
            yield return new WaitForSecondsRealtime(1f);
            Display.text = "####";
            checking = false;
        }
    }

    public void Open()
    {
        if (Padlock != null && Display != null)
        {
            Time.timeScale = 0;
            Padlock.alpha = 1;
            Padlock.blocksRaycasts = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            open = true;
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
            open = false;
        }
    }

    private void OnDestroy()
    {
        EventManager.DelayedLoad -= DelayedLoad;
    }

    public void DelayedLoad(string questList)
    {
        string dud = EventManager.NameFromLoader(QuestKey);
        if (!dud.Equals("") && dud != null)
        {
            if (EventManager.OnQuestCheck(dud))
            {
                Display.text = "Unlocked";
                if (!scriptUnlock)
                {
                    locked = false;
                    DoorsBody.isKinematic = false;
                }
                else
                {
                    locked = false;
                    DoorScript.isLocked = false;
                }
            }
        }
    }
}
