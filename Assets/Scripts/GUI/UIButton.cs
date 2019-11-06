using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.String;

[ExecuteInEditMode]
[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
	private Button button;
	public ButtonSkin skin;
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

[CreateAssetMenu(menuName = "UI/ButtonSkin", fileName = "New Button Skin")]
public class ButtonSkin : ScriptableObject
{
	public Color color;
	public Sprite normal;
}
