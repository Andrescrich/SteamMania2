using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;

public class AudioPauseController : MonoBehaviour
{
    private AudioMixerSnapshot pausedSnapshot;
    private AudioMixerSnapshot unpauseSnapshot;

    private AudioMixer mixer;

    private void Awake()
    {
        mixer = AudioManager.Instance.mixer;
        pausedSnapshot = mixer.FindSnapshot("Paused");
        unpauseSnapshot = mixer.FindSnapshot("Unpaused");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //PauseManager.TogglePause();
            //Lowpass();
        }
    }

    public void Lowpass()
    {
        if (!PauseManager.Paused)
        {
            unpauseSnapshot.TransitionTo(.1f);
        }
        else
        {
            pausedSnapshot.TransitionTo(.1f);
        }
    }


}
