﻿using System.Collections;
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
    PointManager point_manager_;

    [SerializeField]
    float grow_rate_ = 50.0f;
    bool grown_ = false;
    float growth_ = 0.1f;
    public Vector2 heading_vector_ = new Vector2(0.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        health_ = GetComponent<HealthComponent>();
        target_ = GameObject.FindGameObjectWithTag("MainPlayer");
        point_manager_ = FindObjectOfType<PointManager>();
        transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
    }

    public void AttachSpawner(SpawningScript spawner) 
    {
        spawner_ = spawner;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 1.0f && !grown_)
        {
            growth_ += grow_rate_ * Time.deltaTime;
            transform.localScale = new Vector3(growth_, growth_, 1.0f);
        }
        else
        {
            grown_ = true;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        //if (target_ != null)
        //{
        //    Vector2 dir_vector = (Vector2)target_.transform.position - (Vector2)transform.position;
        //    if (dir_vector.magnitude < in_range_)
        //    {
        //        heading_vector_ = dir_vector.normalized;
        //        WalkToTarget(heading_vector_);
        //    }
        //}
        if (health_.currentHp_ <= 0)
        {
            if (spawner_ != null)
            {
                spawner_.GruntDied();
            }
           // point_manager_.AddKillPoints(100, 1);
            Destroy(gameObject);
        }
    }

    void WalkToTarget(Vector2 direction)
    {
        //Vector2 force_to_add = /*direction * speed_ +*/ gameObject.GetComponent<ObliqFlock>().GetFlockingForce();
        rb_.AddForce(direction * speed_, ForceMode2D.Force);
        //Debug.Log(gameObject.GetComponent<ObliqFlock>().GetFlockingForce());
        //rb_.AddForce(gameObject.GetComponent<ObliqFlock>().GetFlockingForce(), ForceMode2D.Force);
    }

    //public void TakeDamage(int damage)
    //{
    //    health_ -= damage;
    //}
}
