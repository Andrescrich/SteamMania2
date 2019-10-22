using System;
using UnityEngine;

[System.Serializable]
public struct FadeData : AnimationData
{
    [SerializeField] public float fadeInDuration;
    [SerializeField] public float fadeOutDuration;
    [SerializeField] public Vector2 fadeAlpha;
    [SerializeField] public float fadeFrom, fadeTo;
}

public interface AnimationData
{
}

[Serializable]
public struct MoveData : AnimationData
{
    [SerializeField] public float moveInDuration;
    [SerializeField] public float moveOutDuration;
    [SerializeField] public LeanTweenType moveEaseInType;
    [SerializeField] public LeanTweenType moveEaseOutType;
    [SerializeField] public bool useDefaultPosition;
    [SerializeField] public Vector3 moveInFrom, moveOriginal, moveOutTo;
}

[Serializable]
public struct ScaleData : AnimationData 
{
    [SerializeField] public Vector3 scaleFrom, scaleTo;
    [SerializeField] public float scaleInDuration;
    [SerializeField] public float scaleOutDuration;
    [SerializeField] public LeanTweenType scaleEaseInType;
    [SerializeField] public LeanTweenType scaleEaseOutType;
}

[Serializable]
public struct RotateData : AnimationData
{
    [SerializeField] public float rotateFrom, rotateTo;
    [SerializeField] public float rotateInDuration;
    [SerializeField] public LeanTweenType rotateEaseInType;
    [SerializeField] public LeanTweenType rotateEaseOutType;
}