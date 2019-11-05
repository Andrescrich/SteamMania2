using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.String;


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
		if (text != Empty)
		{
			textComponent.text = text;
		}
	}

	void OnClick()
	{

	}
}
