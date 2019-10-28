﻿using System;
using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class FadeData : AnimationData
{
    [SerializeField] public float fadeInDuration = 1f;
    [SerializeField] [Slider(0,1)] public float fadeOutDuration = 1f;
    [SerializeField] public Vector2 fadeAlpha;
    [SerializeField] public float fadeFrom = 0f, fadeTo = 1f;
}

public interface AnimationData
{
}

[Serializable]
public class MoveData : AnimationData
{
    [SerializeField] public float moveInDuration = 0.6f;
    [SerializeField] public float moveOutDuration = 0.6f;
    [SerializeField] public LeanTweenType moveEaseInType = LeanTweenType.easeOutBack;
    [SerializeField] public LeanTweenType moveEaseOutType = LeanTweenType.easeInBack;
    [SerializeField] public bool useDefaultPosition = true;
    [SerializeField] public Vector3 moveInFrom = new Vector3(0,-2400,0);
    [SerializeField] public Vector3 moveOriginal = Vector3.zero;
    [SerializeField] public Vector3 moveOutTo = new Vector3(0,2400,0);
}

[Serializable]
public class ScaleData : AnimationData
{
    [SerializeField] public Vector3 scaleFrom = Vector3.zero, scaleTo = Vector3.one;
    [SerializeField] public float scaleInDuration = 0.6f;
    [SerializeField] public float scaleOutDuration = 0.6f;
    [SerializeField] public LeanTweenType scaleEaseInType = LeanTweenType.easeOutCirc;
    [SerializeField] public LeanTweenType scaleEaseOutType = LeanTweenType.easeInCirc;
}

[Serializable]
public class RotateData : AnimationData
{
    [SerializeField] public float rotateFrom, rotateTo;
    [SerializeField] public float rotateInDuration = 0.6f;
    [SerializeField] public float rotateOutDuration = 0.6f;
    [SerializeField] public LeanTweenType rotateEaseInType = LeanTweenType.easeOutCirc;
    [SerializeField] public LeanTweenType rotateEaseOutType = LeanTweenType.easeInCirc;
}