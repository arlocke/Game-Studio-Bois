using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5MailDoorLocked : MonoBehaviour
{
    public Transform MailDoor;
    public Rigidbody MailDoorPhysics;
    public string key = "";

    private void OnTriggerStay(Collider other)
    {
        var dud = other.GetComponent<QuestObjective>();
        var name = EventManager.NameFromLoader(key);
        if (dud != null && name != null)
        {
            if (dud.key == key && EventManager.OnQuestCheck(name) && EventManager.OnQuestCheck("Pills"))
            {
                if(MailDoor.eulerAngles.x <= 0.5f)
                {
                    MailDoorPhysics.isKinematic = true;
                }
            }
        }
    }
}
