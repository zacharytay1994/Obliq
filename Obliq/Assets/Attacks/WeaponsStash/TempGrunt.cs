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
    [SerializeField]
    float health_ = 10.0f;
    Rigidbody2D rb_;
    SpawningScript spawner_;
    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        target_ = GameObject.Find("Player");
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
        if (health_ <= 0)
        {
            spawner_.GruntDied();
            Destroy(gameObject);
        }
    }

    void WalkToTarget(Vector2 direction)
    {
        rb_.AddForce(direction * speed_, ForceMode2D.Force);
    }

    public void TakeDamage(int damage)
    {
        health_ -= damage;
    }
}
