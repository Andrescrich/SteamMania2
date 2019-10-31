using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;



public class Rotate : MonoBehaviour
{


    private void Start()
    {
        Tween.Rotate(transform, Vector3.forward * 90f, Space.Self, 1f, 0f, Tween.EaseLinear,
            Tween.LoopType.Loop);
    }
}
