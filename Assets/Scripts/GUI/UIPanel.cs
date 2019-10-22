using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UITweener))]
public class UIPanel : MonoBehaviour
{
    private UITweener tweener;

    public Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<UITweener>();
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    void OnClickCloseButton()
    {
        PauseManager.Unpause();
    }
}
