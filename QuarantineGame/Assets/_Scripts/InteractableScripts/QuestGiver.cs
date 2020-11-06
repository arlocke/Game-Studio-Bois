﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    //public variables
    public string key = "";
    public bool isHit = false;
    public bool activated = false; //if quest has already been discovered
    //public Text questLogUI; //Click and drag the QuestLog from the FPSPlayer -> Canvas -> QuestLog


    public void UpdateQuestLog()
    {
        //Debug.Log("Updating Quest Log SIR");
        activated = true;

        string dud = EventManager.NameFromLoader(key);
        if (!dud.Equals(""))
        {
            EventManager.OnAddQuestInitiated(dud);
        }
        gameObject.SetActive(false);
    }

    //Some bullshit with giving time constraints and flags for quest completion and all that complicated junk
}
