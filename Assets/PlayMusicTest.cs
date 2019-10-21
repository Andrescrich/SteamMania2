using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicTest : MonoBehaviour
{

    public Audio inMusic;

    public Audio outMusic;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayDelayed(inMusic,1f,gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AudioManager.Stop(inMusic, gameObject);
        }        
        if (Input.GetKeyDown(KeyCode.C))
        {
            AudioManager.FadeOut(outMusic);
            AudioManager.FadeIn(inMusic);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            AudioManager.FadeOut(inMusic);
            AudioManager.FadeIn(outMusic);
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Pause(inMusic);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            AudioManager.Resume(inMusic);
        }
        
    }
}
