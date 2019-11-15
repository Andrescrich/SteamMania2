using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RotateRectTransform : MonoBehaviour
{
    public float rotateSpeed = 100f;
    public float Multiplier { get; set; }
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        var rotation = rectTransform.localRotation;
        rotation = Quaternion.Euler(rotation.x, rotation.y,Multiplier * rotateSpeed * Time.unscaledTime);
        rectTransform.localRotation = rotation;
    }

}
