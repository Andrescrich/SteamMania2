using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UITweener))]
public class UISettings : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxSlider;
    public UIToggle fullScreenToggle;
    public UIToggle vsyncToggle;

    private UITweener tweener;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<UITweener>();
        LoadSettings();
        
    }

    private void LoadSettings()
    {
        masterVolumeSlider.value = SaveSystem<float>.LoadFloat(PlayerPrefsKeys.MasterVolume) * masterVolumeSlider.maxValue;
        musicVolumeSlider.value = SaveSystem<float>.LoadFloat(PlayerPrefsKeys.MusicVolume) * masterVolumeSlider.maxValue;
        sfxSlider.value = SaveSystem<float>.LoadFloat(PlayerPrefsKeys.SFXVolume) * sfxSlider.maxValue;

        bool loadedFullscreen = SaveSystem<int>.LoadInt(PlayerPrefsKeys.FullScreen) == 1;
        bool loadedVSync = SaveSystem<int>.LoadInt(PlayerPrefsKeys.VSync) != 0;
        fullScreenToggle.SetToggle(loadedFullscreen);
        vsyncToggle.SetToggle(loadedVSync);
        SetFullScreen(loadedFullscreen);
        SetVSync(loadedVSync);

    }

    // Update is called once per frame
    void Update()
    {
        AudioManager.MasterVolume = masterVolumeSlider.value/masterVolumeSlider.maxValue;
        AudioManager.MusicVolume = musicVolumeSlider.value/musicVolumeSlider.maxValue;
        AudioManager.SFXVolume = sfxSlider.value / sfxSlider.maxValue;
    }

    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    private int normalFrameRate = 300;
    public void SetVSync(bool vsync)
    {
        if (vsync)
        {
            Application.targetFrameRate = normalFrameRate;
        }
        else
            Application.targetFrameRate = -1;

    }
    
    

    public void SaveSettings()
    {
        AudioManager.Instance.SaveAllVolumes();
        
        if(fullScreenToggle.IsOn)
            SaveSystem<int>.SavePrefs(PlayerPrefsKeys.FullScreen, 1);
        else
        {
            SaveSystem<int>.SavePrefs(PlayerPrefsKeys.FullScreen, 0);
        }
        
        if(vsyncToggle.IsOn)
            SaveSystem<int>.SavePrefs(PlayerPrefsKeys.VSync, 1);
        else
        {
            SaveSystem<int>.SavePrefs(PlayerPrefsKeys.VSync, 0);
        }
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }
}
