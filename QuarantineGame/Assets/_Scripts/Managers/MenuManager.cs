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

    private GameObject[] keybindButtons;

    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    private void Start()
    {
        if(!isInGame)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isInGame && !seize)
        {
            ShowHideOptions();
            if(KeyBindMenu != null)
            {
                if(KeyBindMenu.alpha == 0)
                {
                    Time.timeScale = 1;
                }
                else
                {
                    Time.timeScale = 0;
                }
            }
        }
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Load", 0);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("Load", 1);
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
}
