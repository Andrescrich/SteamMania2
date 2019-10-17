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
        [Space]
        
        [SerializeField]
        [Range(0,1)]
        private float musicVolume = 1;


        [SerializeField]
        [Range(0,1)]
        private float sfxVolume = 1;

        [SerializeField]
        [Range(0,1)]
        private float uiVolume = 1;

        public float MasterVolume { 
            get => masterVolume;
            set => masterVolume = value;
        }
        public float MusicVolume { 
            get => musicVolume;
            set => musicVolume = value;
        }
        public float SFXVolume { 
            get => sfxVolume;
            set => sfxVolume = value;
        }
        public float UIVolume { 
            get => uiVolume;
            set => uiVolume = value;
        }
        
        private float volumeThreshold = -80.0f;

        Audio[] uiAudio;

        Audio[] musicAudio;

        Audio[] sfxAudio;

        
        public static string musicVolumeName = "musicVol";
        public static string masterVolumeName = "masterVol";
        public static string sfxVolumeName = "sfxVol";
        public static string uiVolumeName = "uiVol";

        public AudioMixer mixer;

        private AudioPool pool;

        #endregion

    #region Unity Methods
        public override void Awake()
        {
            base.Awake();
            pool = GetComponent<AudioPool>();
            
        }
        #endregion

        
        #region static methods

        public Audio uiSound;

        public Audio sfxSound;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Play(uiSound);
            }

            if (Input.GetMouseButtonDown(1))
            {
                Play(sfxSound);
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
            AudioSource source = pool.GetAudioSource();
            SetMixer(source, sound);
            sound.Play(source);
        }
        void PlayOnce(Audio sound)
        {
            SetMixer(pool.GetAudioSource(), sound);
            sound.PlayOnce(pool.GetAudioSource());
        }

        void PlayDelayed(Audio sound, float delay)
        {
            SetMixer(pool.GetAudioSource(), sound);
            sound.PlayDelayed(pool.GetAudioSource(), delay);
        }

        void Stop(Audio sound)
        {
            SetMixer(pool.GetAudioSource(),sound);
            sound.Stop(pool.GetAudioSource());
        }

        void PauseSound(Audio sound)
        {
            SetMixer(pool.GetAudioSource(),sound);
            sound.Pause(pool.GetAudioSource());
        }

        void ResumeSound(Audio sound)
        {
            SetMixer(pool.GetAudioSource(),sound);
            sound.Resume(pool.GetAudioSource());
        }
        void FadeOut(Audio sound)
        {
            SetMixer(pool.GetAudioSource(), sound);
            StartCoroutine(AudioFade.FadeOut(pool.GetAudioSource(), fadeOutSpeed));
        }

        void FadeIn(Audio sound)
        {
            SetMixer(pool.GetAudioSource(), sound);
            StartCoroutine(AudioFade.FadeIn(pool.GetAudioSource(), fadeInSpeed, musicVolume));
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

    private void SetMixer(AudioSource source, Audio audio)
    {
        source.outputAudioMixerGroup = mixer.FindMatchingGroups(audio.Type.ToString())[0];
    }
    
    #endregion
    }
}
