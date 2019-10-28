using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UITween))]
public class ScreenFade : MonoBehaviour
{
    private UITween tween;
    private CanvasGroup canvasGroup;

    public bool Active => tween.Active;

    private void Awake()
    {
        tween = GetComponent<UITween>();
        canvasGroup = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(gameObject);
    }

    public void FadeIn()
    {
        tween.Show();
    }

    public void FadeOut()
    {
        tween.Disable();
    }

    public void Block()
    {
        canvasGroup.alpha = 1;
    }
}
