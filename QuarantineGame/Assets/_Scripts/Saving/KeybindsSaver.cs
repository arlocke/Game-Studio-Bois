using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BindsHolder
{
    public string key;
    public KeyCode keyCode;

    public BindsHolder(string k, KeyCode kc)
    {
        key = k;
        keyCode = kc;
    }
}

[System.Serializable]
public class KeybindsSaver
{
    public BindsHolder[] Binds;
    
    public KeybindsSaver(Dictionary<string, KeyCode> set1, Dictionary<string, KeyCode> set2)
    {
        int length = set1.Count + set2.Count;
        Binds = new BindsHolder[length];

        int count = 0;
        foreach(var bind in set1)
        {
            if (count >= length)
            {
                break;
            }
            Binds[count] = new BindsHolder(bind.Key, bind.Value);
            count += 1;
        }
        foreach(var bind in set2)
        {
            if(count >= length)
            {
                break;
            }
            Binds[count] = new BindsHolder(bind.Key, bind.Value);
            count += 1;
        }
    }
}
