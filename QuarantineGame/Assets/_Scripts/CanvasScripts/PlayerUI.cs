﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

struct Thought
{
    public int priority;
    public float time;
    public string message;
    public bool overWritable;
    public Thought(int p, string m, float t, bool o)
    {
        this.priority = p;
        this.message = m;
        this.time = t;
        this.overWritable = o;
    }
}

public class PlayerUI : MonoBehaviour
{
    public string date = "";

    //public variables
    public Text innerThoughtsUI;
    public Text questLogUI;
    public GameObject blackout;

    public Animator fadeInInnerThoughts;
    public Animator blackoutAnim;

    private bool isTutorial = false;

    private Thought currentThought = new Thought(-1, "", 0, true);
    private Thought dudThought = new Thought(-1, "", 0, true);
    private List<Thought> Thoughts = new List<Thought>();

    public void Awake()
    {
        Debug.Log(transform.gameObject.name);
        EventManager.AddQuest += ActivateQuestUI;
        EventManager.CompleteQuest += CompleteQuestUI;
        EventManager.QuestCheck += IsQuestCompleted;
        EventManager.InnerThought += addInner;
        EventManager.GetCompletion += CheckCompletion;
        EventManager.Blackout += PlayBlackout;
        EventManager.BlackoutReverse += PlayBlackoutReverse;
        EventManager.ContainedCheck += CheckQuestContained;
        EventManager.StartTutorial += startTutorial;
        EventManager.EndTutorial += endTutorial;

        if (innerThoughtsUI != null)
        {
            fadeInInnerThoughts = innerThoughtsUI.GetComponent<Animator>();
        }
        if(blackout != null)
        {
            blackoutAnim = blackout.GetComponent<Animator>();
        }
        EventManager.OnInnerThoughtInitiated(date, 5, 200, false);
    }

    private void Start()
    {
        if (blackoutAnim != null)
        {
            if (!blackoutAnim.GetBool("Unpause"))
            {
                EventManager.OnSeize(true);
            }
        }
    }

    public void FixedUpdate()
    {
        if(currentThought.priority > -1)
        {
            if(!isTutorial)
            {
                currentThought.time -= Time.fixedDeltaTime;
            }
            else if (blackoutAnim != null)
            {
                if (!blackoutAnim.GetBool("Unpause"))
                {
                    currentThought.time -= Time.fixedDeltaTime;
                }
            }
            if (currentThought.time < 0)
            {
                Thoughts.RemoveAt(0);
                if(blackoutAnim != null)
                {
                    if(!blackoutAnim.GetBool("Unpause"))
                    {
                        blackoutAnim.SetBool("Unpause", true);
                        EventManager.OnSeize(false);
                    }
                }
                if (currentThought.priority == 150 && EventManager.ending)
                {
                    var index = SceneManager.GetActiveScene().buildIndex;
                    if (EventManager.EndingType())
                    {
                        if (index + 1 < SceneManager.sceneCountInBuildSettings)
                        {
                            PlayerPrefs.SetInt("Load", 0);
                            SceneManager.LoadScene(index + 1);
                        }
                        else
                        {
                            Debug.Log("Last Scene, nothing past here");
                            SceneManager.LoadScene(0);
                        }
                    }
                    else
                    {
                        SceneManager.LoadScene(index);
                        Debug.Log("Reloading self");
                    }
                }
                if (Thoughts.Count < 1)
                {
                    currentThought = dudThought;
                }
                else
                {
                    currentThought = Thoughts[0];
                }
                innerThoughtsUI.text = currentThought.message;
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
        int sud = Regex.Matches(questLogUI.text, "Missed").Count;
        Debug.Log(dud);
        if ((dud + sud) == EventManager.questSize)
        {
            EventManager.completed = true;
        }
    }

    public void PlayBlackout()
    {
        //Debug.Log("Hello From Black Out");
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
        EventManager.InnerThought -= addInner;
        EventManager.GetCompletion -= CheckCompletion;
        EventManager.Blackout -= PlayBlackout;
        EventManager.BlackoutReverse -= PlayBlackoutReverse;
        EventManager.ContainedCheck -= CheckQuestContained;
        EventManager.StartTutorial -= startTutorial;
        EventManager.EndTutorial -= endTutorial;
    }

    public void addInner(string ThoughtText, float DelayTime, int priority, bool overWritable)
    {
        if(!Thoughts.Exists(a => a.message == ThoughtText))
        {
            Thought temp = new Thought(priority, ThoughtText, DelayTime, overWritable);
            if(Thoughts.Count > 0)
            {
                if(Thoughts[0].overWritable)
                {
                    Thoughts[0] = temp;
                }
                else
                {
                    Thoughts.Add(temp);
                }
            }
            else
            {
                Thoughts.Add(temp);
            }
            Thoughts = Thoughts.OrderByDescending(x => x.priority).ToList();
            Debug.Log(Thoughts[0].message);
            currentThought = Thoughts[0];
            innerThoughtsUI.text = currentThought.message;
        }
    }

    public void startTutorial()
    {
        isTutorial = true;
    }

    public void endTutorial()
    {
        isTutorial = false;
    }
}
