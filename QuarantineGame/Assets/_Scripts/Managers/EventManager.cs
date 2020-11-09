using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class EventManager
{
    public static string name = "";
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
    public static System.Action<string> GetName;
    public static System.Action GetCompletion;

    public static void OnSaveInitiated()
    {
        SaveInitiated?.Invoke();
    }

    public static void OnLoadInitiated()
    {
        LoadInitiated?.Invoke();
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

    public static string NameFromLoader(string key)
    {
        GetName?.Invoke(key);
        return name;
    }

    public static bool EndingType()
    {
        GetCompletion?.Invoke();
        return completed;
    }
}
