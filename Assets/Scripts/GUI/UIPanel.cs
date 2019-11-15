using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    public Button closeButton;

    public TextMeshProUGUI titleText;

    public Canvas canvas;

    [SerializeField] private string windowTitle;

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = windowTitle;
        canvas = GetComponent<Canvas>();
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    void OnClickCloseButton()
    {
        if(PauseManager.Paused)
            PauseManager.TogglePause();
    }
}
