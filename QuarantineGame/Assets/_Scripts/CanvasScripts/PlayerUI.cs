using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //public variables
    public Text innerThoughtsUI;
    public Text questLogUI;

    public Animator fadeInInnerThoughts;

    public void Start()
    {
        Debug.Log(transform.gameObject.name);
        EventManager.AddQuest += ActivateQuestUI;
        EventManager.CompleteQuest += CompleteQuestUI;
        EventManager.InnerThought += startInner;

        if (innerThoughtsUI != null)
        {
            fadeInInnerThoughts = innerThoughtsUI.GetComponent<Animator>();
        }
    }

    public void startInner(string ThoughtText, float DelayTime)
    {
        StartCoroutine(InnerThought(ThoughtText, DelayTime));
    }

    public IEnumerator InnerThought(string ThoughtText, float DelayTime)
    {
        bool isActivated = fadeInInnerThoughts.GetBool("activated");
        //fadeInInnerThoughts.SetBool("activated", true);
        innerThoughtsUI.text = ThoughtText;
        innerThoughtsUI.enabled = true;
        yield return new WaitForSeconds(DelayTime);
        //fadeInInnerThoughts.SetBool("activated", false);
        yield return new WaitForSeconds(1.0f);
        innerThoughtsUI.enabled = false;
    }

    public void ActivateQuestUI(string QuestText)
    {
        //Debug.Log("Creating text SIR");
        questLogUI.text += QuestText + "\n";
    }

    public void CompleteQuestUI(string QuestText)
    {
        if(questLogUI.text.Contains(QuestText) && !questLogUI.text.Contains(QuestText + " - Completed"))
        {
            Debug.Log("Replacing Found");
            questLogUI.text = questLogUI.text.Replace(QuestText, QuestText + " - Completed");
        }
    }
}
