using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DDOL : MonoBehaviour
{
    private bool initialized;
    public static DDOL instance { get; private set; }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    

    private void OnEnable()
    {
        var newIns = FindObjectOfType<DDOL>();
        if(newIns!=instance)
        {
            Destroy(gameObject);
        }
    }
}
