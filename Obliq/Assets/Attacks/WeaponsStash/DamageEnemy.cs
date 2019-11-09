﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField]
    int damage_ = 0;
    bool damaged_ = false;
    [SerializeField]
    LayerMask enemy_mask_ = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!damaged_)
        {
            if (enemy_mask_ == (enemy_mask_ | (1 << collision.gameObject.layer)))
            {
                collision.gameObject.GetComponent<TempGrunt>().TakeDamage(damage_);
                // get direction
                Vector2 direction = ((Vector2)collision.gameObject.transform.position - (Vector2)transform.position).normalized;
                collision.gameObject.GetComponent<BloodEffect>().DrawBlood(direction);
                damaged_ = true;
            }
        }
    }
}
