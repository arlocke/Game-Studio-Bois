using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsQuest : MonoBehaviour
{
    //public variables
    public GameObject playerCam;

    void Start()
    {
        if (playerCam == null)
        {
            playerCam = GameObject.FindGameObjectWithTag("MainCamera"); //Right now havePills is in playerRaycast - maybe make a script in the future for storing playerData 
        }
    }

    private void Update()
    {
        if (playerCam.GetComponent<PlayerRaycast>().havePills == true)
        {
            gameObject.SetActive(false);
        }
    }
}
