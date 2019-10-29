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
    private float timeTillDeath = 5f;


    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(13, 9);
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timeTillDeath -= Time.deltaTime;
        if (timeTillDeath < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        _rb.AddForce(bulletV * Time.fixedDeltaTime * transform.right);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Impact();
        if (other.gameObject.layer == 12)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }


    private void Impact()
    {
        bulletParticles.transform.parent = null;
        bulletParticles.transform.localScale = new Vector3(1, 1, 1);
        var particles = bulletParticles.GetComponent<ParticleSystem>();
        particles.Stop();
        ObjectPooler.GetInstance().Spawn("Impact", transform.position, transform.rotation);

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        bulletParticles.SetActive(true);
        bulletParticles.transform.localScale = new Vector3(1, 1, 1);
        timeTillDeath = 5f;
    }
}
