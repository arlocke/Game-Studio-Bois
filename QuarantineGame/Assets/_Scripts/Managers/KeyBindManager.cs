using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KeyBindManager : MonoBehaviour
{
    private static KeyBindManager instance;

    public static KeyBindManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<KeyBindManager>();
            }
            return instance;
        }
    }

    public Dictionary<string, KeyCode> KeyBinds { get; private set; }

    public Dictionary<string, KeyCode> ActionBinds { get; private set; }

    private string bindName;

    // Start is called before the first frame update
    void Start()
    {
        KeyBinds = new Dictionary<string, KeyCode>();
        ActionBinds = new Dictionary<string, KeyCode>();

        Bindkey("UP", KeyCode.W);
        Bindkey("DOWN", KeyCode.S);
        Bindkey("RIGHT", KeyCode.D);
        Bindkey("LEFT", KeyCode.A);

        Bindkey("ACT1", KeyCode.Alpha1);
        Bindkey("ACT2", KeyCode.Alpha2);
        Bindkey("ACT3", KeyCode.Alpha3);
        Bindkey("ACT4", KeyCode.Alpha4);
    }

    public void Bindkey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = KeyBinds;

        if(key.Contains("ACT"))
        {
            currentDictionary = ActionBinds;
        }
        if(!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key, keyBind);
            MenuManager.MyInstance.UpdateKeyText(key, keyBind);
        }
        else if(currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            currentDictionary[myKey] = KeyCode.None;
            MenuManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }

        currentDictionary[key] = keyBind;
        MenuManager.MyInstance.UpdateKeyText(key, keyBind);
        MenuManager.MyInstance.seize = false;
        bindName = string.Empty;
    }

    public void KeyBindOnClick(string bindName)
    {
        MenuManager.MyInstance.seize = true;
        this.bindName = bindName;
    }

    private void OnGUI()
    {
        if(bindName != string.Empty)
        {
            Event e = Event.current;

            if(e.isKey)
            {
                Bindkey(bindName, e.keyCode);
            }
        }
    }
}
