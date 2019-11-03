using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
	private Button button;
	private TextMeshProUGUI textComponent;
	[SerializeField] private string text;

	private void Awake()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
		textComponent = GetComponentInChildren<TextMeshProUGUI>();
		textComponent.text = text;
	}

	void OnClick()
	{
		
	}
}
