using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public string QuestList;

    public QuestData(string quests)
    {
        QuestList += quests;
    }
}
