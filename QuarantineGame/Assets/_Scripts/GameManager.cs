using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public bool endGame = false;
    public float restartDelay = 3f;
    public GameObject playerCam;

    public Text innerThoughtsUI; //This is bad programming

    void Start()
    {
        innerThoughtsUI.text = ""; //This is bad programming
        if (playerCam == null)
        {
            playerCam = GameObject.FindGameObjectWithTag("MainCamera"); //Right now havePills is in playerRaycast - maybe make a script in the future for storing playerData 
        }        
    }

    //Right now havePills is in playerRaycast - maybe make a script in the future for storing playerData 
    public void CompleteDay()
    {
        if(playerCam.GetComponent<PlayerRaycast>().havePills == true) 
        {
            innerThoughtsUI.text = "DAY COMPLETED";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // THIS IS SO FUCKING BAD CODING
        }
        else
        {
            innerThoughtsUI.text = "I need to take my pills first...";
        } 
    }


    public void EndGame()
    {
        if (endGame == false)
        {
            endGame = true;
            Debug.Log("Game Over");
            //Invoke("RestartGame", restartDelay);
        }
    }

}
