﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public static class AudioFade
{
    public static event Action OnFadeInEnd = delegate{};
    public static event Action OnFadeOutEnd = delegate {};

    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
    }
    
    public static IEnumerator FadeOut(AudioSource source, float fadeTime)
    {
        float startVolume = source.volume;
 
        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeTime;
 
            yield return null;
        }
        OnFadeInEnd?.Invoke();
        source.Stop();
        source.volume = startVolume;

    }

    public static IEnumerator FadeIn(AudioSource source,float fadeTime, float MaxVolume)
    {
        float startVolume = 0.2f;
 
        source.volume = 0;
        source.Play();
 
        while (source.volume < MaxVolume)
        {
            source.volume += startVolume * Time.deltaTime / fadeTime;
 
            yield return null;
        }
        OnFadeOutEnd?.Invoke();
        source.volume = MaxVolume;
    }
}