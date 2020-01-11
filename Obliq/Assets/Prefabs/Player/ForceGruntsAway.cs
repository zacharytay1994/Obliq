using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGruntsAway : MonoBehaviour
{
    [SerializeField]
    // push all enemies within radius
    float radius_ = 1.0f;
    [SerializeField]
    // push strength
    float force_ = 1.0f;
    [SerializeField]
    GameObject fart_visual_ = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushAllEnemies()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll((Vector2)transform.position, radius_);
        foreach (Collider2D c in col)
        {
            if (c.gameObject.layer == 14)
            {
                if (c.GetComponent<Rigidbody2D>() != null)
                {
                    // calculate force 
                    Vector2 distance_vec = (Vector2)c.transform.position - (Vector2)transform.position;
                    float distance = distance_vec.magnitude;
                    // calculate ratio force to apply
                    float ratio = radius_ - distance / radius_;
                    c.GetComponent<Rigidbody2D>().AddForce(distance_vec.normalized * ratio * force_);
                }
                // if has health component dmg it
                if (c.gameObject.GetComponent<HealthComponent>() != null)
                {
                    c.gameObject.GetComponent<HealthComponent>().TakeDamage(1);
                }
            }
        }
        if (fart_visual_ != null)
        {
            GameObject temp = Instantiate(fart_visual_);
            temp.transform.position = transform.position;
        }
    }
}
