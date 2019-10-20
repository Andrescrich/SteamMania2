using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicTest : MonoBehaviour
{

    public Audio mainMusic;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayDelayed(mainMusic,1f,gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AudioManager.Stop(mainMusic, gameObject);
        }        
        if (Input.GetKeyDown(KeyCode.C))
        {
            AudioManager.FadeIn(mainMusic);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            AudioManager.FadeOut(mainMusic);
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Pause(mainMusic);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            AudioManager.Resume(mainMusic);
        }
        
    }
}
