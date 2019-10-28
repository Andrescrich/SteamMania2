﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component {
    private static T instance;

    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    instance = new GameObject(nameof(T)).AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public virtual void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
