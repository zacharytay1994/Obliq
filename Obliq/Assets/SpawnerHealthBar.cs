﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHealthBar : MonoBehaviour
{
    HealthComponent health_component_;
    [SerializeField]
    GameObject health_unit_;
    [SerializeField]
    float display_size_;
    [SerializeField]
    float display_spacing_;
    int num_healthbar_;

    // Start is called before the first frame update
    void Start()
    {
        health_component_ = gameObject.GetComponent<HealthComponent>();

        SpawnHealthbar();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDamage();
    }

    public void SpawnHealthbar()
    {
        for (int i = 0; i < health_component_.getCurrentHp(); i++)
        {
            GameObject health_unit = Instantiate(health_unit_, transform);
            health_unit.transform.SetParent(gameObject.transform);
            health_unit.GetComponent<RectTransform>().localPosition = 
                new Vector3((display_size_ + display_spacing_) * (i + 1), -(display_size_ / 2) - display_spacing_, 0);
            num_healthbar_++;
        }
    }

    void CheckForDamage()
    {
        if (health_component_.getCurrentHp() < num_healthbar_)
        {
            Destroy(transform.GetChild(num_healthbar_ - 1).gameObject);
            num_healthbar_--;
        }

        if (health_component_.getCurrentHp() <= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        if (health_component_.getCurrentHp() > num_healthbar_)
        {
            GameObject health_unit = Instantiate(health_unit_, transform);
            health_unit.transform.SetParent(gameObject.transform);
            health_unit.GetComponent<RectTransform>().localPosition = new Vector3((display_size_ + display_spacing_) * (num_healthbar_ + 1),
                -(display_size_ / 2) - display_spacing_, 0);
        }
    }
}
