using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public List<Text> clocksToDisplay = new List<Text>();
    //public Text timeText; //Drag drop timeText in editor

    private const float REAL_SECONDS_WHILE_AWAKE = 990f; //16.5 minutes (7:30am-12am)
    private const float REAL_SECONDS_PER_INGAME_DAY = 1440f; //24 minutes (1440 seconds)
    public float time;
    public float timeRounded;
    private float timeToDisplay;
    private float hoursPerDay = 24f; 
    private float minutesPerHour = 60f;
    private bool tutorial = false;
    private bool loaded = false;

    string hoursString;
    string minutesString;

    private void Awake()
    {
        EventManager.SaveInitiated += Save;
        EventManager.LoadInitiated += Load;
        EventManager.StartTutorial += StartTutorial;
        EventManager.EndTutorial += EndTutorial;
        EventManager.FastForward += SkipTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!loaded)
        {
            time += 450; //has the player start at 7:30am (60 * 7.5 = 450)
        }
    }

    void FixedUpdate()
    {
        if(!tutorial)
        {
            time += Time.fixedDeltaTime; // use this when calling events
        }
        timeRounded = Mathf.Ceil(time);
        //Debug.Log(timeRounded);

        timeToDisplay = time / REAL_SECONDS_PER_INGAME_DAY;
        

        float timeNormalized = timeToDisplay % 1f;
        

        hoursString = Mathf.Floor(timeNormalized * hoursPerDay).ToString("0"); 
        minutesString = Mathf.Floor(((timeNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        for (int i = 0; i < clocksToDisplay.Count; i++)
        {
             clocksToDisplay[i].text = hoursString + ":" + minutesString; // is this bad?
        }

        //if (timeRounded > 1260 & !EventManager.ending)
        //{
        //    EventManager.ending = true;
        //    if(EventManager.EndingType())
        //    {
        //        EventManager.OnInnerThoughtInitiated("I've done all my tasks for the day!", 10.0f, 100, false);
        //    }
        //    else
        //    {
        //        EventManager.OnInnerThoughtInitiated("I still have things to do today!", 10.0f, 100, false);
        //    }
        //}
    }

    private void StartTutorial()
    {
        tutorial = true;
    }

    private void EndTutorial()
    {
        tutorial = false;
    }

    private void OnDestroy()
    {
        EventManager.SaveInitiated -= Save;
        EventManager.LoadInitiated -= Load;
        EventManager.StartTutorial -= StartTutorial;
        EventManager.EndTutorial -= EndTutorial;
        EventManager.FastForward -= SkipTime;
    }

    private void SkipTime(float flatValue)
    {
        time += flatValue;
        if(time >= 1200)
        {
            time = 1140;
        }
    }

    public void Save()
    {
        QuestData dud = new QuestData(time.ToString());
        Debug.Log(dud.QuestList);
        SaveLoad.SaveQuests(dud, "time_stored");
    }

    public void Load()
    {
        loaded = true;
        QuestData dud = SaveLoad.LoadQuests("time_stored");
        time = float.Parse(dud.QuestList);
    }
}
