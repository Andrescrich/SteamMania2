using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum AnimationType
{
    NONE,
    FADE,
    MOVEMENT,
    ROTATE,
    SCALE,
}

[RequireComponent(typeof(CanvasGroup),typeof(RectTransform))]
public class UITweener : MonoBehaviour
{


    [SerializeField] private FadeData fadeData;
    private float fadeValue;
    [SerializeField] private MoveData moveData;

    [SerializeField] private ScaleData scaleData;

    [SerializeField] private RotateData rotateData;
    

    [SerializeField] public bool startHidden;
    
    [SerializeField] public bool useFadeIn;
    [SerializeField] public bool useMovementIn;
    [SerializeField] public bool useScaleIn;
    [SerializeField] public bool useRotateIn;

    [SerializeField] public bool useFadeOut;
    [SerializeField] public bool useMovementOut;
    [SerializeField] public bool useScaleOut;
    [SerializeField] public bool useRotateOut;



    private CanvasGroup canvasGroup;

    private RectTransform rectTransform;


    private Vector3 moveValue;

    private Vector3 scaleValue;



    public Vector3 originalPosition;/*
    {
        get
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }
            return rectTransform.anchoredPosition3D;
        }
        private set
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
                rectTransform.anchoredPosition3D = value;
            }
        }
            
    }
*/
    private Vector3 originalScale;

    private GameObject objectToAnimate;
    private LeanTweenType easeType;
    private float duration;
    private float delay;

    private bool loop;
    private bool pingpong;
    protected LTDescr tweenObject;


    public bool StartHidden
    {
        get => startHidden;
        set => startHidden = value;
    }

    #region Move Properties
    public float MoveInDuration
    {
        get => moveData.moveInDuration;
        set => moveData.moveInDuration = value;
    }
    public float MoveOutDuration
    {
        get => moveData.moveOutDuration;
        set => moveData.moveOutDuration = value;
    }
    public Vector3 MoveInFrom
    {
        get => moveData.moveInFrom;
        set => moveData.moveInFrom = value;
    }

    public Vector3 MoveOriginal
    {
        get => moveData.moveOriginal;
        set => moveData.moveOriginal = value;
    }


    public Vector3 MoveOutTo
    {
        get => moveData.moveOutTo;
        set => moveData.moveOutTo = value;
    }

    public bool UseDefaultPosition
    {
        get => moveData.useDefaultPosition;
        set => moveData.useDefaultPosition = value;
    }

    public LeanTweenType MoveEaseInType
    {
        get => moveData.moveEaseInType;
        set => moveData.moveEaseInType = value;
    }

    public LeanTweenType MoveEaseOutType
    {
        get => moveData.moveEaseOutType;
        set => moveData.moveEaseOutType = value;
    }
    #endregion
    
    #region Fade Properties

    public float FadeInDuration
    {
        get => fadeData.fadeInDuration;
        set => fadeData.fadeInDuration = value;
    }

    public float FadeOutDuration
    {
        get => fadeData.fadeOutDuration;
        set => fadeData.fadeOutDuration = value;
    }

    public float FadeFrom
    {
        get => fadeData.fadeFrom;
        set => fadeData.fadeFrom = value;
    }

    public float FadeTo
    {
        get => fadeData.fadeTo;
        set => fadeData.fadeTo = value;
    }

    #endregion

    #region Scale Properties

    public LeanTweenType ScaleEaseInType
    {
        get => scaleData.scaleEaseInType;
        set => scaleData.scaleEaseInType = value;
    }
    public LeanTweenType ScaleEaseOutType
    {
        get => scaleData.scaleEaseOutType;
        set => scaleData.scaleEaseOutType = value;
    }

    public float ScaleInDuration
    {
        get => scaleData.scaleInDuration;
        set => scaleData.scaleInDuration = value;
    }

    public float ScaleOutDuration
    {
        get => scaleData.scaleOutDuration;
        set => scaleData.scaleOutDuration = value;
    }

    public Vector3 ScaleFrom
    {
        get => scaleData.scaleFrom;
        set => scaleData.scaleFrom = value;
    }
    public Vector3 ScaleTo
    {
        get => scaleData.scaleTo;
        set => scaleData.scaleTo = value;
    }

    #endregion

    #region Rotate Properties

    public LeanTweenType RotateEaseInType
    {
        get => rotateData.rotateEaseInType;
        set => rotateData.rotateEaseInType = value;
    }
    public LeanTweenType RotateEaseOutType
    {
        get => rotateData.rotateEaseOutType;
        set => rotateData.rotateEaseOutType = value;
    }

    public float RotateInDuration
    {
        get => rotateData.rotateInDuration;
        set => rotateData.rotateInDuration = value;
    }
    
