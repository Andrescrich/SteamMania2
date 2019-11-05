using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
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

    [SerializeField] private Audio clickSound;
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
        HandleClick(playButton.transform);
        HideButtons();
        Hidetitle();
        HideGears();
    }
    
    private void OnClickLoad()
    {
        HandleClick(loadButton.transform);
    }

    private void HandleClick(Transform trans)
    {
        if (clickSound != null)
        {
            AudioManager.Play(clickSound);
        }
        //Tween.Shake(trans, trans.localPosition, Vector3.one * 10f, 0.2f, 0f);
        trans.localScale = Vector3.one;
        Tween.LocalScale(trans, trans.localScale + Vector3.one *0.1f, 0.1f,
            0, Tween.EaseInOut);
        trans.localScale = Vector3.one;
        Tween.LocalScale(trans, trans.localScale, 0.1f,
            0.1f, Tween.EaseInOut);
    }

    private void OnClickSettings()
    {
        HandleClick(settingsButton.transform);
    }
    
    private void OnClickExit()
    {
        HandleClick(exitButton.transform);
    }



}
