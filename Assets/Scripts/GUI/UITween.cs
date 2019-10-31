using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

[ExecuteInEditMode]
[RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
public class UITween : MonoBehaviour
{
	public AnimationCurve easeType;
	public float duration;
	public float delay;


	public bool fade;
	
	public bool move;
	
	[ShowIf("move")]
	public Vector2 moveFrom;
	[ShowIf("move")]
	public Vector2 moveTo;

	public bool scale;

	public bool rotate;
	[ShowIf("rotate")]
	public int laps;
	private Vector3 destinationRotation => laps * 360 * Vector3.forward;
	private CanvasGroup canvasGroup;
	private RectTransform rectTransform;


	private Vector2 originalPosition;
	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		rectTransform = GetComponent<RectTransform>();
		originalPosition = rectTransform.anchoredPosition;
	}

	private TweenBase tween;

	
	private bool showing;

	public bool Active { get; private set; }

	private void Start()
	{
		HidePanel();
	}

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
		    tween = Tween.LocalScale(transform, Vector3.one, duration, delay, easeType, Tween.LoopType.None,
			    OnStartCallback, OnCompleteCallback, false);
		    
	    }

	    if (rotate)
	    {
		    tween = Tween.Rotate(rectTransform, destinationRotation, Space.Self, duration, delay, easeType,
				    Tween.LoopType.None,
				    OnStartCallback, OnCompleteCallback, false);
	    }
    }

    public void ButtonClick()
    {
	    if (scale)
	    {
		    tween = Tween.LocalScale(transform, Vector3.one, duration, delay, easeType, Tween.LoopType.None,
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
		    rectTransform.localScale = Vector3.one;
	    }
    }
    

    public void HidePanel()
    {
	    if (Active && !showing) return;
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
	    Active = false;
    }

    private void OnStartCallback()
    {
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