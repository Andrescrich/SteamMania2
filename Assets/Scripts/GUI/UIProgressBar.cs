using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    public int maximum;
    public int minimum;
    public float current;

    public Image fill;

    // Update is called once per frame
    void Update()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        fill.fillAmount = fillAmount;
    }

    public void SetCurrent(float value)
    {
        current = value;
    }
}
