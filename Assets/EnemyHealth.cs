using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private void Update()
    {
        if(currentHealth <= 0)
            Death();
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
