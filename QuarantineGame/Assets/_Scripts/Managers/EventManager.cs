﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    //Creating Event Actions
    public static System.Action SaveInitiated;
    public static System.Action LoadInitiated;

    public static void OnSaveInitiated()
    {
        SaveInitiated?.Invoke();
    }

    public static void OnLoadInitiated()
    {
        LoadInitiated?.Invoke();
    }
}
