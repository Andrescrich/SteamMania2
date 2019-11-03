using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class UISliderBar : MonoBehaviour
{
	private Slider slider;

	public int maximum;
	public int minimum;

	private void Awake()
	{
		slider = GetComponent<Slider>();
		// TODO Pasarle el valor de la vida
		TestHealth.OnHealthChanged += SetValue;
	}

    public void SetValue(float newValue)
    {
	    float currentOffset = newValue - minimum;
	    float maximumOffset = maximum - minimum;
	    float fillAmount = currentOffset / maximumOffset;
	    slider.value = fillAmount;
    }
}
