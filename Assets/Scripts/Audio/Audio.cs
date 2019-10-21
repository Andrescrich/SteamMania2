using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using NaughtyAttributes;
#if UNITY_EDITOR
    using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu(fileName="New Audio", menuName= "AudioManager/Audio")]
public class Audio : ScriptableObject
{
    [SerializeField] public int AudioID;
    
    [SerializeField] public AudioType Type;

    [SerializeField]
    [BoxGroup]
    public List<AudioClip> Clips;

    [SerializeField]
    [Slider(0f,1f)]
    public float Volume = 1f;

    [SerializeField]
    [Slider(0.5f,1.5f)]
    public float Pitch = 1f;

    [SerializeField] [Slider(0f, 1f)] public float SpatialBlend = 0.5f;

    [Header("Modifications")]
    [SerializeField]
    [Slider(0f,0.5f)]
    public float RandomVolume = .1f;

    [SerializeField]
    [Slider(0, 0.5f)]
    public float RandomPitch = .1f;

    [SerializeField] private bool loop;

    private AudioSource hidableSource;
    [Button("Play")] 
    public void PreviewClip()
    {
        #if UNITY_EDITOR
            loop = false;
        #endif
        Play(hidableSource);
    }

    [Button("Stop")]
    public void StopClip()
    { 
        #if UNITY_EDITOR
                loop = false;
        #endif
        Stop(hidableSource);
    }

    private void OnEnable()
    {
        hidableSource =
            EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave,
                typeof(AudioSource)).GetComponent<AudioSource>();
        AudioID = GetInstanceID();
    }

    private void ModifyAudio(AudioSource source) {        
        source.clip = GetRandomClip();
        source.loop = loop;
        source.spatialBlend = SpatialBlend;
        if (Type != AudioType.Music)
        {
            source.volume = Volume * (1 + Random.Range(-RandomVolume / 2f, RandomVolume / 2f));
            source.pitch = Pitch * (1 + Random.Range(-RandomPitch / 2f, RandomPitch / 2f));
        }
    }
    
    public void Play(AudioSource source) {
        ModifyAudio(source);
        source.Play();
        
    }

    public void PlayDelayed(AudioSource source, float delay)
    {

        ModifyAudio(source);
        source.PlayDelayed(delay);
    }

    public void PlayOnce(AudioSource source)
    {

        ModifyAudio(source);
        source.PlayOneShot(source.clip);
    }


    public void Pause(AudioSource source)
    {
        source.Pause();
    }

    public void Resume(AudioSource source)
    {
        source.UnPause();
    }

    public void Stop(AudioSource source)
    {
        source.Stop();
    }
    
    
    public AudioClip GetRandomClip() {
        if (Clips.Count == 0) {
            Debug.LogError(nameof(Audio)+" does not have audio clips.");
            return null;
        }
        return Clips[Random.Range(0, Clips.Count)];
    }

}