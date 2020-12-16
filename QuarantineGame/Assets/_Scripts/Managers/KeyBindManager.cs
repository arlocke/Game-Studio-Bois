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
    }

    public void BindkeyInitalize(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = KeyBinds;

        if (key.Contains("ACT"))
        {
            currentDictionary = ActionBinds;
        }
        if (!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key, keyBind);
            MenuManager.MyInstance.UpdateKeyText(key, keyBind);
        }
        else if (currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            currentDictionary[myKey] = KeyCode.None;
            MenuManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }

        currentDictionary[key] = keyBind;
        MenuManager.MyInstance.UpdateKeyText(key, keyBind);
        StartCoroutine(MenuManager.MyInstance.DelayedUnlock());
        bindName = string.Empty;
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
        StartCoroutine(MenuManager.MyInstance.DelayedUnlock());
        bindName = string.Empty;
        EventManager.OnKeySave();
    }

    public void KeyBindOnClick(string bindName)
    {
        MenuManager.MyInstance.selectLock = true;
        this.bindName = bindName;
    }

    private void OnGUI()
    {
        if(bindName != string.Empty)
        {
            Event e = Event.current;

            if(e.isKey)
            {
                if (bindName == "ACT1" && e.keyCode != KeyCode.Space)
                {
                    Bindkey(bindName, e.keyCode);
                }
                else if(bindName != "ACT1")
                {
                    Bindkey(bindName, e.keyCode);
                }
            }
        }
    }

    public void Awake()
    {
        EventManager.KeySave += SaveKeyBinds;

        KeyBinds = new Dictionary<string, KeyCode>();
        ActionBinds = new Dictionary<string, KeyCode>();

        BindkeyInitalize("UP", KeyCode.W);
        BindkeyInitalize("DOWN", KeyCode.S);
        BindkeyInitalize("RIGHT", KeyCode.D);
        BindkeyInitalize("LEFT", KeyCode.A);

        BindkeyInitalize("ACT1", KeyCode.P);
        BindkeyInitalize("ACT2", KeyCode.LeftControl);
        BindkeyInitalize("ACT3", KeyCode.Alpha3);
        BindkeyInitalize("ACT4", KeyCode.Alpha4);

        LoadKeybinds();
    }

    public void SaveKeyBinds()
    {
        KeybindsSaver keys = new KeybindsSaver(KeyBinds, ActionBinds);
        SaveLoad.SaveKeybinds(keys, "keys_Saved");
    }

    public void LoadKeybinds()
    {
        KeybindsSaver keys = SaveLoad.LoadKeybinds("keys_Saved");
        if(keys != null)
        {
            foreach (var key in keys.Binds)
            {
                BindkeyInitalize(key.key, key.keyCode);
            }
        }
    }

    public void OnDestroy()
    {
        EventManager.KeySave -= SaveKeyBinds;
    }
}
