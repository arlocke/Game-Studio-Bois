using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class EventManager
{

    //Creating Event Actions
    public static System.Action SaveInitiated;
    public static System.Action LoadInitiated;
    public static System.Action FirstCall;
    public static System.Action<string, float> InnerThought;

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
}
