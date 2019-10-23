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

    private UITweener tweener;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<UITweener>();
        masterVolumeSlider.value = SaveSystem<float>.LoadFloat(AudioVariables.MasterVolume) * masterVolumeSlider.maxValue;
        musicVolumeSlider.value = SaveSystem<float>.LoadFloat(AudioVariables.MusicVolume) * masterVolumeSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        AudioManager.MasterVolume = masterVolumeSlider.value/masterVolumeSlider.maxValue;
        AudioManager.MusicVolume = musicVolumeSlider.value/musicVolumeSlider.maxValue;
    }

    public void SaveSettings()
    {
        AudioManager.Instance.SaveAllVolumes();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }
}
