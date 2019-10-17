using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFXScript : MonoBehaviour
{
    public Transform _pSL;
    public Transform _pSR;
    public GameObject particle;
    public GameObject bullet;
    private PlayerMovement _pM;
    private SpriteRenderer _sR;
    private PlayerStates _pS;

    private void Awake()
    {
        _pM = GetComponentInParent<PlayerMovement>();
        _sR = GetComponentInParent<SpriteRenderer>();
        _pS = GetComponentInParent<PlayerStates>();
    }

    public void PlayParticle()
    {
        if (_sR.flipX)
        {
            Instantiate(particle, _pSR.transform.position, _pSR.transform.rotation);
            Instantiate(bullet, _pSR.transform.position, _pSR.transform.rotation);
        }
        else
        {
            Instantiate(particle, _pSL.transform.position, _pSL.transform.rotation);
            Instantiate(bullet, _pSL.transform.position, _pSL.transform.rotation);
        }
    }
    
    public void CanFlip()
    {
        _pM._canFlip = !_pM._canFlip;
    }

    public void ShootFinished()
    {
        _pS.isShoothing = false;
    }
}
