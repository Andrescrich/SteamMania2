using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class Rotate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private bool left;

    public bool Left
    {
        get { return left; }
        set
        {
            if (value != left)
            {
                HandleRotation();
            }
        }
    }

    private TweenBase tween;

    private void Start()
    {
        HandleRotation();
    }

    void HandleRotation()
    {
        if (Left)
        {
            if(tween!=null)
                tween.Stop();
            tween = Tween.Rotate(transform, -Vector3.forward * 90f, Space.Self, 1f, 0f, Tween.EaseLinear,
                Tween.LoopType.Loop);
        }
        else
        {
            if(tween!=null)
                tween.Stop();
            tween = Tween.Rotate(transform, Vector3.forward * 90f, Space.Self, 1f, 0f, Tween.EaseLinear,
                Tween.LoopType.Loop);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.yellow;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.white;
    }
}
