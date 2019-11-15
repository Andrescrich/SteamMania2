using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{

	public UISlider healthBar;
	private RectTransform rectTransform;
	private void Awake()
	{
		rectTransform = healthBar.GetComponent<RectTransform>();
	}

}
