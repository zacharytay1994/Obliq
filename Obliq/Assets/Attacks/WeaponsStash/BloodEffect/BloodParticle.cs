using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{
    [SerializeField]
    float life_span_ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Random.Range(2.0f, life_span_));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
