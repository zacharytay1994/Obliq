using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField]
    int damage_ = 0;
    [SerializeField]
    LayerMask enemy_mask_ = 0;
    [SerializeField]
    float damage_interval_;
    float damage_interval_timer_;
    // Start is called before the first frame update
    void Start()
    {
        damage_interval_timer_ = damage_interval_;
    }

    // Update is called once per frame
    void Update()
    {
        damage_interval_timer_ -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (damage_interval_timer_<=0)
        {
            if (enemy_mask_ == (enemy_mask_ | (1 << collision.gameObject.layer)))
            {
                if (collision.gameObject.GetComponent<HealthComponent>() != null)
                {
                    collision.gameObject.GetComponent<HealthComponent>().TakeDamage(damage_);
                    // get direction
                    Vector2 direction = ((Vector2)collision.gameObject.transform.position - (Vector2)transform.position).normalized;
                    collision.gameObject.GetComponent<BloodEffect>().DrawBlood(direction);
                    damage_interval_timer_ = damage_interval_;
                }
            }
        }
    }
}
