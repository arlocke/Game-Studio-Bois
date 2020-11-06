using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    public string key = "";
    public LoadManager loader;

    public void SetComplete()
    {
        if(loader != null)
        {
            if (loader.checkDictionary(key))
            {
                EventManager.OnCompleteQuestInitiated(loader.getQuestName(key));
            }
        }
    }
}
