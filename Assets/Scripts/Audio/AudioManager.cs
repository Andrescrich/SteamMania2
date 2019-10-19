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

        private GameObject musicHolder;
        private Queue<AudioSource> musicSources;

        private Dictionary<AudioSource, Audio> soundsOn;
        

        #endregion

    #region Unity Methods
        public override void Awake()
        {
            base.Awake();
            gameObject.name = "AudioManager";
            musicHolder = new GameObject("Holder");
            musicHolder.transform.SetParent(transform);
            musicSources = new Queue<AudioSource>();
            soundsOn = new Dictionary<AudioSource, Audio>();
            mixer = Resources.Load<AudioMixer>("Audio/MasterMixer");
            SetMasterVolume(MasterVolume);
            SetMusicVolume(MusicVolume);
            SetSFXVolume(SFXVolume);
            SetUIVolume(UIVolume);
        }

        public void Start()
        {            

            InitializePool();
        }


        private void Update()
        {            
            foreach (var music in musicSources)
            {
                if (!music.isPlaying)
                {
                    music.enabled = false;
                }
            }
            
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
            AudioSource source = GetAudioSource();

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
                SetMixer(source, sound);
                sound.Play(source);
            }
        }
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
        AudioSource source = musicHolder.AddComponent<AudioSource>();
        source.enabled = forceEnabled;
        musicSources.Enqueue(source);
        return source;
    }
    private AudioSource GetAudioSource()
    {
        foreach (var music in musicSources)
        {
            if (!music.enabled)
            {
                music.enabled = true;
                return music;
            }
        }
        return CreateAudioSource(true);
    }
    private void SetMixer(AudioSource source, Audio audio)
    {
        source.outputAudioMixerGroup = mixer.FindMatchingGroups(audio.Type.ToString())[0];
    }
    #endregion
    }

