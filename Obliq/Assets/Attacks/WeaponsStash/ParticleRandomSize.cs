using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRandomSize : MonoBehaviour
{
    [SerializeField]
    Vector2 random_size_min_ = new Vector2(0.0f, 0.0f);
    [SerializeField]
    Vector2 random_size_max_ = new Vector2(0.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector2(Random.Range(random_size_min_.y, random_size_max_.x), Random.Range(random_size_min_.y, random_size_max_.y));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
