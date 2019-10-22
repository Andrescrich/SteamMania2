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
        var direction = new Vector2(-Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical"));
        var directionNorm = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 10f;
        var directionRounded = new Vector2((float)Math.Round(directionNorm.x), (float)Math.Round(directionNorm.y));
        
        if(directionRounded == new Vector2(0, 10))
          Shoot(direction, pSU.position, pSU.rotation);

        if(directionRounded == new Vector2(0, -10))
          Shoot(direction, pSD.position, pSD.rotation);
        
        if (_sR.flipX)
        {
            if (directionRounded == new Vector2(10, 0))
                Shoot(direction, pSR.position, pSR.rotation);
            else if(directionRounded == new Vector2(7, 7))
                Shoot(direction, pSRU.position, pSRU.rotation);
            else if(directionRounded == new Vector2(7, -7))
                Shoot(direction, pSRD.position, pSRD.rotation);
            else if(directionRounded == Vector2.zero)
                Shoot(new Vector2(-1, 0), pSR.position, pSR.rotation);
        }
        else
        {
            if(directionRounded == new Vector2(-7, 7))
                Shoot(direction, pSLU.position, pSLU.rotation);
            else if(directionRounded == new Vector2(-10, 0))
                Shoot(direction, pSL.position, pSL.rotation);
            else if(directionRounded == new Vector2(-7, -7))
                Shoot(direction, pSLD.position, pSLD.rotation);
            else if(directionRounded == Vector2.zero)
                Shoot(new Vector2(1, 0), pSL.position, pSL.rotation);
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

    private IEnumerator Recoiling(Vector2 direction)
    {
        var force = 6500f;
        _pS.recoiling = true;
        _rB.velocity = Vector3.zero;
        while (force > 1000)
        {
            _rB.AddForce(new Vector2(force * direction.x * Time.fixedDeltaTime, force * direction.y * Time.fixedDeltaTime));
            Mathf.SmoothDamp(force, force - 3250, ref force, 1); 
            yield return null;
        }

        _pS.recoiling = false;
    }

    private void Shoot(Vector2 recoilDir, Vector3 position, Quaternion rotation)
    {
        StartCoroutine(Recoiling(recoilDir));
        Instantiate(particle, position, rotation);
        Instantiate(bullet, position, rotation);
    }
}
