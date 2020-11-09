using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct QuestHolder
{
    public string key;
    public string questName;
}

public class LoadManager : MonoBehaviour
{
    public QuestHolder[] QuestEditor;
    private Dictionary<string, string> Quests = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Awake()
    {
        if(QuestEditor != null)
        {
            foreach (var quest in QuestEditor)
            {
                Quests.Add(quest.key, quest.questName);
            }
            EventManager.questSize = Quests.Count;
        }
        DestroyedObjectManager.Initiate();
    }

    private void Start()
    {
        EventManager.GetName += getQuestName;
        if (PlayerPrefs.GetInt("Load", 0) == 1)
        {
            EventManager.OnLoadInitiated();
        }
    }

    public bool checkDictionary(string key)
    {
        if(Quests.ContainsKey(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void getQuestName(string key)
    {
        if(checkDictionary(key))
        {
            EventManager.name = Quests[key];
        }
        else
        {
            EventManager.name = "";
        }
    }
}
