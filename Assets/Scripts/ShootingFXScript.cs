using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class ShootingFXScript : MonoBehaviour
{
    public Transform pSL;
    public Transform pSR;
    public Transform pSRU;
    public Transform pSRD;
    public Transform pSLU;
    public Transform pSLD;
    public Transform pSU;
    public Transform pSD;
    public GameObject particle;
    public GameObject bullet;
    private PlayerMovement _pM;
    private SpriteRenderer _sR;
    private PlayerStates _pS;
    private Rigidbody2D _rB;
    private void Awake()
    {
        _pM = GetComponentInParent<PlayerMovement>();
        _sR = GetComponentInParent<SpriteRenderer>();
        _pS = GetComponentInParent<PlayerStates>();
        _rB = GetComponentInParent<Rigidbody2D>();
    }

    public void PlayParticle()
    {
        if (GetComponentInParent<PlayerMovement>().bullets <= 0) return;
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 10f;
        var directionRounded = new Vector2((float)Math.Round(direction.x), (float)Math.Round(direction.y));
        
        if(directionRounded == new Vector2(10, 0))
          Shoot(-1, pSR.position, pSR.rotation);
   
        if(directionRounded == new Vector2(7, 7))
          Shoot(-1, pSRU.position, pSRU.rotation);
    
        if(directionRounded == new Vector2(0, 10))
          Shoot(-1, pSU.position, pSU.rotation);
     
        if(directionRounded == new Vector2(-7, 7))
          Shoot(1, pSLU.position, pSLU.rotation);
     
        if(directionRounded == new Vector2(-10, 0))
          Shoot(1, pSL.position, pSL.rotation);
     
        if(directionRounded == new Vector2(-7, -7))
          Shoot(1, pSLD.position, pSLD.rotation);
     
        if(directionRounded == new Vector2(0, -10))
          Shoot(-1, pSD.position, pSD.rotation);
     
        if(directionRounded == new Vector2(7, -7))
          Shoot(-1, pSRD.position, pSRD.rotation);
     
        if(directionRounded == Vector2.zero)
        {
          if (_sR.flipX)
              Shoot(-1, pSR.position, pSR.rotation);
          else
              Shoot(1, pSL.position, pSL.rotation);
        }
        GetComponentInParent<PlayerMovement>().bullets--;
    }
    
    public void CanFlip()
    {
        _pM._canFlip = !_pM._canFlip;
    }

    public void ShootFinished()
    {
        _pS.isShoothing = false;
    }

    private IEnumerator Recoiling(float direction)
    {
        var force = new Vector2(200f, 0f);
        _pS.recoiling = true;
        while (force.x > 50)
        {
            _rB.AddForce(direction * force);
            Vector2.SmoothDamp(force, new Vector2(force.x - 100, force.y), ref force, 1); 
            yield return null;
        }

        _pS.recoiling = false;
    }

    private void Shoot(float recoilDir, Vector3 position, Quaternion rotation)
    {
        StartCoroutine(Recoiling(recoilDir));
        Instantiate(particle, position, rotation);
        Instantiate(bullet, position, rotation);
    }
}
