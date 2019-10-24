using System;
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
        get => Instance.paused;
        private set => Instance.paused = value;
    }

    public static float PAUSE_TIME_THRESHOLD = 0.6f;

    private float timeSinceLastPause;

    public static event Action OnPaused = delegate { };
    public static event Action OnUnpaused = delegate { };

    public override void Awake()
    {
        base.Awake();
        gameObject.name = "Pause Manager";
        timeSinceLastPause = PAUSE_TIME_THRESHOLD;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        timeSinceLastPause += Time.unscaledDeltaTime;
        if (timeSinceLastPause > PAUSE_TIME_THRESHOLD * 3)
        {
            timeSinceLastPause = PAUSE_TIME_THRESHOLD * 3;
        }

    }

    public static void TogglePause()
    {
        if (Instance.timeSinceLastPause >= PAUSE_TIME_THRESHOLD)
        {
            Instance.timeSinceLastPause = 0;

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
    
    public static void Pause()
    {
        
        Time.timeScale = 0;
        Paused = true;
        
        OnPaused?.Invoke();
    }

    public static void Unpause()
    {
        Time.timeScale = 1;
        Paused = false;
        
        OnUnpaused?.Invoke();
    }

}
