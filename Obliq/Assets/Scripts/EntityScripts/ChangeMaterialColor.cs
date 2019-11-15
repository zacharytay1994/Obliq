using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour
{
    float max_health_;
    float current_health_;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        max_health_ = gameObject.GetComponent<HealthComponent>().maxHp_;
        current_health_ = gameObject.GetComponent<HealthComponent>().currentHp_;
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetColorBasedOnHp();
    }

    void SetColorBasedOnHp()
    {
        float ratio = UpdateHpRatio();
        sr.material.SetColor("_EmissionColor", new Color(1 - ratio, ratio, 0));
    }

    float UpdateHpRatio()
    {
        current_health_ = gameObject.GetComponent<HealthComponent>().currentHp_;
        return current_health_ / max_health_;
    }
}
