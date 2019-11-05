using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    private void Start()
    {
        
        ClosePanel();
    }


    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!PauseManager.CanPause) return;
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
        EventSystem.current.SetSelectedGameObject(null);
        settingsMenu.HidePanel();
        pauseMenu.HidePanel();
        if(PauseManager.Paused)
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
        ClosePanel();
        LevelManager.GetInstance().LoadScene("MainMenu");
    }
}
