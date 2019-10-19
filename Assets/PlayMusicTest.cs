using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicTest : MonoBehaviour
{

    public Audio mainMusic;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Play(mainMusic, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AudioManager.Stop(mainMusic, gameObject);
        }
        /*
        if (Input.GetKeyDown(KeyCode.C))
        {
            AudioManager.Play(mainMusic);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            AudioManager.Stop(mainMusic);
        }
        */
    }
}
