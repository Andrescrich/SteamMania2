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

    public void SetToggle(bool isOn)
    {
	    toggle.isOn = isOn;	  
	    if (clickSound != null)
	    {
		    AudioManager.Play(clickSound);
	    }
    }

    private void OnValueChanged(bool on)
    {
	    
		//SetToggle(on);
    }

}
