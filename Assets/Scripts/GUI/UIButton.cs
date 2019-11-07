using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.String;


[RequireComponent(typeof(Button))]
public class UIButton : UIComponent
{
	public enum ButtonType
	{
		Default,
		Confirm,
		Decline
	}
	private Button button;
	private Image image;
	public ButtonType buttonType;
	private TextMeshProUGUI textComponent;
	[SerializeField] private string text;

	public  void Awake()
	{
		button = GetComponent<Button>();
		image = GetComponent<Image>();
		button.onClick.AddListener(OnClick);
		textComponent = GetComponentInChildren<TextMeshProUGUI>();

	}
	
	

	void OnClick()
	{

	}

	protected override void OnSkinUI()
	{
		base.OnSkinUI();
		button = GetComponent<Button>();
		image = GetComponent<Image>();
		
		button.transition = Selectable.Transition.ColorTint;
		/*
		button.transition = Selectable.Transition.SpriteSwap;
		button.targetGraphic = image;
		image.sprite = SkinData.sprite;
		button.spriteState = SkinData.spriteState;
		image.type = Image.Type.Sliced;
		*/
		if (textComponent != null)
		{
			if (text != textComponent.text && text != Empty)
			{
				textComponent.text = text;
			}
		}

		var newColors = button.colors;
		newColors.highlightedColor = skinData.onHighlighted;
		newColors.pressedColor = skinData.onPressed;
		newColors.selectedColor = skinData.onSelected;
		newColors.disabledColor = skinData.onDisabled;
		button.colors = newColors;

		switch (buttonType)
		{
			case ButtonType.Default:
				image.color = skinData.defaultColor;
				break;
			case ButtonType.Confirm:
				image.color = skinData.confirmColor;
				break;
			case ButtonType.Decline:
				image.color = skinData.declineColor;
				break;
		}



	}
}
