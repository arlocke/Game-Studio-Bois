using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DestroyedObjectManager
{
    public static List<string> IDS = new List<string>();

    public static void Initiate()
    {
        EventManager.SaveInitiated += Save;
    }

    private static void Save()
    {
        Debug.Log("Destroyed Object Saving");
        if(IDS.Count > 0)
        {
            for (int i = 0; i < IDS.Count; i++)
            {
                Debug.Log("Inside Loop");
                SaveLoad.SaveObjectDestruction(IDS[i]);
            }
        }
    }
}
