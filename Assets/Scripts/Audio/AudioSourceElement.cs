using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceElement : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioSource Source
    {
        get => audioSource;
        set => audioSource = value;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
