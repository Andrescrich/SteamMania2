using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public Button playButton;
    public Button loadButton;
    public Button settingsButton;
    public Button exitButton;

    public bool loadSceneOnEnable;
    [ShowIf("loadSceneOnEnable")]
    public string sceneToLoad = "SampleScene";

    private void Awake()
    {
        playButton.onClick.AddListener(OnClickPlay);
        loadButton.onClick.AddListener(OnClickLoad);
        settingsButton.onClick.AddListener(OnClickSettings);
        exitButton.onClick.AddListener(OnClickExit);
    }

    private void OnClickPlay()
    {
        LevelManager.GetInstance().LoadScene(sceneToLoad, 3);
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
