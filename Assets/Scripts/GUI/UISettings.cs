using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISettings : MonoBehaviour, ISaveData
{
    public UISlider masterVolumeSlider;
    public UISlider musicVolumeSlider;
    public UISlider sfxSlider;
    public Toggle fullScreenToggle;
    public Toggle vsyncToggle;
    public TMP_Dropdown antialising;
    public TMP_Dropdown resolutionsDropdown;
    
    public Resolution[] resolutions;
    [SerializeField]
    private GameSettings gameSettings;

    void Awake()
    {
    }
    private void OnEnable()
    {

        gameSettings = new GameSettings();
        masterVolumeSlider.Slider.onValueChanged.AddListener(delegate { ChangeMasterVolume();});
        musicVolumeSlider.Slider.onValueChanged.AddListener(delegate { ChangeMusicVolume();});
        sfxSlider.Slider.onValueChanged.AddListener(delegate { ChangeSFXVolume();});
        fullScreenToggle.onValueChanged.AddListener(delegate { SetFullScreen();});
        vsyncToggle.onValueChanged.AddListener(delegate { SetVSync();});
        //antialising.onValueChanged.AddListener(delegate { SetAntialising(); });
        resolutionsDropdown.onValueChanged.AddListener(delegate { SetResolution(); });
        

        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionsDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
        }
        Load();
        
    }

    private void SetResolution()
    {
        var resolutionValue = resolutionsDropdown.value;
        Screen.SetResolution(resolutions[resolutionValue].width,
            resolutions[resolutionValue].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionValue;
    }

    private void SetAntialising()
    {
        QualitySettings.antiAliasing = gameSettings.antialiasing = (int)Mathf.Pow(2f, antialising.value);
    }


    void ChangeMasterVolume()
    {
        gameSettings.masterVolume = masterVolumeSlider.Slider.value / masterVolumeSlider.Slider.maxValue;
        AudioManager.SetMasterVolume(gameSettings.masterVolume);
        //AudioManager.MasterVolume = gameSettings.masterVolume / masterVolumeSlider.maxValue;
        
        masterVolumeSlider.SetMultiplier(gameSettings.masterVolume);
    }
    
    void ChangeMusicVolume()
    {
        gameSettings.musicVolume = musicVolumeSlider.Slider.value / musicVolumeSlider.Slider.maxValue;
        AudioManager.SetMusicVolume(gameSettings.musicVolume);
        musicVolumeSlider.SetMultiplier(gameSettings.musicVolume);
    }
    
    void ChangeSFXVolume()
    {
        gameSettings.sfxVolume = sfxSlider.Slider.value / sfxSlider.Slider.maxValue;
        AudioManager.SetSFXVolume(gameSettings.sfxVolume);
        sfxSlider.SetMultiplier(gameSettings.sfxVolume);
    }

    public void SetFullScreen()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullScreenToggle.isOn;
    }

    public void SetVSync()
    {
        var isOn = vsyncToggle.isOn;
        QualitySettings.vSyncCount = isOn ? 1 : 0;
        gameSettings.vSync = isOn;
    }

    public static String GameSettingsKey = "GameSettings.json";
    public void Load()
    {
        gameSettings = SaveSystem.LoadJSON<GameSettings>(GameSettingsKey);
        masterVolumeSlider.current = gameSettings.masterVolume * masterVolumeSlider.maximum;
        musicVolumeSlider.current = gameSettings.musicVolume * musicVolumeSlider.maximum;
        sfxSlider.current = gameSettings.sfxVolume * sfxSlider.maximum;
        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
        fullScreenToggle.isOn = gameSettings.fullscreen;
        vsyncToggle.isOn = gameSettings.vSync;
        resolutionsDropdown.value = gameSettings.resolutionIndex;
        //antialising.value = gameSettings.antialiasing;
    }

    
    public void Save()
    {
        SaveSystem.SaveJSON(gameSettings, GameSettingsKey);
        
    }

    public void ApplyChanges()
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        ApplyChanges();
    }
}
