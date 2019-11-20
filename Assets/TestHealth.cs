using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;


public class TestHealth : MonoBehaviour
{
	private float health = 10;
	[SerializeField]
	private UISlider slider;

	private void Start()
	{
		slider = GetComponent<UISlider>();
	}

	void Update()
    {
	    if (Input.GetMouseButtonDown(0))
	    {
		    health -= 2;
		    slider.SetValue(health);
		    Tween.Shake(transform, GetComponent<RectTransform>().anchoredPosition, Vector3.one * 20f, 0.3f, 0);
	    }

	    if (Input.GetMouseButtonDown(1))
	    {
		    health = 10;
		    slider.SetValue(health);
	    }
    }
}
