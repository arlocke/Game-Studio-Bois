using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    //public variables
    public bool isHit = false;
    public bool activated = false; //if quest has already been discovered
    public string QuestText;
    //public Text questLogUI; //Click and drag the QuestLog from the FPSPlayer -> Canvas -> QuestLog
    public PlayerUI playerUI;
    


    public void UpdateQuestLog()
    {
        activated = true;
        playerUI.ActivateQuest(QuestText);
        transform.localScale = new Vector3(0, 0, 0);
    }

    //Some bullshit with giving time constraints and flags for quest completion and all that complicated junk
}
