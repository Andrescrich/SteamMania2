using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image bullet1I;
    public Image bullet2I;
    public Image bullet3I;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        switch (PlayerMovement.Instance.bullets)
        {
            case 3: 
                bullet1I.gameObject.SetActive(true);
                bullet2I.gameObject.SetActive(true);
                bullet3I.gameObject.SetActive(true);
                break;
            case 2:
                bullet1I.gameObject.SetActive(true);
                bullet2I.gameObject.SetActive(true);
                bullet3I.gameObject.SetActive(false);
                break;
            case 1:
                bullet1I.gameObject.SetActive(true);
                bullet2I.gameObject.SetActive(false);
                bullet3I.gameObject.SetActive(false);
                break;
            case 0: 
                bullet1I.gameObject.SetActive(false);
                bullet2I.gameObject.SetActive(false);
                bullet3I.gameObject.SetActive(false);
                break;
        }
    }
}
