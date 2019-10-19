using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

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

        [Header("Pool")]
        public int StartingAudioSources = 15;
        [SerializeField]
        private bool canGrow;

        public AudioSourceElement prefabSource;

        public List<AudioSource> musicSources;
        public List<AudioSource> inUse;

        

        #endregion

    #region Unity Methods
        public override void Awake()
        {
            base.Awake();
            gameObject.name = "AudioManager";
            musicSources = new List<AudioSource>();
            inUse = new List<AudioSource>();
            mixer = Resources.Load<AudioMixer>("Audio/MasterMixer");
            prefabSource = Resources.Load<AudioSourceElement>("Audio/AudioSourceElement");

        }

        public void Start()
        {            
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

    
        public void PlayMethod(Audio sound, GameObject position = default)
        {

            if (position != default)
            {
                AudioSource newSource = position.GetComponent<AudioSource>();
                if (newSource != null)
                {
                    SetMixer(newSource, sound);
                    sound.Play(newSource);
                }
                else
                {
                    newSource = position.AddComponent<AudioSource>();
                    SetMixer(newSource, sound);
                    sound.Play(newSource);
                }
            }
            else
            {
                
                AudioSource source = GetAudioSource();
                SetMixer(source, sound);
                sound.Play(source);
            }
        }
        
        
        
        void StopMethod(Audio sound, GameObject position = default)
        {
            if (position != default)
            {
                AudioSource newSource = position.GetComponent<AudioSource>();
                if (newSource != null)
                {
                    SetMixer(newSource, sound);
                    sound.Stop(newSource);
                    Destroy(position.GetComponent<AudioSource>());
                }
                else
                {
                    newSource = position.AddComponent<AudioSource>();
                    SetMixer(newSource, sound);
                    sound.Stop(newSource);
                    Destroy(position.GetComponent<AudioSource>());
                    
                }
            }

            foreach (var s in musicSources)
            {
                foreach (var clip in sound.Clips)
                {
                    if (clip == s.clip)
                    {
                        SetMixer(s, sound);
                        sound.Stop(s);
                    }
                }
            }
        }
        /*
        void PlayOnce(Audio sound)
        {
            AudioSource source = GetAudioSource();
            SetMixer(source, sound);
            sound.PlayOnce(source);
        }

        void PlayDelayed(Audio sound, float delay)
        {
            AudioSource source = GetAudioSource();
            SetMixer(source, sound);
            sound.PlayDelayed(source, delay);
        }


        void PauseSound(Audio sound)
        {
            AudioSource source = GetAudioSource();
            SetMixer(source, sound);
            sound.Pause(source);
        }

        void ResumeSound(Audio sound)
        {
            AudioSource source = GetAudioSource();
            SetMixer(source, sound);
            sound.Resume(source);
        }
        
        void FadeIn(Audio sound)
        {
            AudioSource source = GetAudioSource();
            SetMixer(source, sound);    
            StartCoroutine(AudioFade.StartFade(mixer, masterVolumeName, fadeInSpeed, 1f));
        }
        void FadeOut(Audio sound)
        {
            AudioSource source = GetAudioSource();
            SetMixer(source, sound);
            StartCoroutine(AudioFade.StartFade(mixer, masterVolumeName, fadeOutSpeed, 0f));
        }
        
        */
        
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
    private AudioSource CreateAudioSource(bool forceEnabled)
    {
        AudioSourceElement source = Instantiate(prefabSource, transform, true);
        source.gameObject.SetActive(forceEnabled);
        musicSources.Add(source.Source);
        return source.Source;
    }
    private AudioSource GetAudioSource()
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

