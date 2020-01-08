﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GF;

public class Sectopod : MonoBehaviour
{
    public GameObject target_reference_;
    // entity reference
    Entity entity_reference_;
    [SerializeField]
    GameObject sectopod_bullet_;
    [SerializeField]
    float bullet_speed_;
    GameObject player_ = null;
    
    // Start is called before the first frame update
    void Start()
    {
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new SectopodIdleState());
        //GetComponent<ImAProjectile>().InitProj();
        player_ = GameObject.Find("Player");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.GetComponent<ImAProjectile>() != null)
        {
            Debug.Log("Enemy hit");
            GC<Entity>(gameObject).TakeDamage(20); //temp magic no
            
        }*/
    }
        // Update is called once per frame
    void Update()
    {
        float angle = GF.AngleBetween(transform.position, player_.transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, angle), Mathf.PingPong(Time.time,
            6 * Time.deltaTime));
    }
    public void FireBullet()
    {
        GameObject bullet = Instantiate(sectopod_bullet_, (Vector2)gameObject.transform.position + ((gameObject.GetComponent<CircleCollider2D>().radius + 2.0f) *
             ((Vector2)target_reference_.transform.position - (Vector2)gameObject.transform.position).normalized), Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = ((Vector2)target_reference_.transform.position - (Vector2)gameObject.transform.position).normalized * bullet_speed_;
    }
}
