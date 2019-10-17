using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AudioPool : MonoBehaviour
{
    public int MusicSourcesCount;

    private GameObject musicHolder;
    public List<AudioSource> musicSources;

    private void Awake()
    {
        musicHolder = new GameObject("musicHolder");
        musicHolder.transform.SetParent(gameObject.transform);
        musicSources = new List<AudioSource>();
        
    }

    private void Start()
    {
        InitializePool();
    }

    public void InitializePool()
    {
        for (int i = 0; i < MusicSourcesCount; i++)
        {
            CreateAudioSource();
        }
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
    }

    public AudioSource GetAudioSource()
    {
        foreach (var music in musicSources)
        {
            if (!music.enabled)
            {
                music.enabled = true;
                return music;
            }
        }

        return CreateAudioSource();
    }

    private AudioSource CreateAudioSource()
    {
        AudioSource source = musicHolder.AddComponent<AudioSource>();
        source.enabled = false;
        musicSources.Add(source);
        return source;
    }
}
