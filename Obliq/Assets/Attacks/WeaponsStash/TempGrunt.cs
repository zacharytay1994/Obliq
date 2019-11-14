using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGrunt : MonoBehaviour
{
    [SerializeField]
    float in_range_ = 1.0f;
    [SerializeField]
    float speed_ = 1.0f;
    GameObject target_ = null;

    HealthComponent health_;
    Rigidbody2D rb_;
    SpawningScript spawner_;
    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        health_ = GetComponent<HealthComponent>();
        target_ = GameObject.FindGameObjectWithTag("MainPlayer");
    }

    public void AttachSpawner(SpawningScript spawner) 
    {
        spawner_ = spawner;
    }

    // Update is called once per frame
    void Update()
    {
        if (target_ != null)
        {
            Vector2 dir_vector = (Vector2)target_.transform.position - (Vector2)transform.position;
            if (dir_vector.magnitude < in_range_)
            {
                WalkToTarget(dir_vector.normalized);
            }
        }
        if (health_.getCurrentHp() <= 0)
        {
            if (spawner_ != null)
            {
                spawner_.GruntDied();
            }
            Destroy(gameObject);
        }
    }

    void WalkToTarget(Vector2 direction)
    {
        rb_.AddForce(direction * speed_, ForceMode2D.Force);
    }

    //public void TakeDamage(int damage)
    //{
    //    health_ -= damage;
    //}
}
