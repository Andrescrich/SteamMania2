using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class UIManager : Singleton<UIManager>
{
    public UITweener pauseMenu;

    public UITweener settingsMenu;

    public Button resumeButton;

    public UITweener activePanel;

    private List<UITweener> components;
    
    private bool Paused;

    public override void Awake()
    {
        gameObject.name = "UIManager";
        PauseManager pause = PauseManager.Instance;
        pauseMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(true);
        
    }

    
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!PauseManager.Instance.CanPause) return;
            if (activePanel == null)
            {
                PauseManager.TogglePause();
                OpenPausePanel();
            }

            else if (activePanel == settingsMenu)
            {
                OpenPausePanel();
            }

            else if (activePanel == pauseMenu)
            {
                PauseManager.TogglePause();
                ClosePanel();
            }
        }
    }
    
    public void ClosePanel()
    {
        settingsMenu.Close(activePanel == pauseMenu);
        pauseMenu.Close(activePanel == settingsMenu);
        PauseManager.TogglePause();
        activePanel = null;
    }


    public void OpenOptionsPanel()
    {

        pauseMenu.Close();
        

        activePanel = settingsMenu;
        settingsMenu.Open();
    }

    public void OpenPausePanel()
    {
        settingsMenu.Close();
        resumeButton.Select();
        activePanel = pauseMenu;
        pauseMenu.Open();
    }

    public void GoBackToMenu()
    {
        PauseManager.TogglePause();
        LevelManager.Instance.LoadScene("MainMenu");
    }
}
