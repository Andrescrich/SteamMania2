using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(UITween))]
public class ScreenFade : Singleton<ScreenFade>
{
    private UITween tween;
    private Image fadeImage;

    public bool Active => tween.Active;

    protected override void Awake()
    {
        base.Awake();
        gameObject.name = "ScreenFade";
        tween = GetComponent<UITween>();
        
    }

    private void Start()
    {
        //TRUQUITO PORQUE NO SE QUE PASA QUE NO HACE EL FADEOUT AL PRINCIPIO, PONE CANVASGROUP.ALPHA A 0
        FadeIn();
        Invoke("FadeOut", 0.3f);
    }

    public void FadeIn()
    {
        tween.ShowPanel();
    }

    public void FadeOut()
    {
        tween.HidePanel();
    }

}
