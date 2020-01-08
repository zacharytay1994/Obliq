using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemParticle : MonoBehaviour
{
    Transform parent_ = null;
    [SerializeField]
    float speed_ = 2.0f;
    [SerializeField]
    float threshold_ = 0.01f;
    [SerializeField]
    float life_span_ = 5.0f;

    SpriteRenderer sr_ = null;
    [SerializeField]
    float alpha_speed_ = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        sr_ = GetComponent<SpriteRenderer>();
        // set alpha to 0
        Color temp = sr_.color;
        temp.a = 0.0f;
        sr_.color = temp;
    }

    // Update is called once per frame
    void Update()
    {
        if (parent_ != null)
        {
            // move towards parent position
            Vector2 distance_between_ = (Vector2)parent_.position - (Vector2)transform.position;
            if (distance_between_.magnitude > threshold_)
            {
                transform.position += (Vector3)(distance_between_.normalized * speed_ * Time.deltaTime);
            }
            else
            {
                GameObject.Destroy(gameObject);
            }

            // update life
            if (life_span_ > 0.0f)
            {
                life_span_ -= Time.deltaTime;
            }
            else
            {
                GameObject.Destroy(gameObject);
            }

            // increment alpha
            if (sr_.color.a < 1.0f)
            {
                Color temp = sr_.color;
                temp.a += alpha_speed_ * Time.deltaTime;
                sr_.color = temp;
            }
        }
    }

    public void Init(Transform transform)
    {
        parent_ = transform;
    }
}
