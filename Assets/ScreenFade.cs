using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(UITween), typeof(Image)), RequireComponent(typeof(Canvas))]
public class ScreenFade : Singleton<ScreenFade>
{
    private UITween tween;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    public Image fadeImage;

    public float duration = 0.35f;
    public float delay = .1f;
    public AnimationCurve easeType = Tween.EaseInOutStrong;

    public bool Active => tween.Active;

    protected  override void Awake()
    {
        base.Awake();
        tween = GetComponent<UITween>();
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        fadeImage = GetComponent<Image>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
        fadeImage.color = Color.black;
        fadeImage.sprite = Resources.Load<Sprite>("Sprites/cubo");
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
        tween.Set(duration, delay, easeType, true, false, false, false);
        FadeOut();
    }

    public void FadeIn()
    {
        tween.ShowPanel();
    }

    public void FadeOut()
    {
        tween.HidePanel();
    }

    public void Block()
    {
        canvasGroup.alpha = 1;
    }
}
