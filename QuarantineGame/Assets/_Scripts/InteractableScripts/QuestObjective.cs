using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    public string key = "";
    //public LoadManager loader;

    public void SetComplete()
    {
        string dud = EventManager.NameFromLoader(key);
        if(!dud.Equals(""))
        {
            EventManager.OnCompleteQuestInitiated(dud);
        }
    }
}
