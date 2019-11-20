using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Toggle))]
public class UIToggle : MonoBehaviour
{
	private Toggle toggle;
	[SerializeField] private Audio clickSound;
	public bool IsOn => toggle.isOn;
    void Awake()
    {
	    toggle = GetComponent<Toggle>();
	    
	    toggle.onValueChanged.AddListener(OnValueChanged);
    }


    private void OnValueChanged(bool on)
    {
	    
	    if (clickSound != null)
	    {
		    AudioManager.Play(clickSound);
	    }
    }

}
