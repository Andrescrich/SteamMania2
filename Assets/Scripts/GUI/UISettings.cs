using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour, ISaveData
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxSlider;
    public Toggle fullScreenToggle;
    public Toggle vsyncToggle;

    
    void Awake()
    {
        Load();
        masterVolumeSlider.onValueChanged.AddListener(ChangeMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
        
    }

    void ChangeMasterVolume(float volume)
    {
        AudioManager.MasterVolume = volume / masterVolumeSlider.maxValue;
    }
    
    void ChangeMusicVolume(float volume)
    {
        AudioManager.MusicVolume = volume / musicVolumeSlider.maxValue;
    }
    
    void ChangeSFXVolume(float volume)
    {
        AudioManager.SFXVolume = volume / sfxSlider.maxValue;
    }

    public void Load()
    {
        masterVolumeSlider.value = SaveSystem<float>.LoadFloat(PlayerPrefsKeys.MasterVolume) * masterVolumeSlider.maxValue;
        musicVolumeSlider.value = SaveSystem<float>.LoadFloat(PlayerPrefsKeys.MusicVolume) * masterVolumeSlider.maxValue;
        sfxSlider.value = SaveSystem<float>.LoadFloat(PlayerPrefsKeys.SFXVolume) * sfxSlider.maxValue;

        bool loadedFullscreen = SaveSystem<int>.LoadInt(PlayerPrefsKeys.FullScreen) == 1;
        bool loadedVSync = SaveSystem<int>.LoadInt(PlayerPrefsKeys.VSync) != 0;
        fullScreenToggle.isOn = loadedFullscreen;
        vsyncToggle.isOn = loadedVSync;
        SetFullScreen(loadedFullscreen);
        SetVSync(loadedVSync);

    }

    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    private int normalFrameRate = 300;
    public void SetVSync(bool vsync)
    {
        Application.targetFrameRate = vsync ? normalFrameRate : -1;
    }
    
    public void Save()
    {
        AudioManager.GetInstance().Save();
        
        if(fullScreenToggle.isOn)
            SaveSystem<int>.SavePrefs(PlayerPrefsKeys.FullScreen, 1);
        else
        {
            SaveSystem<int>.SavePrefs(PlayerPrefsKeys.FullScreen, 0);
        }
        
        if(vsyncToggle.isOn)
            SaveSystem<int>.SavePrefs(PlayerPrefsKeys.VSync, 1);
        else
        {
            SaveSystem<int>.SavePrefs(PlayerPrefsKeys.VSync, 0);
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
