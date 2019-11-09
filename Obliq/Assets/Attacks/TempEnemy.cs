﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TempEnemy : MonoBehaviour
{
    float size_ = 20.0f;
    float speed_ = 5.0f;
    int i = 0;
    bool safety_check_ = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        WalkToPlayer();
        if (safety_check_)
        {
            i++;
        }
        if (i > 300)
        {
            if (gameObject.transform.localScale.x > 1.25f && safety_check_)
            {
                SplitSelf();
            }
            else
            {
                safety_check_ = false;
            }
        }
    }

    void SplitSelf()
    {
        float radius_ = gameObject.GetComponent<SpriteRenderer>().bounds.size.x/2;
        GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Attacks/MockEnemy.prefab", typeof(GameObject));
        GameObject temp = (GameObject)Instantiate(prefab, new Vector3(transform.position.x - radius_, transform.position.y + radius_, 0.0f), Quaternion.identity);
        GameObject temp2 = (GameObject)Instantiate(prefab, new Vector3(transform.position.x + radius_, transform.position.y + radius_, 0.0f), Quaternion.identity);
        GameObject temp3 = (GameObject)Instantiate(prefab, new Vector3(transform.position.x - radius_, transform.position.y - radius_, 0.0f), Quaternion.identity);
        GameObject temp4 = (GameObject)Instantiate(prefab, new Vector3(transform.position.x + radius_, transform.position.y -  radius_, 0.0f), Quaternion.identity);
        temp.GetComponent<TempEnemy>().Init(size_ / 2.0f, speed_, gameObject.GetComponent<Rigidbody2D>().mass);
        temp2.GetComponent<TempEnemy>().Init(size_ / 2.0f, speed_, gameObject.GetComponent<Rigidbody2D>().mass);
        temp3.GetComponent<TempEnemy>().Init(size_ / 2.0f, speed_, gameObject.GetComponent<Rigidbody2D>().mass);
        temp4.GetComponent<TempEnemy>().Init(size_ / 2.0f, speed_, gameObject.GetComponent<Rigidbody2D>().mass);
        Destroy(gameObject);
    }

    void Init(float size, float speed, float mass)
    {
        gameObject.GetComponent<Rigidbody2D>().mass = mass / 2.0f;
        size_ = size;
        speed_ = speed;
        transform.localScale = new Vector3(size, size, 1.0f);
    }

    void WalkToPlayer()
    {
        Vector2 temp = (GameObject.Find("MockPlayer").transform.position - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(temp * speed_, ForceMode2D.Force);
    }
}