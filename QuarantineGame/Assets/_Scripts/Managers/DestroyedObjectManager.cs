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
        for(int i = 0; i < IDS.Count; i++)
        {
            SaveLoad.SaveObjectDestruction(IDS[i]);
        }
    }
}
