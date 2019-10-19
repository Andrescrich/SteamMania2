using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private Animator anim;
    private static readonly int Hit = Animator.StringToHash("Hit");
    private SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
   //     sr.color = Color.Lerp(sr.color, new Color(255, 0, 0), 5);
    }

    private void Update()
    {
        if(currentHealth <= 0)
            Death();
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
       // anim.SetTrigger(Hit);
       StartCoroutine(TakeDamageFx());
    }

    private IEnumerator TakeDamageFx()
    {
        var spriteColor = sr.color;
        sr.color = Color.Lerp(sr.color, Color.red, 5f);
        yield return new WaitForSeconds(.1f);
        sr.color = Color.Lerp(sr.color, Color.white, 5f);
        yield return new WaitForSeconds(.1f);
    }
}
