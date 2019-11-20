using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class UISlider : MonoBehaviour
{
	public Slider Slider;

	public int maximum;
	public int minimum;
	public float current;
	private HandleSliderController handleArea;

	private void Awake()
	{
		Slider = GetComponent<Slider>();
		handleArea = GetComponentInChildren<HandleSliderController>();
		// TODO Pasarle el valor de la vida
		
	}

	private void Start()
	{
		Slider.minValue = minimum;
		Slider.maxValue = maximum;
	}

	public void SetValue(float newValue)
	{
		Mathf.Clamp(newValue, minimum, maximum);
		/*
	    float currentOffset = newValue - minimum;
	    float maximumOffset = maximum - minimum;
	    float fillAmount = currentOffset / maximumOffset;
	    */
	    Slider.value = current = newValue;
    }

    public void SetMultiplier(float value)
    {
	    handleArea.Multiplier = value;
    }
}
