using System;
using UnityEngine;


public class LevelEntry : MonoBehaviour
{ 

	public int SpawnID;
	private void Awake()
	{
	}

	public string LevelName;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<PlayerMovement>() != null)
		{
			LevelManager.GetInstance().LoadScene(LevelName, SpawnID);
		}
	}
}
