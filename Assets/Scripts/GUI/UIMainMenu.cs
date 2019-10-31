using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public RectTransform buttonHolder;
    public TextMeshProUGUI titleText;
    public Button playButton;
    public Button loadButton;
    public Button settingsButton;
    public Button exitButton;

    public string sceneToLoad = "SampleScene";

    public UITween rightGears;
    public UITween leftGears;
    private void Awake()
    {
        playButton.onClick.AddListener(OnClickPlay);
        loadButton.onClick.AddListener(OnClickLoad);
        settingsButton.onClick.AddListener(OnClickSettings);
        exitButton.onClick.AddListener(OnClickExit);
        HideButtons();
        Hidetitle();
        HideGears();

    }

    private void ShowGears()
    {
        rightGears.HidePanel();
        leftGears.HidePanel();
    }

    private void HideGears()
    {
        rightGears.ShowPanel();
        leftGears.ShowPanel();
    }

 
    private void Start()
    {
        Invoke("ShowButtons", 1f);
        Invoke("ShowTitle", 1f);
        Invoke("ShowGears", 1f);
    }

    private void ShowTitle()
    {
        titleText.GetComponent<UITween>().ShowPanel();
    }
    
    private void Hidetitle()
    {
        titleText.GetComponent<UITween>().HidePanel();
    }
    private void ShowButtons()
    {
        buttonHolder.GetComponent<UITween>().ShowPanel();
    }
    
    private void HideButtons()
    {
        buttonHolder.GetComponent<UITween>().HidePanel();
    }

    private void OnClickPlay()
    {
        LevelManager.GetInstance().LoadScene(sceneToLoad, 3);
        HideButtons();
        Hidetitle();
        HideGears();
    }
    
    private void OnClickLoad()
    {
        throw new NotImplementedException();
    }
    
    private void OnClickSettings()
    {
        throw new NotImplementedException();
    }
    
    private void OnClickExit()
    {
        throw new NotImplementedException();
    }



}
