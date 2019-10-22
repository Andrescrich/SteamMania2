using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public UITweener component;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            component.Open();
        }

        if (Input.GetMouseButtonDown(1))
        {
            component.Close();
        }
    }
}
