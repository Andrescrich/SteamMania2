using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class UIManager : MonoBehaviour
{
    public UITweener pauseMenu;

    public UITweener settingsMenu;

    public Button resumeButton;

    public UITweener activePanel;

    private List<UITweener> components;
    
    private bool Paused;

    private void Awake()
    {
        PauseManager pause = PauseManager.Instance;
        pauseMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(true);
        
    }

    private void OnEnable()
    {
        foreach (var sel in Selectable.allSelectablesArray)
        {
            Debug.Log(sel.name);
        }
    }
    
    /*
    private void OnDisable()
    {
        PauseManager.OnPaused -= OpenPanel;
        PauseManager.OnUnpaused -= ClosePanel;
    }
    */
    
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
    
}
