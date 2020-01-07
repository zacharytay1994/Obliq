using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticle : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float growth_counter_ = 0;
    [SerializeField] GameObject ring_prefab_;
    [SerializeField] float growth_rate_ = 2;
    [SerializeField] float max_growth_;
    [SerializeField] float alpha_rate_;
    [SerializeField] int number_of_rings_ = 0;
    [SerializeField] float interval_between_rings_ = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color temp = spriteRenderer.color;
        temp.a = 1.0f;
        spriteRenderer.color = temp;
    }

    // Update is called once per frame
    void Update()
    {
        Color temp = spriteRenderer.color;
        if (temp.a >= 0)
        {
            temp.a -= alpha_rate_ * Time.deltaTime;
            spriteRenderer.color = temp;
        }
        transform.localScale = new Vector3(transform.localScale.x + growth_rate_ * Time.deltaTime, transform.localScale.y + growth_rate_ * Time.deltaTime, transform.localScale.z);

        growth_counter_ += growth_rate_ * Time.deltaTime;

        if (growth_counter_ >= max_growth_)
        {
            GameObject.Destroy(gameObject);
        }

        if (interval_between_rings_ >= 0)
        {
            interval_between_rings_ -= Time.deltaTime;
        }
        else
        {
            if (number_of_rings_ > 0)
            {
                number_of_rings_--;
                GameObject next_ring_ = Instantiate(ring_prefab_);
                next_ring_.GetComponent<DashParticle>().init(number_of_rings_);
            }
        }
    }

    public void init(int value)
    {
        number_of_rings_ = value;
    }
}
