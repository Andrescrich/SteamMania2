using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

 public class AudioManager : Singleton<AudioManager>
    {
        #region references

        [Header("Fade")]


        [SerializeField]
        private float fadeInSpeed = 1f;
        [SerializeField]
        private float fadeOutSpeed = 1f;

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

        [Header("Pool")]
        public int StartingAudioSources = 15;
        [SerializeField]
        private bool canGrow;

        public AudioSourceElement prefabSource;

        public List<AudioSourceElement> musicSources;
        public List<AudioSourceElement> inUse;

        

        #endregion

    #region Unity Methods
        public override void Awake()
        {
            base.Awake();
            gameObject.name = "AudioManager";
            musicSources = new List<AudioSourceElement>();
            inUse = new List<AudioSourceElement>();
            mixer = Resources.Load<AudioMixer>("Audio/MasterMixer");
            prefabSource = Resources.Load<AudioSourceElement>("Audio/AudioSourceElement");

        }

        public void Start()
        {         
            //TODO Set PlayerPerfs values to XVolume
            
            SetMasterVolume(MasterVolume);
            SetMusicVolume(MusicVolume);
            SetSFXVolume(SFXVolume);
            SetUIVolume(UIVolume);
            InitializePool();
        }


        private void Update()
        {
            SetMasterVolume(MasterVolume);
            SetMusicVolume(MusicVolume);
            SetSFXVolume(SFXVolume);
            SetUIVolume(UIVolume);
        }
        #endregion

        
        #region static methods

        public static void Play(Audio sound, GameObject go = default)
        {
            Instance.PlayMethod(sound, go);
        }

        public static void Stop(Audio sound, GameObject go = default)
        {
            Instance.StopMethod(sound, go);
        }
        
        public static void PlayOnce(Audio sound, GameObject go = default)
        {
            Instance.PlayOnceMethod(sound, go);
        }
        
        public static void PlayDelayed(Audio sound,float delay, GameObject go = default)
        {
            Instance.PlayDelayedMethod(sound, delay, go);
        }
        public static void Pause(Audio sound, GameObject go = default)
        {
            Instance.PauseMethod(sound, go);
        }
        public static void Resume(Audio sound, GameObject go = default)
        {
            Instance.UnPauseMethod(sound, go);
        }

        public static void FadeIn(Audio sound, GameObject go = default)
        {
            Instance.FadeInMethod(sound, go);
        }
        
        public static void FadeOut(Audio sound, GameObject go = default)
        {
            Instance.FadeOutMethod(sound, go);
        }
        
        /*
¡

        public static void FadeIn(string name)
        {
            instance.FadeIn(instance.FindAudio(name));
        }

        public static void FadeOut(string name)
        {
            instance.FadeOut(instance.FindAudio(name));
        }

        */

    #endregion

    #region public Methods

        public void ReturnToPool(AudioSourceElement element)
        {
            inUse.Remove(element);
        }
    
        public void PlayMethod(Audio sound, GameObject position)
        {
            AudioSource source = HandleAudioSource(sound, position);
            sound.Play(source);
            
        }

        public void PlayOnceMethod(Audio sound, GameObject position)
        {
            AudioSource source = HandleAudioSource(sound, position);
            sound.PlayOnce(source);
        }
        public void PlayDelayedMethod(Audio sound, float delay, GameObject position)
        {
            AudioSource source = HandleAudioSource(sound, position);
            sound.PlayDelayed(source, delay);
        }
        
        void StopMethod(Audio sound, GameObject go)
        {
            foreach (AudioSourceElement s in inUse.ToList())
            {
                if (sound == s.sound)
                {
                    //Get the sound in the playing Sounds that is equal to the audio we want to stop
                    sound.Stop(s.Source);
                    //remove it if it contains it
                    if (inUse.Contains(s))
                    {
                        inUse.Remove(s);
                    }
                }
            }
        }

        void PauseMethod(Audio sound, GameObject go)
        {
            foreach (AudioSourceElement s in inUse.ToList())
            {
                if (sound == s.sound)
                {
                    //Get the sound in the playing Sounds that is equal to the audio we want to stop
                    sound.Pause(s.Source);
                }
            }
        }

        void UnPauseMethod(Audio sound, GameObject go)
        {
            foreach (AudioSourceElement s in inUse.ToList())
            {
                if (sound == s.sound)
                {
                    //Get the sound in the playing Sounds that is equal to the audio we want to stop
                    sound.Resume(s.Source);
                }
            }
        }

        private IEnumerator FadeInCoroutine;
        private IEnumerator FadeOutCoroutine;
        void FadeInMethod(Audio sound, GameObject go)
        {
            AudioSource source = HandleAudioSource(sound, go);
            source.clip = sound.GetRandomClip();
            FadeInCoroutine = AudioFade.FadeIn(source, fadeInSpeed, sound.Volume);
            if(FadeOutCoroutine!=null)
                StopCoroutine(FadeOutCoroutine);
            StartCoroutine(FadeInCoroutine);

        }
        
        void FadeOutMethod(Audio sound, GameObject go)
        {
            foreach (AudioSourceElement s in inUse.ToList())
            {
                if (sound == s.sound)
                {
                    FadeOutCoroutine = AudioFade.FadeOut(s.Source, fadeOutSpeed);
                    if(FadeInCoroutine!=null)
                        StopCoroutine(FadeInCoroutine);
                    StartCoroutine(FadeOutCoroutine);
                }
            }

        }

        #region VolumeControl
        public void SetMasterVolume(float sliderValue)
        {
            masterVolume = sliderValue;
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
            musicVolume = sliderValue;
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
            sfxVolume = sliderValue;
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
            uiVolume = sliderValue;
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

    #endregion

    #region private Methods
            
    private void InitializePool()
    {
        for (int i = 0; i < StartingAudioSources; i++)
        {
            CreateAudioSource(false);
        }
    }
    private AudioSourceElement CreateAudioSource(bool forceEnabled)
    {
        AudioSourceElement source = Instantiate(prefabSource, transform, true);
        source.gameObject.SetActive(forceEnabled);
        musicSources.Add(source);
        return source;
    }
    
    private AudioSource HandleAudioSource(Audio sound, GameObject position = default)
    {
        AudioSourceElement sourceElement = GetAudioSource();
        AudioSource source = sourceElement.Source;
            
        if (position != default)
        {
            sourceElement.transform.position = position.transform.position;

        }

        sourceElement.sound = sound;
        SetMixer(source, sound);
        inUse.Add(sourceElement);
        return source;
    }
    private AudioSourceElement GetAudioSource()
    {
        foreach (var music in musicSources)
        {
            if (!music.gameObject.activeInHierarchy)
            {
                music.gameObject.SetActive(true);
                return music;
            }
        }
        return CreateAudioSource(true);
    }
    private void SetMixer(AudioSource source, Audio sound)
    {
        source.outputAudioMixerGroup = mixer.FindMatchingGroups(sound.Type.ToString())[0];
    }
    
    #endregion
    }

