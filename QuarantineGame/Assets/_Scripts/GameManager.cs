using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject playerCam;

    private void Awake()
    {
        EventManager.End += EndDay;
    }

    void Start()
    {
        if (playerCam == null)
        {
            playerCam = GameObject.FindGameObjectWithTag("MainCamera"); //Right now havePills is in playerRaycast - maybe make a script in the future for storing playerData 
        }        
    }

    ////Right now havePills is in playerRaycast - maybe make a script in the future for storing playerData 
    //public void CompleteDay() // This needs to be changed to ask the player if they want to transistion to the next day
    //{
    //    if(EventManager.OnQuestCheck("Pills") == true) 
    //    {
    //        if(EventManager.EndingType())
    //        {
    //            EventManager.Blackout();
    //            var index = SceneManager.GetActiveScene().buildIndex;
    //            if (index + 1 < SceneManager.sceneCountInBuildSettings)
    //            {
    //                PlayerPrefs.SetInt("Load", 0);
    //                SceneManager.LoadScene(index + 1);
    //            }
    //            else
    //            {
    //                Debug.Log("Last Scene, nothing past here");
    //                SceneManager.LoadScene(0);
    //            }
    //        }
    //        else
    //        {
    //            EventManager.OnInnerThoughtInitiated("I've taken my medicine, but I still have stuff to do...", 10.0f, 97, false);
    //        }

    //    }
    //    else
    //    {
    //        EventManager.OnInnerThoughtInitiated("I need to take my pills first...", 10.0f, 97, false);
    //    } 
    //}

    public void EndDay()
    {
        EventManager.Blackout();
        if (EventManager.EndingType())
        {
            EventManager.OnInnerThoughtInitiated("I'm exhausted, at least I've done everything I can.", 10.0f, 150, false);
        }
        else
        {
            EventManager.OnInnerThoughtInitiated("I'm too tired to finish today!", 10.0f, 150, false);
        }
    }

    private void OnDestroy()
    {
        EventManager.End -= EndDay;
    }

}
