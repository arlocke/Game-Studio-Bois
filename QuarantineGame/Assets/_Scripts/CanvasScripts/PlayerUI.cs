using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //public variables
    public Text innerThoughtsUI;
    public Text questLogUI;

    public void Start()
    {
        EventManager.InnerThought += startInner;
    }

    public void startInner(string ThoughtText, float DelayTime)
    {
        StartCoroutine(InnerThought(ThoughtText, DelayTime));
    }

    public IEnumerator InnerThought(string ThoughtText, float DelayTime)
    {
        innerThoughtsUI.text = ThoughtText;
        innerThoughtsUI.enabled = true;
        yield return new WaitForSeconds(DelayTime);
        innerThoughtsUI.enabled = false;
    }
    
    public void ActivateQuest(string QuestText)
    {
        questLogUI.text += QuestText + "\n";
    }

}
