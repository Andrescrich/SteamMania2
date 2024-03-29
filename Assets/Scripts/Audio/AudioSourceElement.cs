﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceElement : MonoBehaviour
{
    [SerializeField] public int SourceElementID;
    [SerializeField] public Audio sound;
    
    private AudioSource audioSource;

    public AudioSource Source => audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SourceElementID = GetInstanceID();
    }

    private void Update()
    {
        if (!audioSource.isPlaying && audioSource.time == 0)
        {
            AudioManager.GetInstance().ReturnToPool(this);
            gameObject.SetActive(false);
        }
    }
}