    public float RotateOutDuration
    {
        get => rotateData.rotateOutDuration;
        set => rotateData.rotateOutDuration = value;
    }
    public float RotateFrom
    {
        get => rotateData.rotateFrom;
        set => rotateData.rotateFrom = value;
    }
    public float RotateTo
    {
        get => rotateData.rotateTo;
        set => rotateData.rotateTo = value;
    }

    #endregion

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Start()
    {
        if (startHidden)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }

        canvasGroup.blocksRaycasts = false;
        objectToAnimate = gameObject;
        originalPosition = rectTransform.anchoredPosition3D;
        originalScale = rectTransform.localScale;
        ResetCanvas();
    }

    public bool active;
    public void Open()
    {
        if (active) return;
        active = true;
        ResetCanvas();
        Show();
        if (useFadeIn)
        {
            FadeIn();
        }

        if (useMovementIn)
        {
            MoveIn();
        }

        if (useRotateIn)
        {
            RotateIn();
        }

        if (useScaleIn)
        {
            ScaleIn();
        }
        


    }
    public void Close(bool fast = false)
    {
        if (fast)
        {
            Hide();
        }
        
        ResetCanvas();
        if (useFadeOut)
        {
            FadeOut();
        }
        if (useMovementOut)
        {
            MoveOut();
        }

        if (useScaleOut)
        {
            ScaleOut();
        }
        
        
        
    }

    protected void Show()
    {
        rectTransform.localScale = originalScale;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    protected void Hide()
    {
        rectTransform.localScale = originalScale;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        active = false;
    }


    protected void FadeIn()
    {
        rectTransform.localScale = originalScale;

        canvasGroup.alpha = FadeFrom;

        var tweenFade = LeanTween.alphaCanvas(canvasGroup, FadeTo, FadeInDuration);
        tweenFade.setIgnoreTimeScale(true);
        tweenFade.setOnComplete(() =>
        {
            active = false;
        });
    }

    protected void FadeOut()
    {
        
        rectTransform.localScale = originalScale;

        canvasGroup.alpha = FadeTo;
        var tweenFade = LeanTween.alphaCanvas(canvasGroup, FadeFrom, FadeOutDuration);
        tweenFade.setIgnoreTimeScale(true);
        tweenFade.setOnComplete(() =>
        {
            active = false;
        });
    }

    protected void MoveIn()
    {
        rectTransform.anchoredPosition3D = MoveInFrom;

        if (UseDefaultPosition)
        {
            MoveOriginal = originalPosition;
        }

        var tweenFade = LeanTween.move(rectTransform, MoveOriginal, MoveInDuration);

        tweenFade.setDelay(delay);
        tweenFade.setEase(MoveEaseInType);
        tweenFade.setIgnoreTimeScale(true);
        tweenFade.setOnComplete(() =>
        {
            active = false;
        });
    }

    protected void MoveOut()
    {
        rectTransform.anchoredPosition3D = MoveOriginal;

        var tweenFade = LeanTween.move(rectTransform, MoveOutTo, MoveOutDuration);

        tweenFade.setDelay(delay);
        tweenFade.setEase(MoveEaseOutType);
        tweenFade.setIgnoreTimeScale(true);
        tweenFade.setOnComplete(() =>
        {
            active = false;
        });
    }

    protected void ScaleIn()
    {
        rectTransform.localScale = ScaleFrom;


        canvasGroup.alpha = 1;

        var tweenFade = LeanTween.scale(objectToAnimate, ScaleTo, ScaleInDuration);

        tweenFade.setDelay(delay);
        tweenFade.setEase(ScaleEaseInType);
        tweenFade.setIgnoreTimeScale(true);
        tweenFade.setOnComplete(() =>
        {
            active = false;
        });
    }

    protected void ScaleOut()
    {
        rectTransform.localScale = ScaleTo;

        var tweenFade = LeanTween.scale(objectToAnimate, ScaleFrom, ScaleOutDuration);

        tweenFade.setDelay(delay);
        tweenFade.setEase(ScaleEaseOutType);
        tweenFade.setIgnoreTimeScale(true);
        tweenFade.setOnComplete(() =>
        {
            active = false;
        });
    }

    protected void RotateIn()
    {

        var tweenFade = LeanTween.rotate(rectTransform, RotateTo, RotateInDuration);
        tweenFade.setDelay(delay);
        tweenFade.setEase(RotateEaseInType);
        tweenFade.setIgnoreTimeScale(true);
        tweenFade.setOnComplete(() =>
        {
            active = false;
        });
    }

    void RotateOut()
    {
        var tweenFade = LeanTween.rotate(rectTransform, RotateFrom, RotateOutDuration);
        tweenFade.setDelay(delay);
        tweenFade.setEase(RotateEaseOutType);
        tweenFade.setIgnoreTimeScale(true);
        tweenFade.setOnComplete(() =>
        {
            active = false;
        });
    }



    protected void ResetCanvas()
    {
        rectTransform.anchoredPosition3D = originalPosition;
    }

}