using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class EventManager
{
    public static string name = "";
    public static bool isQuestCompleted = false;
    public static bool completed = false;
    public static bool ending = false;
    public static int questSize = 0;

    //Creating Event Actions
    public static System.Action SaveInitiated;
    public static System.Action LoadInitiated;
    public static System.Action FirstCall;
    public static System.Action<string, float> InnerThought;
    public static System.Action<string> AddEmail;
    public static System.Action<string> AddQuest;
    public static System.Action<string> CompleteQuest;
    public static System.Action<string> QuestCheck;
    public static System.Action<string> GetName;
    public static System.Action GetCompletion;
    public static System.Action StartTutorial;
    public static System.Action EndTutorial;
    public static System.Action<float> FastForward;
    public static System.Action<bool> Seize;

    public static void OnSaveInitiated()
    {
        SaveInitiated?.Invoke();
        Debug.Log("Finished Saving");
    }

    public static void OnLoadInitiated()
    {
        LoadInitiated?.Invoke();
        Debug.Log("Finished Loading");
    }

    public static void OnFirstCallInitiated()
    {
        FirstCall?.Invoke();
    }

    public static void OnInnerThoughtInitiated(string thought, float time)
    {
        InnerThought?.Invoke(thought, time);
    }

    public static void OnAddEmailInitiated(string email)
    {
        AddEmail?.Invoke(email);
    }

    public static void OnAddQuestInitiated(string name)
    {
        //Debug.Log("Adding quest SIR");
        AddQuest?.Invoke(name);
    }

    public static void OnCompleteQuestInitiated(string name)
    {
        CompleteQuest?.Invoke(name);
    }

    public static bool OnQuestCheck(string name)
    {
        QuestCheck?.Invoke(name);
        return isQuestCompleted;
    }

    public static string NameFromLoader(string key)
    {
        GetName?.Invoke(key);
        return name;
    }

    //Checks what the current type of ending is for the player.
    public static bool EndingType()
    {
        GetCompletion?.Invoke();
        return completed;
    }

    public static void OnStartTutorial()
    {
        StartTutorial?.Invoke();
    }

    public static void OnEndTutorial()
    {
        EndTutorial?.Invoke();
    }

    public static void OnFastForward(float value)
    {
        FastForward?.Invoke(value);
    }

    public static void OnSeize(bool facts)
    {
        Seize?.Invoke(facts);
    }
}
