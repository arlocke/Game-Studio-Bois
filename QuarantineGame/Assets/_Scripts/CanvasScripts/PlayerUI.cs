using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    //public variables
    public Text innerThoughtsUI;
    public Text questLogUI;
    public GameObject blackout;

    public Animator fadeInInnerThoughts;
    public Animator blackoutAnim;

    public void Start()
    {
        Debug.Log(transform.gameObject.name);
        EventManager.AddQuest += ActivateQuestUI;
        EventManager.CompleteQuest += CompleteQuestUI;
        EventManager.QuestCheck += IsQuestCompleted;
        EventManager.InnerThought += startInner;
        EventManager.GetCompletion += CheckCompletion;
        EventManager.Blackout += PlayBlackout;
        EventManager.BlackoutReverse += PlayBlackoutReverse;
        EventManager.ContainedCheck += CheckQuestContained;


        if (innerThoughtsUI != null)
        {
            fadeInInnerThoughts = innerThoughtsUI.GetComponent<Animator>();
        }
        if(blackout != null)
        {
            blackoutAnim = blackout.GetComponent<Animator>();
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
        //yield return new WaitForSeconds(1.0f);
        innerThoughtsUI.enabled = false;
        if(EventManager.ending)
        {
            if(EventManager.completed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }
    }

    public void ActivateQuestUI(string QuestText)
    {
        //Debug.Log("Creating text SIR");
        if(!questLogUI.text.Contains(QuestText))
        {
            questLogUI.text += QuestText + "\n";
        }
        /*if(questLogUI.text.Contains("Work") && !questLogUI.text.Contains("Work - Completed"))
        {
            questLogUI.text = questLogUI.text.Replace("Work", "Work - Completed");
        }*/
    }

    public void CompleteQuestUI(string QuestText)
    {
        if(questLogUI.text.Contains(QuestText) && !questLogUI.text.Contains(QuestText + " - Completed"))
        {
            Debug.Log("Replacing Found");
            questLogUI.text = questLogUI.text.Replace(QuestText, QuestText + " - Completed");
        }
    }

    public void CheckQuestContained(string QuestText)
    {
        if(questLogUI.text.Contains(QuestText))
        {
            EventManager.isQuestContained = true;
        }
        else
        {
            EventManager.isQuestContained = false;
        }
    }

    public void IsQuestCompleted(string QuestText)
    {
        if(questLogUI.text.Contains(QuestText + " - Completed"))
        {
            EventManager.isQuestCompleted = true;
        }
        else
        {
            EventManager.isQuestCompleted = false;
        }
    }

    public void CheckCompletion()
    {
        int dud = Regex.Matches(questLogUI.text, "Completed").Count;
        Debug.Log(dud);
        if (dud == EventManager.questSize)
        {
            EventManager.completed = true;
        }
    }

    public void PlayBlackout()
    {
        //Debug.Log("Trying to play");
        blackoutAnim.SetBool("Activated", true);

    }

    public void PlayBlackoutReverse()
    {
        //Debug.Log("Trying to play");
        blackoutAnim.SetBool("Activated", false);
    }

    private void OnDestroy()
    {
        EventManager.AddQuest -= ActivateQuestUI;
        EventManager.CompleteQuest -= CompleteQuestUI;
        EventManager.QuestCheck -= IsQuestCompleted;
        EventManager.InnerThought -= startInner;
        EventManager.GetCompletion -= CheckCompletion;
        EventManager.Blackout -= PlayBlackout;
        EventManager.BlackoutReverse -= PlayBlackoutReverse;
        EventManager.ContainedCheck -= CheckQuestContained;
    }
}
