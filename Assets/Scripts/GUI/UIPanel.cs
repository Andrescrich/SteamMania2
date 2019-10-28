using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UITweener))]
public class UIPanel : MonoBehaviour
{
    private UITweener tweener;

    public Button closeButton;

    public TextMeshProUGUI titleText;

    [SerializeField] private string windowTitle;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<UITweener>();
        titleText.text = windowTitle;
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    void OnClickCloseButton()
    {
        PauseManager.TogglePause();
    }
}
