using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;

    public static MenuManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MenuManager>();
            }

            return instance;
        }
    }

    public bool isInGame = false;
    public bool seize = false;

    [SerializeField]
    private CanvasGroup KeyBindMenu;

    [SerializeField]
    private CanvasGroup PauseMenu;

    private GameObject[] keybindButtons;

    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    private void Start()
    {
        if(!isInGame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && isInGame && !seize)
        {
            if(KeyBindMenu != null && PauseMenu != null)
            {
                if(PauseMenu.alpha == 1)
                {
                    KeyBindMenu.alpha = 0;
                    KeyBindMenu.blocksRaycasts = false;
                    PauseMenu.alpha = 0;
                    PauseMenu.blocksRaycasts = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1;
                }
                else
                {
                    PauseMenu.alpha = 1;
                    PauseMenu.blocksRaycasts = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                }
            }
        }
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Load", 0);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowHideOptions()
    {
        if(KeyBindMenu != null && !seize)
        {
            KeyBindMenu.alpha = KeyBindMenu.alpha > 0 ? 0 : 1;
            KeyBindMenu.blocksRaycasts = KeyBindMenu.blocksRaycasts == true ? false : true;
        }
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void Resume()
    {
        KeyBindMenu.alpha = 0;
        KeyBindMenu.blocksRaycasts = false;
        PauseMenu.alpha = 0;
        PauseMenu.blocksRaycasts = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void GameQuit()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        EventManager.OnSaveInitiated();
    }

    public void LoadGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Load", 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene", 1), LoadSceneMode.Single);
    }
}
