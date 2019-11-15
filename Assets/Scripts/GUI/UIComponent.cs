using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class UIComponent : MonoBehaviour
{
 
    public ComponentSkinData skinData;

    protected virtual void OnSkinUI()
    {
        
    }

    public virtual void Start()
    {
        OnSkinUI();
    }

    public virtual void Update()
    {
        if (Application.isEditor)
        {
            OnSkinUI();
        }
    }
}

