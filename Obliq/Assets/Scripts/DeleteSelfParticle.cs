using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelfParticle : MonoBehaviour
{

    private ParticleSystem ps;
    [SerializeField] bool delete_self;

    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                if (delete_self)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
