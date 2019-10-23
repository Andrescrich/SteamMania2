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

    }
}
