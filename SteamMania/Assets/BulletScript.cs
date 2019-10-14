using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private Rigidbody2D _rb;
    public GameObject bulletParticles;
    public GameObject impactParticle;
    [SerializeField] private float bulletV = 500f;


    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(13, 9);
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(bulletV * Time.fixedDeltaTime * transform.right);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Impact();
    }


    private void Impact()
    {
        bulletParticles.transform.parent = null;
        bulletParticles.transform.localScale = new Vector3(1, 1, 1);
        var particles = bulletParticles.GetComponent<ParticleSystem>();
        particles.Stop();
        Instantiate(impactParticle, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
