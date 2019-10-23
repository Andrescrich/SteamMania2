using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIToggle : MonoBehaviour
{
    public Image onImage;
    public Image offImage;

    private Toggle toggle;
    
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggle);
        toggle.isOn = SaveSystem<int>.LoadInt("ScreenKey") == 1;
    }

    void OnToggle(bool on)
    {
        Screen.fullScreen = on;
        if (on)
        {
            
            onImage.gameObject.SetActive(true);
            offImage.gameObject.SetActive(false);
        }
        else
        {
            onImage.gameObject.SetActive(false);
            offImage.gameObject.SetActive(true);
        }
    }

    private void OnApplicationQuit()
    {
        if(toggle.isOn)
            SaveSystem<int>.SavePrefs("ScreenKey", 1);
        else
        {
            SaveSystem<int>.SavePrefs("ScreenKey", 0);
        }
    }
}
