using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroup : MonoBehaviour
{

    public bool startHidden;

    public bool overrideAnimationInType;

    public bool overrideAllIn;
    public bool overrideAllOut;

    [SerializeField]
    public AnimationType animationInType;


    public bool overrideInDuration;
    public float InDuration;

    public bool overrideInFadeTo;
    public float FadeToIn;


    public bool overrideAnimationOutType;
    [SerializeField] public AnimationType animationOutType;

    public bool overrideOutDuration;
    public float OutDuration;
    public bool overrideOutFadeTo;
    public float FadeToOut;

    [SerializeField] private List<UITweener> components;

    public List<UITweener> Components
    {
        get => components;
        set => components = value;
    }


    private void Awake()
    {
        foreach (var component in components)
        {
            component.StartHidden = startHidden;
            if(overrideAllIn)
                SetAnimation(component, animationInType, true);
            if(overrideAllOut)
                SetAnimation(component, animationOutType, false);
        }
    }

    private void Update()
    {
        foreach (var component in components)
        {
            component.StartHidden = startHidden;
            if(overrideAllIn)
                SetAnimation(component, animationInType, true);
            if(overrideAllOut)
              SetAnimation(component, animationOutType, false);
        }
    }
    private void SetAnimation(UITweener component, AnimationType animationType, bool toggleIn)
    {
        if (toggleIn)
        {
            if (overrideAnimationInType)
                component.AnimInType = animationType;
            if (overrideInDuration)
            {
                component.FadeInDuration = InDuration;

                component.MoveInDuration = InDuration;

                component.ScaleInDuration = InDuration;

                component.RotateInDuration = InDuration;

                if (overrideInFadeTo)
                {
                    component.FadeTo = FadeToIn;
                }
            }
        }
        else
        {
            if (overrideAnimationOutType)
                component.AnimOutType = animationType;
            if (overrideOutDuration)
            {
                component.FadeOutDuration = OutDuration;

                component.MoveOutDuration = OutDuration;

                component.ScaleOutDuration = OutDuration;
            }

            if (overrideOutFadeTo)
            {
                component.FadeTo = FadeToOut;
            }

        }
    }

    public void ShowGroup()
    {
        foreach (var component in components)
        {
            component.Open();
        }
    }

    public void HideGroup()
    {
        foreach (var component in components)
        {
            component.Close();
        }
    }
}