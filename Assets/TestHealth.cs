using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;


public class TestHealth : MonoBehaviour
{
	private int maxHealth = 10;
	private float currentHealth = 10;
	[SerializeField]
	private UIProgressBar progressBar;

	private void Start()
	{
		progressBar = GetComponent<UIProgressBar>();
		progressBar.maximum = maxHealth;
		progressBar.SetCurrent(currentHealth);
	}

	void Update()
    {
	    if (Input.GetMouseButtonDown(0))
	    {
		    currentHealth -= 1;
		    progressBar.SetCurrent(currentHealth);
		    Tween.Shake(transform, GetComponent<RectTransform>().anchoredPosition, Vector3.one * 20f, 0.3f, 0);
	    }

	    if (Input.GetMouseButtonDown(1))
	    {
		    currentHealth = 10;
		    progressBar.SetCurrent(currentHealth);
	    }
    }
}
