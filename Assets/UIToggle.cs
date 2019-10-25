using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIToggle : MonoBehaviour
{

    private Toggle toggle;

    private UISettings settings;

    public bool IsOn => toggle.isOn;
    
    // Start is called before the first frame update
    void Start()
    {
        settings = GetComponentInParent<UISettings>();
        toggle = GetComponent<Toggle>();
    }

    public void SetToggle(bool on)
    {
        toggle.isOn = on;
    }

}
