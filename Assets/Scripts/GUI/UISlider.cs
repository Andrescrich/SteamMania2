using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class UISlider : MonoBehaviour
{
	public Slider Slider { get; private set; }

	public int maximum;
	public int minimum;
	public float current;
	private RotateRectTransform handleArea;

	private void Awake()
	{
		Slider = GetComponent<Slider>();
		current = Slider.value;
		handleArea = GetComponentInChildren<RotateRectTransform>();
		// TODO Pasarle el valor de la vida
		TestHealth.OnHealthChanged += SetValue;
	}

    public void SetValue(float newValue)
    {
	    float currentOffset = newValue - minimum;
	    float maximumOffset = maximum - minimum;
	    float fillAmount = currentOffset / maximumOffset;
	    Slider.value = current = fillAmount;
    }

    public void SetMultiplier(float value)
    {
	    handleArea.Multiplier = value;
    }
}
