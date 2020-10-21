using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectSaver : MonoBehaviour
{
    public string ID { get; private set; }

    private void Awake()
    {
        ID = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
        Debug.Log("ID is " + ID);
        //Adding Saving to Events
        EventManager.SaveInitiated += Save;
        EventManager.LoadInitiated += Load;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void Save()
    {
        SaveLoad.SaveObject(this, ID);
    }

    private void Load()
    {
        ObjectData data = SaveLoad.LoadObject(ID);
        if(data != null)
        {
            if(data.destroyed)
            {
                Destroy(gameObject);
            }
            else
            {
                Vector3 position;
                position.x = data.position[0];
                position.y = data.position[1];
                position.z = data.position[2];
                Vector3 rotation;
                rotation.x = data.rotation[0];
                rotation.y = data.rotation[1];
                rotation.z = data.rotation[2];
                transform.eulerAngles = rotation;
                transform.position = position;
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.SaveInitiated -= Save;
        EventManager.LoadInitiated -= Load;
        DestroyedObjectManager.IDS.Add(ID);
    }
}
