using System;
using System.Collections;
using System.Collections.Generic;
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

[RequireComponent(typeof(CanvasGroup))]
public class UITweener : MonoBehaviour {
    
    [SerializeField] private bool startHidden;
    
    [SerializeField] private FadeData fadeData; 
    private float fadeValue;
    [SerializeField] private MoveData moveData;

    [SerializeField] private ScaleData scaleData;

    [SerializeField] private RotateData rotateData;
    



    [SerializeField] private AnimationType animationInType;

    [SerializeField] private AnimationType animationOutType;

    [SerializeField] public bool useFadeIn;
    [SerializeField] public bool useMovementIn;
    [SerializeField] public bool useScaleIn;
    [SerializeField] public bool useRotateIn;
    
    [SerializeField] public bool useFadeOut;
    [SerializeField] public bool useMovementOut;
    [SerializeField] public bool useScaleOut;
    
    

    protected CanvasGroup canvasGroup;

    protected RectTransform rectTransform;


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
    
    
    public AnimationType AnimInType
    {
        get => animationInType;

        set => animationInType = value;

    }

    public AnimationType AnimOutType
    {
        get => animationOutType;

        set => animationOutType = value;

    }

    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        if (startHidden)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }
        objectToAnimate = gameObject;

        originalPosition = rectTransform.anchoredPosition3D;
        originalScale = rectTransform.localScale;
        ResetCanvas();
    }

    public void Open()
    {
        ResetCanvas();
        Show();
        canvasGroup.interactable = true;
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
    public void Close()
    {
        ResetCanvas();
        Hide();
        //canvasGroup.interactable = false;
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
    }

    protected void Hide()
    {
        rectTransform.localScale = originalScale;
        canvasGroup.alpha = 0;
    }


    protected void FadeIn()
    {
        rectTransform.localScale = originalScale;

        canvasGroup.alpha = FadeFrom;

        tweenObject = LeanTween.alphaCanvas(canvasGroup, FadeTo, FadeInDuration);

    }

    protected void FadeOut()
    {
        rectTransform.localScale = originalScale;

        canvasGroup.alpha = FadeTo;
        tweenObject = LeanTween.alphaCanvas(canvasGroup, FadeFrom, FadeOutDuration);

    }

    protected void MoveIn()
    {
        rectTransform.anchoredPosition3D = MoveInFrom;

        if (UseDefaultPosition) {
            MoveOriginal = originalPosition;
        }

        tweenObject = LeanTween.move(rectTransform, MoveOriginal, MoveInDuration);

        tweenObject.setDelay(delay);
        tweenObject.setEase(MoveEaseInType);
    }

    protected void MoveOut()
    {
        rectTransform.anchoredPosition3D = MoveOriginal;

        tweenObject = LeanTween.move(rectTransform, MoveOutTo, MoveOutDuration);

        tweenObject.setDelay(delay);
        tweenObject.setEase(MoveEaseOutType);
    }

    protected void ScaleIn()
    {
        rectTransform.localScale = ScaleFrom;
        

        canvasGroup.alpha = 1;

        tweenObject = LeanTween.scale(objectToAnimate, ScaleTo, ScaleInDuration);

        tweenObject.setDelay(delay);
        tweenObject.setEase(ScaleEaseInType);
    }

    protected void ScaleOut()
    {
        rectTransform.localScale = ScaleTo;

        tweenObject = LeanTween.scale(objectToAnimate, ScaleFrom, ScaleOutDuration);

        tweenObject.setDelay(delay);
        tweenObject.setEase(ScaleEaseOutType);
    }

    protected void RotateIn() {

        tweenObject = LeanTween.rotate(rectTransform, RotateTo, RotateInDuration);
        tweenObject.setDelay(delay);
        tweenObject.setEase(RotateEaseInType);
    }



    protected void ResetCanvas()
    {
        rectTransform.anchoredPosition3D = originalPosition;
    }

}