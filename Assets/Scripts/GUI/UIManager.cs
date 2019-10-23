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

    private List<UITweener> components;
    
    private bool Paused;

    private void Awake()
    {
        PauseManager pause = PauseManager.Instance;
    }

    private void OnEnable()
    {        
        PauseManager.OnPaused += OpenPausePanel;
        PauseManager.OnUnpaused += ClosePanel;
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
    }
    
    public void ClosePanel()
    {
        pauseMenu.Close();
        settingsMenu.Close();
    }


    public void OpenOptionsPanel()
    {
        pauseMenu.Close();
        settingsMenu.Open();
    }

    public void OpenPausePanel()
    {
        settingsMenu.Close();
        pauseMenu.Open();
    }
    
}
