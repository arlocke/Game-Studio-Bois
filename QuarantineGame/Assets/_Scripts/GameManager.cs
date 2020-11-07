using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject playerCam;


    void Start()
    {
        if (playerCam == null)
        {
            playerCam = GameObject.FindGameObjectWithTag("MainCamera"); //Right now havePills is in playerRaycast - maybe make a script in the future for storing playerData 
        }        
    }

    //Right now havePills is in playerRaycast - maybe make a script in the future for storing playerData 
    public void CompleteDay() // This needs to be changed to ask the player if they want to transistion to the next day
    {
        if(playerCam.GetComponent<PlayerRaycast>().havePills == true) 
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // THIS IS SO FUCKING BAD CODING
        }
        else
        {
            EventManager.OnInnerThoughtInitiated("I need to take my pills first...", 10.0f);
        } 
    }

}
