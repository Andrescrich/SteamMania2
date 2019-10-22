using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class UIManager : MonoBehaviour
{

    public UITweener component;
    
    private bool Paused;

    private void Awake()
    {
        PauseManager pause = PauseManager.Instance;
    }

    private void OnEnable()
    {        
        PauseManager.OnPaused += OpenPanel;
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
        component.Close();
    }

    public void OpenPanel()
    {
        component.Open();
    }
    
    
}
