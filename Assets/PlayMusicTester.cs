using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayMusicTester : MonoBehaviour
{

	public Audio sound;
    void Start()
    {
	
        if(!AudioManager.IsPlaying(sound))
			AudioManager.PlayDelayed(sound, 1f, gameObject);
    }

}
