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
    [Range(0f, 1f)]
    public float Volume = 1f;

    [SerializeField]
    public float Pitch = 1f;

    [SerializeField]
    [Range(0, 0.5f)]
    public float RandomVolume = .1f;

    [SerializeField]
    [Range(0, 0.5f)]
    public float RandomPitch = .1f;

    [SerializeField] private bool loop;

    private AudioSource source;
    [Button("Play")] 
    public void PreviewClip()
    {
        loop = false;
        Play(source);
    }

    [Button("Stop")]
    public void StopClip()
    { 
        loop = false;
        Stop(source);
    }

    private void OnEnable()
    {
        source =
            EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave,
                typeof(AudioSource)).GetComponent<AudioSource>();
        AudioID = GetInstanceID();
    }

    private void ModifyAudio(AudioSource source) {
        source.volume = Volume * (1 + Random.Range(-RandomVolume / 2f, RandomVolume / 2f));
        source.pitch = Pitch * (1 + Random.Range(-RandomPitch / 2f, RandomPitch / 2f));
        source.loop = loop;
    }
    
    public void Play(AudioSource source) {
        ModifyAudio(source);
        source.clip = GetRandomClip();
        var length = source.clip.length;
        source.enabled = true;
        source.Play();
        
    }

    public void PlayDelayed(AudioSource source, float delay)
    {
        ModifyAudio(source);
        source.clip = GetRandomClip();
        source.PlayDelayed(delay);
    }

    public void PlayOnce(AudioSource source)
    {
        ModifyAudio(source);
        source.PlayOneShot(GetRandomClip());
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
    
    
    private AudioClip GetRandomClip() {
        if (Clips.Count == 0) {
            Debug.LogError(nameof(Audio)+" does not have audio clips.");
            return null;
        }
        return Clips[Random.Range(0, Clips.Count)];
    }

}