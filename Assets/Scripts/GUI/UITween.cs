using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;


[RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
public class UITween : MonoBehaviour
{
	public AnimationCurve easeType;
	public float duration;
	public float delay;


	public bool fade;
	
	public bool move;
	
	public Vector2 moveFrom;
	public Vector2 moveTo;

	public bool scale;

	public bool rotate;
	public int laps;
	private Vector3 destinationRotation => laps * 360 * Vector3.forward;
	private CanvasGroup canvasGroup;
	private RectTransform rectTransform;


	private Vector2 originalPosition;
	private Vector2 originalScale;
	
	public static event Action<UITween> OnComplete = delegate {  };
	public static event Action<UITween> OnStart = delegate {  };
	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		rectTransform = GetComponent<RectTransform>();
	}

	private void Start()
	{
		
		originalPosition = rectTransform.anchoredPosition;
		originalScale = rectTransform.localScale;
	}

	private TweenBase tween;

	
	
	private bool showing;

	public bool Active { get; private set; }
	
	
	public void ShowPanel()
    {
	    if (Active && showing) return;
	    Active = true;
	    showing = true;
	    
	    if (fade)
	    {

		    tween = Tween.CanvasGroupAlpha(canvasGroup, 1, duration, delay, easeType, Tween.LoopType.None,
			    OnStartCallback, OnCompleteCallback, false);
	    }

	    if (move)
	    {
		    tween = Tween.AnchoredPosition(rectTransform, moveTo, duration, delay, easeType, Tween.LoopType.None,
			    OnStartCallback, OnCompleteCallback, false);
	    }

	    if (scale)
	    {
		    tween = Tween.LocalScale(transform, originalScale, duration, delay, easeType, Tween.LoopType.None,
			    OnStartCallback, OnCompleteCallback, false);
		    
	    }

	    if (rotate)
	    {
		    tween = Tween.Rotate(rectTransform, destinationRotation, Space.Self, duration, delay, easeType,
				    Tween.LoopType.None,
				    OnStartCallback, OnCompleteCallback, false);
	    }

    }
	
    private void ResetPosition()
    {
	    if (!move)
	    {
		    rectTransform.anchoredPosition = originalPosition;
		    
	    }
	    if (!fade)
	    {
		    canvasGroup.alpha = 1;
	    }

	    if (!scale)
	    {
		    rectTransform.localScale = originalScale;
	    }
    }

    private void Update()
    {

    }
    public void HidePanel()
    {
	    if (Active) return;
	    Active = true;
	    showing = false;
	    if (fade)
	    {

		    tween = Tween.CanvasGroupAlpha(canvasGroup, 0, duration, delay, easeType, Tween.LoopType.None,
			    OnStartCallback, OnCompleteCallback, false);
	    }

	    if (move)
	    {
		    tween = Tween.AnchoredPosition(rectTransform, moveFrom, duration, delay, easeType, Tween.LoopType.None,
			    OnStartCallback, OnCompleteCallback, false);
	    }
	    	    
	    if (scale)
	    {
		    tween = Tween.LocalScale(transform, Vector3.zero, duration, delay, easeType, Tween.LoopType.None,
			    OnStartCallback, OnCompleteCallback, false);
	    }
	    
	    if (rotate)
	    {
		    tween = Tween.Rotate(rectTransform, destinationRotation, Space.Self, duration, delay, easeType, Tween.LoopType.None,
			    OnStartCallback, OnCompleteCallback, false);
	    }
	    
    }

    private void OnCompleteCallback()
    {
	    OnComplete?.Invoke(this);
	    Active = false;
    }

    private void OnStartCallback()
    {
	    OnStart?.Invoke(this);
	    ResetPosition();
    }

    public void Set(float duration, float delay, AnimationCurve animationCurve, bool fade = false,
	    bool move = false, bool scale = false, bool rotate = false)
    {
	    this.duration = duration;
	    this.delay = delay;
	    this.easeType = animationCurve;
	    this.fade = fade;
	    this.move = move;
	    this.scale = scale;
	    this.rotate = rotate;
    }
}