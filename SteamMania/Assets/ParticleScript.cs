using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    private ParticleSystem _pS;
    // Update is called once per frame


    private void Awake()
    {
        _pS = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(!_pS.IsAlive())
            Destroy(gameObject);
    }
}
