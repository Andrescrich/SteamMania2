using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
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

        public static float MasterVolume { 
            get => GetInstance().masterVolume;
            set => GetInstance().masterVolume = value;
        }
        public static float MusicVolume { 
            get => GetInstance().musicVolume;
            set => GetInstance().musicVolume = value;
        }
        public static float SFXVolume { 
            get => GetInstance().sfxVolume;
            set => GetInstance().sfxVolume = value;
        }
        public static float UIVolume { 
            get => GetInstance().uiVolume;
            set => GetInstance().uiVolume = value;
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
        public int StartingAudioSources = 7;
        [SerializeField]
        private bool canGrow;

        public AudioSourceElement prefabSource;

        public List<AudioSourceElement> musicSources;
        public List<AudioSourceElement> inUse;

        

        #endregion

    #region Unity Methods
        protected override void Awake()
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
            LoadAllVolumes();
            InitializePool();
        }


        private void Update()
        {
            SetMasterVolumeMethod(MasterVolume);
            SetMusicVolumeMethod(MusicVolume);
            SetSFXVolumeMethod(SFXVolume);
            SetUIVolumeMethod(UIVolume);
        }
        #endregion

        
        #region static Methods

        public static void Play(Audio sound, GameObject go = default)
        {
            GetInstance().PlayMethod(sound, go);
        }

        public static void Stop(Audio sound, GameObject go = default)
        {
            GetInstance().StopMethod(sound, go);
        }
        
        public static void PlayOnce(Audio sound, GameObject go = default)
        {
            GetInstance().PlayOnceMethod(sound, go);
        }
        
        public static void PlayDelayed(Audio sound,float delay, GameObject go = default)
        {
            GetInstance().PlayDelayedMethod(sound, delay, go);
        }
        public static void Pause(Audio sound, GameObject go = default)
        {
            GetInstance().PauseMethod(sound, go);
        }
        public static void Resume(Audio sound, GameObject go = default)
        {
            GetInstance().UnPauseMethod(sound, go);
        }

        public static void FadeIn(Audio sound, GameObject go = default, float fadeTime = 1f)
        {
            GetInstance().FadeInMethod(sound, go, fadeTime);
        }
        
        public static void FadeOut(Audio sound, GameObject go = default, float fadeTime = 1f)
        {
            GetInstance().FadeOutMethod(sound, go, fadeTime);
        }

        public static void CrossFade(Audio inSound, Audio outSound, GameObject inGo = default,
            GameObject outGo = default, float fadeInTime = 1f, float fadeOutTime = 1f)
        {
            GetInstance().CrossFade(inSound, outSound, fadeInTime, fadeOutTime, inGo, outGo);
        }

        public static void SetMasterVolume(float volume)
        {
            GetInstance().SetMasterVolumeMethod(volume);
        }
        
        public static void SetMusicVolume(float volume)
        {
            GetInstance().SetMusicVolumeMethod(volume);
        }
        public static void SetSFXVolume(float volume)
        {
            GetInstance().SetSFXVolumeMethod(volume);
            GetInstance().SetUIVolumeMethod(volume);
        }
        
        public static bool IsPlaying(Audio audio)
        {
            return GetInstance().IsPlayingMethod(audio);
        }

        #endregion

    #region public Methods

        public void ReturnToPool(AudioSourceElement element)
        {
            inUse.Remove(element);
        }
    
        

        #region VolumeControl
        public void SetMasterVolumeMethod(float sliderValue)
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

        public void SetMusicVolumeMethod(float sliderValue)
        {
            musicVolume = sliderValue;
            if (sliderValue <= 0)
            {
                mixer.SetFloat(musicVolumeName, volumeThreshold);
            }
            else
            { 
                float value = 20f * Mathf.Log10(sliderValue);
                mixer.SetFloat(musicVolumeName, value);
            }
        }

        public void SetSFXVolumeMethod(float sliderValue)
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

        public void SetUIVolumeMethod(float sliderValue)
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


        private bool  IsPlayingMethod(Audio sound)
        {
            foreach (var s in inUse)
            {
                if (s.sound == sound)
                {
                    return true;
                }
            }

            return false;
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
    
    void PlayMethod(Audio sound, GameObject position)
        {
            AudioSource source = HandleAudioSource(sound, position);
            sound.Play(source);
            
        }

        void PlayOnceMethod(Audio sound, GameObject position)
        {
            AudioSource source = HandleAudioSource(sound, position);
            sound.PlayOnce(source);
        }
        void PlayDelayedMethod(Audio sound, float delay, GameObject position)
        {
            AudioSource source = HandleAudioSource(sound, position);
            sound.PlayDelayed(source, delay);
        }
        
        void StopMethod(Audio sound, GameObject go)
        {
            foreach (AudioSourceElement s in inUse.ToList())
            {
                if (sound.AudioID == s.SourceElementID)
                {
                    //Get the sound in the playing Sounds that is equal to the audio we want to stop
                    sound.Stop(s.Source);
                    //remove it if it contains it
                    if (inUse.Contains(s))
                    {
                        ReturnToPool(s);
                    }
                }
            }
        }

        void PauseMethod(Audio sound, GameObject go)
        {
            foreach (AudioSourceElement s in inUse.ToList())
            {
                if (sound.AudioID == s.SourceElementID)
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
                if (sound.AudioID == s.SourceElementID)
                {
                    //Get the sound in the playing Sounds that is equal to the audio we want to stop
                    sound.Resume(s.Source);
                }
            }
        }

        void FadeInMethod(Audio sound, GameObject go, float fadeTime)
        {
            AudioSource source = HandleAudioSource(sound, go);
            source.clip = sound.GetRandomClip();
            StartCoroutine(AudioFade.FadeIn(source, fadeTime, sound.Volume));
        }
        
        void FadeOutMethod(Audio sound, GameObject go, float fadeTime)
        {
            foreach (AudioSourceElement s in inUse.ToList())
            {
                if (sound.AudioID == s.SourceElementID)
                {
                   StartCoroutine(AudioFade.FadeOut(s.Source, fadeTime));
                }
            }
        }

        void CrossFade(Audio outMusic, Audio inMusic, float fadeInTime, float fadeOutTime, GameObject inGo, GameObject outGo)
        {
            FadeOutMethod(outMusic, outGo, fadeOutTime);
            FadeInMethod(inMusic, inGo, fadeInTime);
        }
        
        
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
        sourceElement.SourceElementID = sound.AudioID;
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

    public void SaveAllVolumes()
    {
        SaveSystem<float>.SavePrefs(PlayerPrefsKeys.MasterVolume, masterVolume);
        SaveSystem<float>.SavePrefs(PlayerPrefsKeys.MusicVolume, musicVolume);
        SaveSystem<float>.SavePrefs(PlayerPrefsKeys.SFXVolume, sfxVolume);
    }

    public void LoadAllVolumes()
    {
        MasterVolume = SaveSystem<float>.LoadFloat(PlayerPrefsKeys.MasterVolume);
        MusicVolume = SaveSystem<float>.LoadFloat(PlayerPrefsKeys.MusicVolume);
        SFXVolume = SaveSystem<float>.LoadFloat(PlayerPrefsKeys.SFXVolume);
    }
    }

