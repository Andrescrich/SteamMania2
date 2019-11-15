using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;



public class TestHealth : MonoBehaviour
{
	private float health = 10;
	public static event Action<float> OnHealthChanged = delegate {  };
    void Start()
    {
		
    }

    void Update()
    {
	    if (Input.GetMouseButtonDown(0))
	    {
		    health -= 2;
		    OnHealthChanged?.Invoke(health);
		    Tween.Shake(transform, GetComponent<RectTransform>().anchoredPosition, Vector3.one * 20f, 0.3f, 0);
	    }

	    if (Input.GetMouseButtonDown(1))
	    {
		    health = 10;
		    OnHealthChanged?.Invoke(health);
	    }
    }
}
