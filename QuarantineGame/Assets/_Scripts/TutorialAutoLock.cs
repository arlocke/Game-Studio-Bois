using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAutoLock : MonoBehaviour
{
    private DoorScript target;
    private bool safeToUse = true;

    private void Awake()
    {
        EventManager.EndTutorial += Unlock;
        EventManager.LoadInitiated += Unlock;
        target = transform.GetComponent<DoorScript>();
        if (target == null)
        {
            safeToUse = false;
            Debug.Log("Missing DoorScript On Object: " + transform.name);
        }
        else
        {
            safeToUse = true;
            target.isLocked = true;
        }
    }

    void Unlock()
    {
        if(safeToUse)
        {
            target.isLocked = false;
        }
    }

    private void OnDestroy()
    {
        EventManager.EndTutorial -= Unlock;
        EventManager.LoadInitiated -= Unlock;
    }
}
