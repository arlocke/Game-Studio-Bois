using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredBox : MonoBehaviour
{
    //Dynamic Changes
    public bool unlocking = false;
    public bool spawning = false;
    public bool enabling = false;
    public bool questCompleting = false; //Completes Quest on Trigger.
    public bool questSetting = false; //Sets Quest on Trigger.
    public bool keyBasedCompletion = false; //Uses list of keys to complete. If false, uses internal quest name.
    public bool playerTriggered = false; //Player can trigger this.
    public bool oneTimeUse = true; //Makes the trigger a one time use.

    //Settings
    public string nonKeyQuestName = "";
    public string[] keys;
    public Rigidbody[] unlockables;
    public Transform spawnable;
    public Transform[] enablable;
    public Vector3 spawnLocation;
    public Vector3 spawnRotation;

    //Internal Settings
    private bool safeToUse = true;
    private string keyName = "";
    private bool used = false; //Compliments oneTimeUse. It's the brain.

    private void Awake()
    {
        if(keys.Length <= 0)
        {
            safeToUse = false;
            Debug.Log("This trigger volume has no keys: " + transform.name);
        }
        if(unlocking)
        {
            if(unlockables.Length <= 0)
            {
                safeToUse = false;
                Debug.Log("This unlocking trigger volume has no rigid bodies to unlock: " + transform.name);
            }
        }
        if(spawning)
        {
            if(spawnLocation == null || spawnRotation == null || spawnable == null)
            {
                safeToUse = false;
                Debug.Log("This spawning trigger volume has no spawnable or set location: " + transform.name);
            }
        }
        if(enabling)
        {
            if(enablable.Length <= 0)
            {
                safeToUse = false;
                Debug.Log("This enabling trigger volume has no object to enable: " + transform.name);
            }
        }
        if(!keyBasedCompletion && questCompleting)
        {
            if(nonKeyQuestName == "")
            {
                safeToUse = false;
                Debug.Log("This quest completing trigger volume has no internal name when it is not using keys: " + transform.name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(used);
        if(safeToUse && !used)
        {
            if(other.tag != "Player")
            {
                var dud = other.GetComponent<QuestObjective>();
                if (dud != null)
                {
                    foreach (string key in keys)
                    {
                        if (dud.key == key)
                        {
                            Switch(key);
                            break;
                        }
                    }
                }
            }
            else if(playerTriggered)
            {
                if (oneTimeUse)
                {
                    used = true;
                }
                if (unlocking)
                {
                    Unlock();
                }
                if (spawning)
                {
                    Spawn();
                }
                if (enabling)
                {
                    Enable();
                }
            }
            
        }
    }

    private void Unlock()
    {
        if (oneTimeUse)
        {
            used = true;
        }
        foreach (var component in unlockables)
        {
            DoorScript dud = component.transform.GetComponent<DoorScript>();
            if(dud != null)
            {
                dud.isLocked = false;
            }
            else
            {
                component.isKinematic = false;
            }
        }
    }

    private void Spawn()
    {
        if (oneTimeUse)
        {
            used = true;
        }
        Instantiate(spawnable, spawnLocation, Quaternion.Euler(spawnRotation));
    }

    private void Enable()
    {
        if (oneTimeUse)
        {
            used = true;
        }
        foreach (var obj in enablable)
        {
            obj.gameObject.SetActive(true);
        }
    }

    private void CompleteQuest(string key)
    {
        if(keyBasedCompletion)
        {
            keyName = EventManager.NameFromLoader(key);
            if(keyName != "")
            {
                if(EventManager.OnContainedCheck(keyName))
                {
                    if (oneTimeUse)
                    {
                        used = true;
                    }
                    EventManager.OnCompleteQuestInitiated(keyName);
                    if (unlocking)
                    {
                        Unlock();
                    }
                    if (spawning)
                    {
                        Spawn();
                    }
                    if(enabling)
                    {
                        Enable();
                    }
                }
                keyName = "";
            }
        }
        else
        {
            if(nonKeyQuestName != "")
            {
                if(EventManager.OnContainedCheck(nonKeyQuestName))
                {
                    if (oneTimeUse)
                    {
                        used = true;
                    }
                    EventManager.OnCompleteQuestInitiated(nonKeyQuestName);
                    if (unlocking)
                    {
                        Unlock();
                    }
                    if (spawning)
                    {
                        Spawn();
                    }
                    if (enabling)
                    {
                        Enable();
                    }
                }
            }
        }
    }

    private void SetQuest(string key)
    {
        if (oneTimeUse)
        {
            used = true;
        }
        if (keyBasedCompletion)
        {
            keyName = EventManager.NameFromLoader(key);
            if (keyName != "")
            {
                EventManager.OnAddQuestInitiated(keyName);
                keyName = "";
            }

        }
        else
        {
            if (nonKeyQuestName != "")
            {
                EventManager.OnAddQuestInitiated(nonKeyQuestName);
            }
        }
    }

    private void Switch(string key)
    {
        if (questSetting)
        {
            SetQuest(key);
        }
        if (questCompleting)
        {
            CompleteQuest(key);
        }
        else
        {
            if (unlocking)
            {
                Unlock();
            }
            if (spawning)
            {
                Spawn();
            }
            if (enabling)
            {
                Enable();
            }
        }
    }
}
