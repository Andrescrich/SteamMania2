using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : Singleton<CanvasFade>
{
    public override void Awake()
    {
        base.Awake();
        gameObject.name = "CanvasFade";
    }

}
