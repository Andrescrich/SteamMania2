using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum UITweenType
{
    MOVE,
    SCALE,
    FADE,
    ROTATE
}

[RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
public class UITween : MonoBehaviour
{
    public GameObject objectToAnimate;

    public UITweenType animationType;
    public LeanTweenType easeType;
    public float duration;
    public float delay;

    public bool loop;
    public bool pingpong;

    public bool startPositionOffset;
    public Vector3 from;
    public Vector3 to;

    private LTDescr tweenObject;

    public bool showOnEnable;
    public bool workOnDisable;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    public void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        if (showOnEnable)
        {
            Show();
        }
    }

    public void Show()
    {
        HandleTween();
    }

    public bool Active { get; private set; }

    void HandleTween()
    {
        Active = true;
        if (objectToAnimate == null)
        {
            objectToAnimate = gameObject;
        }

        switch (animationType)
        {            
            case UITweenType.FADE:
                Fade();
                break;
            case UITweenType.MOVE:
                Move();
                break;
            case UITweenType.SCALE:
                Scale();
                break;
            case UITweenType.ROTATE:
                Rotate();
                break;
        }

        tweenObject.setDelay(delay);
        tweenObject.setEase(easeType);
        tweenObject.setOnComplete(() => { Active = false; });

        if (loop)
        {
            tweenObject.loopCount = int.MaxValue;
        }

        if (pingpong)
        {
            tweenObject.setLoopPingPong();
        }
    }

    void Fade()
    {
        if (startPositionOffset)
        {
            canvasGroup.alpha = @from.x;
        }

        tweenObject = LeanTween.alphaCanvas(canvasGroup, to.x, duration);
    }

    void Move()
    {
        rectTransform.anchoredPosition = @from;

        tweenObject = LeanTween.move(rectTransform, to, duration);
    }

    void Scale()
    {
        if (startPositionOffset)
        {
            rectTransform.localScale = @from;
        }

        tweenObject = LeanTween.scale(objectToAnimate, to, duration);
    }

    void Rotate()
    {
        if (startPositionOffset)
        {
            rectTransform.anchoredPosition = @from;
        }

        tweenObject = LeanTween.rotate(rectTransform, to, duration);
    }

    void SwapDirection()
    {
        var temp = @from;
        from = to;
        to = temp;
    }

    public void Disable()
    {
        SwapDirection();
        HandleTween();

        tweenObject.setOnComplete(() =>
        {
            SwapDirection();
        });
    }

    public void Disable(Action onCompleteAction)
    {
        SwapDirection();
        HandleTween();
        tweenObject.setOnComplete(() =>
        {
            SwapDirection();
        });
        tweenObject.setOnComplete(onCompleteAction);
    }
    
    
}
