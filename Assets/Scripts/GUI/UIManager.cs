using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class UIManager : Singleton<UIManager>
{
    public UITween pauseMenu;

    public UITween settingsMenu;

    public Button resumeButton;

    public UITween activePanel;

    
    private bool Paused;

    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "UIManager";
    }

    
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!PauseManager.GetInstance().CanPause) return;
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
        settingsMenu.HidePanel();
        pauseMenu.HidePanel();
        PauseManager.TogglePause();
        activePanel = null;
    }


    public void OpenOptionsPanel()
    {

        pauseMenu.HidePanel();
        

        activePanel = settingsMenu;
        settingsMenu.ShowPanel();
    }

    public void OpenPausePanel()
    {
        settingsMenu.HidePanel();
        resumeButton.Select();
        activePanel = pauseMenu;
        pauseMenu.ShowPanel();
    }

    public void GoBackToMenu()
    {
        PauseManager.TogglePause();
        LevelManager.GetInstance().LoadScene("MainMenu");
    }
}
