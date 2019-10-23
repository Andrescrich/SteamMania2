using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticle : MonoBehaviour
{
    // Start is called before the first frame update

    private ParticleSystem pS;
    void Start()
    {
        pS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pS.IsAlive())
        {
            gameObject.SetActive(false);
        }
    }
}
