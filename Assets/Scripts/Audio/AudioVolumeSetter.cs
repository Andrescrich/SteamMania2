using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioVolumeSetter : MonoBehaviour
{
    public Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ChangeMasterVolume);
        slider.value = AudioManager.Instance.MasterVolume;
    }

    void ChangeMasterVolume(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }
}
