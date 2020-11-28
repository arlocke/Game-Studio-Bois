﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredBox : MonoBehaviour
{
    public bool unlocking = false;
    public bool spawning = false;
    public bool questCompleting = false;
    public bool questSetting = false;
    public bool keyBasedCompletion = false;
    public string nonKeyQuestName = "";
    public string[] keys;
    public Rigidbody[] unlockables;

    public Transform spawnable;
    public Vector3 spawnLocation;
    public Vector3 spawnRotation;

    private bool safeToUse = true;
    private string keyName = "";

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(safeToUse)
        {
            var dud = other.GetComponent<QuestObjective>();
            if (dud != null)
            {
                foreach (string key in keys)
                {
                    if (dud.key == key)
                    {
                        if (questSetting)
                        {
                            SetQuest(key);
                            if (questCompleting)
                            {
                                CompleteQuest(key);
                            }
                            else
                            {
                                if(unlocking)
                                {
                                    Unlock();
                                }
                                if(spawning)
                                {
                                    Spawn();
                                }
                            }
                        }
                        else
                        {
                            if(questCompleting)
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
                            }
                        }
                        break;
                    }
                }
            }
        }
    }

    private void Unlock()
    {
        foreach(var component in unlockables)
        {
            component.isKinematic = false;
        }
    }

    private void Spawn()
    {
        Instantiate(spawnable, spawnLocation, Quaternion.Euler(spawnRotation));
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
                    EventManager.OnCompleteQuestInitiated(keyName);
                    if (unlocking)
                    {
                        Unlock();
                    }
                    if (spawning)
                    {
                        Spawn();
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
                    EventManager.OnCompleteQuestInitiated(nonKeyQuestName);
                    if (unlocking)
                    {
                        Unlock();
                    }
                    if (spawning)
                    {
                        Spawn();
                    }
                }
            }
        }
    }

    private void SetQuest(string key)
    {
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
}
