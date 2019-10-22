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

    public static event Action OnPaused = delegate { };
    public static event Action OnUnpaused = delegate { };

    public override void Awake()
    {
        base.Awake();
        gameObject.name = "Pause Manager";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Paused)
                Unpause();
            else
                Pause();
        }
        Debug.Log(Paused);
    }

    public static void TogglePause()
    {
        if (Paused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
        Debug.Log(Paused);
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
