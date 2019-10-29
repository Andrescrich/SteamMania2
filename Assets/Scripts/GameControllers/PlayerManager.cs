using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : Singleton<PlayerManager>
{

	public GameObject player;

	protected PlayerManager()
	{
	}

	protected override void Awake()
    {
	    base.Awake();
	    player = Resources.Load("Player/PlayerPrefab") as GameObject;
    }

    void Update()
    {
       	
    }
}
