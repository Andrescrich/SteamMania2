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

    public bool Paused = false;

    private void Awake()
    {
        mixer = AudioManager.Instance.mixer;
        pausedSnapshot = mixer.FindSnapshot("Paused");
        unpauseSnapshot = mixer.FindSnapshot("Unpaused");
    }

    public void TogglePause()
    {
        if (!Paused)
        {
            Time.timeScale = 0;
            Paused = true;
        }
        else if(Paused)
        {
            Time.timeScale = 1;
            Paused = false;
        }
        
        Lowpass();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void Lowpass()
    {
        if (!Paused)
        {
            unpauseSnapshot.TransitionTo(.1f);
        }
        else
        {
            pausedSnapshot.TransitionTo(.1f);
        }
    }


}
