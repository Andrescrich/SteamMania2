using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnButtonClick);
    }

    private void Update()
    {
    }

    private void OnButtonClick()
    {
        PauseManager.Unpause();
    }
}
