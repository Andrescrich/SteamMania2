﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
 using UnityEngine.PlayerLoop;

 namespace HW
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region references

        [Header("Fade")]


        [SerializeField]
        private float fadeInSpeed = 0.6f;
        [SerializeField]
        private float fadeOutSpeed = 0.3f;

        [Header("Volume")]

        [SerializeField]
        [Range(0,1)]
        private float masterVolume = 1;

        public float MasterVolume { 
            get => masterVolume;
            set => masterVolume = value;
        }
        [SerializeField]
        [Range(0,1)]
        private float musicVolume = 1;

        public float MusicVolume{ 
            get => musicVolume;
            set => musicVolume = value;
        }

        [SerializeField]
        [Range(0,1)]
        private float sfxVolume = 1;
        
        public float SFXVolume{ 
            get => sfxVolume;
            set => sfxVolume = value;
        }

        [SerializeField]
        [Range(0,1)]
        private float uiVolume = 1;
        public float UIVolume { 
            get => uiVolume;
            set => uiVolume = value;
        }
        
        private float volumeThreshold = -80.0f;

        Audio[] uiAudio;

        Audio[] musicAudio;

        Audio[] sfxAudio;

        GameObject musicGameObject;


        GameObject SFXGameObject;


        GameObject UIGameObject;
        
        public static string musicVolumeName = "musicVol";
        public static string masterVolumeName = "masterVol";
        public static string sfxVolumeName = "sfxVol";
        public static string uiVolumeName = "uiVol";

        public AudioMixer mixer;

        #endregion

    #region Unity Methods
        public override void Awake()
        {
            base.Awake();
            source = GetComponent<AudioSource>();
            InitHolder();
            
        }

        private void InitHolder() {
            musicGameObject = transform.Find("MusicHolder").gameObject;
            if (musicGameObject == null) {
                GameObject musicHolder = new GameObject("MusicHolder");
                musicHolder.transform.SetParent(transform);
            }

            SFXGameObject = transform.Find("SFXHolder").gameObject;
            if (SFXGameObject == null) {
                GameObject sfxHolder = new GameObject("SFXHolder");
                sfxHolder.transform.SetParent(transform);
            }

            UIGameObject = transform.Find("UIHolder").gameObject;
            if (UIGameObject == null) {
                GameObject uiHolder = new GameObject("UIHolder");
                uiHolder.transform.SetParent(transform);
            }
        }
        #endregion

        private AudioSource source;
        
        #region static methods

        public Audio uiSound;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Play(uiSound);
            }
            SetMasterVolume(masterVolume);
            SetUIVolume(uiVolume);
        }
        /*
        public static void PlayOnce(string name)
        {
            instance.PlayOnce(instance.FindAudio(name));
        }
        public static void Play(string name)
        {
            instance.Play(instance.FindAudio(name));
        }

        public static void Stop(string name)
        {
            instance.Stop(instance.FindAudio(name));
        }


        public static void FadeIn(string name)
        {
            instance.FadeIn(instance.FindAudio(name));
        }

        public static void FadeOut(string name)
        {
            instance.FadeOut(instance.FindAudio(name));
        }

        public static void Pause(string name)
        {
            instance.PauseSound(instance.FindAudio(name));
        }

        public static void Resume(string name)
        {
            instance.ResumeSound(instance.FindAudio(name));
        }

        public static void PlayDelayed(string name, float delay)
        {
            instance.PlayDelayed(instance.FindAudio(name), delay);
        }
        */

    #endregion

    #region public Methods

        public void Play(Audio sound)
        {
            SetMixer(sound, mixer);
            sound.Play(source);
        }
        void PlayOnce(Audio sound)
        {
            SetMixer(sound, mixer);
            sound.PlayOnce(source);
        }

        void PlayDelayed(Audio sound, float delay)
        {
            SetMixer(sound, mixer);
            sound.PlayDelayed(source, delay);
        }

        void Stop(Audio sound)
        {
            SetMixer(sound, mixer);
            sound.Stop(source);
        }

        void PauseSound(Audio sound)
        {
            SetMixer(sound, mixer);
            sound.Pause(source);
        }

        void ResumeSound(Audio sound)
        {
            SetMixer(sound, mixer);
            sound.Resume(source);
        }
        void FadeOut(Audio sound)
        {
            SetMixer(sound, mixer);
            StartCoroutine(AudioFade.FadeOut(source, fadeOutSpeed));
        }

        void FadeIn(Audio sound)
        {
            SetMixer(sound, mixer);
            StartCoroutine(AudioFade.FadeIn(source, fadeInSpeed, musicVolume));
        }

        public void SetMasterVolume(float sliderValue)
        {
            if (sliderValue <= 0)
            {
                mixer.SetFloat(masterVolumeName, volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(sliderValue);
                mixer.SetFloat(masterVolumeName, value);
            }
        }

        public void SetMusicVolume(float sliderValue)
        {
            if (sliderValue <= 0)
            {
                mixer.SetFloat(musicVolumeName, volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(sliderValue);
                mixer.SetFloat(musicVolumeName, value);
            }
        }

        public void SetSFXVolume(float sliderValue)
        {
            if (sliderValue <= 0)
            {
                mixer.SetFloat(sfxVolumeName, volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(sliderValue);
                mixer.SetFloat(sfxVolumeName, value);
            }
        }

        public void SetUIVolume(float sliderValue)
        {
            if (sliderValue <= 0)
            {
                mixer.SetFloat(uiVolumeName, volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(sliderValue);
                mixer.SetFloat(uiVolumeName, value);
            }
        }

        public void ClearMasterVolume()
        {
            mixer.ClearFloat(masterVolumeName);
        }

        public void ClearMusicVolume()
        {
            mixer.ClearFloat(musicVolumeName);
        }

        public void ClearSFXVolume()
        {
            mixer.ClearFloat(sfxVolumeName);
        }

        public void ClearUIVolume()
        {
            mixer.ClearFloat(uiVolumeName);
        }

    #endregion

    #region private Methods

    private void SetMixer(Audio audio, AudioMixer mixer)
    {
        source.outputAudioMixerGroup = mixer.FindMatchingGroups(audio.Type.ToString())[0];
    }
    
    #endregion
    }
}
