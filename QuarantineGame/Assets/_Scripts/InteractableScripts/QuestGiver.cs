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
    public Text questLogUI; //Click and drag the QuestLog from the FPSPlayer -> Canvas -> QuestLog

    public Vector3 face;

    /*
    public void Start()
    {
        face = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }*/


    public void UpdateQuestLog()
    {
        activated = true;
        questLogUI.text +=   QuestText + "\n";
        //transform.position = Vector3.MoveTowards(transform.position, face, 10 * Time.deltaTime); tried to animate the sticky note flying towards the camera before being turned invisible
        transform.localScale = new Vector3(0, 0, 0);
    }

    //Some bullshit with giving time constraints and flags for quest completion and all that complicated junk
}
