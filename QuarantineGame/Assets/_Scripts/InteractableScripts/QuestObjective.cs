using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    public string key = "";
    public bool disabling = false;
    //public LoadManager loader;

    public void Awake()
    {
        EventManager.DelayedLoad += DelayedLoad;
    }

    public void SetComplete()
    {
        string dud = EventManager.NameFromLoader(key);
        if(!dud.Equals("") && dud != null)
        {
            EventManager.OnCompleteQuestInitiated(dud);
            if(EventManager.OnQuestCheck(dud))
            {
                transform.gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.DelayedLoad -= DelayedLoad;
    }

    public void DelayedLoad(string questList)
    {
        string dud = EventManager.NameFromLoader(key);
        if (!dud.Equals("") && dud != null)
        {
            if (EventManager.OnQuestCheck(dud) && disabling)
            {
                transform.gameObject.SetActive(false);
            }
        }
    }
}
