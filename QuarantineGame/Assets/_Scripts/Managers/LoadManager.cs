using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DestroyedObjectManager.Initiate();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Load", 0) == 1)
        {
            EventManager.OnLoadInitiated();
        }
    }
}
