using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.EventSystems;


public class HandleSliderController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float rotateSpeed = 100f;
    public float Multiplier { get; set; }
    [SerializeField]
    private Vector3 originalScale;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    void Update()
    {
        var rotation = rectTransform.localRotation;
        rotation = Quaternion.Euler(rotation.x, rotation.y,Multiplier * rotateSpeed * Time.unscaledTime);
        rectTransform.localRotation = rotation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.Lerp(originalScale, originalScale * 1.5f, 0.6f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.Lerp(rectTransform.localScale,originalScale, 0.6f);
    }
}
