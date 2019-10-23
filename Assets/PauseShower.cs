using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PauseShower : MonoBehaviour
{
    private Image pauseImage;

    private void Awake()
    {
        pauseImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.Paused)
        {
            pauseImage.enabled = true;
        }
        else
        {
            pauseImage.enabled = false;
        }
    }
}
