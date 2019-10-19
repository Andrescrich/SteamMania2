using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public override void Awake()
    {
        base.Awake();
        gameObject.name = "GameManager";
    }

    public void HitStop()
    {
        StartCoroutine(StaticMethods.HitStopCor());
    }
}
