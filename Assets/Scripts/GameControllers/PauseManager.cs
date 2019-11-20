﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PauseManager : Singleton<PauseManager>
{
    [SerializeField]
    public bool paused;

    public static bool Paused
    {
        get => GetInstance().paused;
        private set => GetInstance().paused = value;
    }

    public static float PAUSE_TIME_THRESHOLD = 0.5f;

    public static bool CanPause;
    private static float timeSinceLastPause;

    public static event Action OnPaused = delegate { };
    public static event Action OnUnpaused = delegate { };

    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "Pause Manager";
        timeSinceLastPause = PAUSE_TIME_THRESHOLD;
    }

    private void Update()
    {
        CanPause = timeSinceLastPause >= PAUSE_TIME_THRESHOLD;
        timeSinceLastPause += Time.unscaledDeltaTime;
        if (timeSinceLastPause > PAUSE_TIME_THRESHOLD * 3)
        {
            timeSinceLastPause = PAUSE_TIME_THRESHOLD * 3;
        }

    }

    public static void TogglePause()
    {
        if (timeSinceLastPause >= PAUSE_TIME_THRESHOLD)
        {
            timeSinceLastPause = 0;

            if (Paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    static void Pause()
    {
        if (!CanPause) return;
        Time.timeScale = 0;
        Paused = true;
        
        OnPaused?.Invoke();
    }

    static void Unpause()
    {
        if (!CanPause) return;
        Time.timeScale = 1;
        Paused = false;
        
        OnUnpaused?.Invoke();
    }

}
