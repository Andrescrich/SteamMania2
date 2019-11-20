using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class UIManager : MonoBehaviour
{
    public UITween pauseMenu;

    public UITween settingsMenu;

    public UITween activePanel;

    public Audio openPanel;
    protected void Awake()
    {
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
        if(openPanel!=null)
            AudioManager.Play(openPanel);
        EventSystem.current.SetSelectedGameObject(null);
        settingsMenu.HidePanel();
        pauseMenu.HidePanel();
        if(PauseManager.Paused)
            PauseManager.TogglePause();
        activePanel = null;
    }


    public void OpenOptionsPanel()
    {
        if(openPanel!=null)
            AudioManager.Play(openPanel);
        pauseMenu.HidePanel();
        
        activePanel = settingsMenu;
        settingsMenu.ShowPanel();
    }

    public void OpenPausePanel()
    {
        if(openPanel!=null)
            AudioManager.Play(openPanel);
        settingsMenu.HidePanel();
        activePanel = pauseMenu;
        pauseMenu.ShowPanel();
    }

    public void GoBackToMenu()
    {
        PauseManager.TogglePause();
        ClosePanel();
        LevelManager.GetInstance().LoadScene(SceneNames.MainMenu,LoadSceneMode.Single);
    }
}
