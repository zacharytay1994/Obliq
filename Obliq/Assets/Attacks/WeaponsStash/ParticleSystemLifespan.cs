using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemLifespan : MonoBehaviour
{
    [SerializeField]
    bool persistent_ = false;
    [SerializeField]
    Vector2 random_life_span_ = new Vector2(0.0f,0.0f);
    // Start is called before the first frame update
    void Start()
    {
        if (!persistent_)
        {
            Destroy(gameObject, Random.Range(random_life_span_.x, random_life_span_.y));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
