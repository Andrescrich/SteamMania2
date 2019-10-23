using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    private ParticleSystem _pS;

    private Transform parent;
    // Update is called once per frame


    private void Awake()
    {
        parent = transform.parent;
        _pS = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!_pS.IsAlive())
        {
            gameObject.SetActive(false);
            transform.parent = parent;
        }
    }
}
