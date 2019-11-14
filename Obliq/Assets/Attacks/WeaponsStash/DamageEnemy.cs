using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField]
    int damage_ = 0;
    [SerializeField]
    LayerMask enemy_mask_ = 0;
    [SerializeField]
    bool causes_hit_pause_ = false;

    HitPause hit_pause_;
    // Start is called before the first frame update
    void Start()
    {
        hit_pause_ = FindObjectOfType<HitPause>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemy_mask_ == (enemy_mask_ | (1 << collision.gameObject.layer)))
        {
            if (collision.gameObject.GetComponent<HealthComponent>() != null)
            {
                if(causes_hit_pause_)
                {
                    hit_pause_.Freeze(damage_);
                }
                collision.gameObject.GetComponent<HealthComponent>().TakeDamage(damage_);
                // get direction
                Vector2 direction = ((Vector2)collision.gameObject.transform.position - (Vector2)transform.position).normalized;

                if (collision.gameObject.GetComponent<BloodEffect>() != null)
                {
                    collision.gameObject.GetComponent<BloodEffect>().DrawBlood(direction);
                }


            }
        }

    }
}
