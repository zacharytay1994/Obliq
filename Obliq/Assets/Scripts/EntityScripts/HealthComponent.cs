using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public int maxHp_;
    [SerializeField] public int currentHp_;
    [SerializeField] public float invincibility_time_;
    bool is_invincible_;
    float invincibility_start_time_;

    [SerializeField]
    float damage_flash_duration_ = 0.0f;
    SpriteRenderer sr;
    bool taken_damage_ = false;
    float counter = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        currentHp_ = maxHp_;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (taken_damage_)
        {
            Color original_color_ = sr.color;

            if (counter < damage_flash_duration_)
            {
                counter += Time.deltaTime;
                sr.material.SetColor("_EmissionColor", new Color(1, 1, 1));
            }
            else
            {
                sr.material.SetColor("_EmissionColor", original_color_);
                taken_damage_ = false;
            }
        }
    }
  
    public void TakeDamage(int damage)
    {
        if (currentHp_ > 0 && !isInvincible())
        {
            currentHp_ -= damage;
            invincibility_start_time_ = Time.time;
            taken_damage_ = true;
            counter = 0.0f;
        }
    }
    public bool isInvincible()
    {
       if(Time.time - invincibility_start_time_ > invincibility_time_)
        {
            return false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            return true;
        }
    }
    public void HealHp(int healing)
    {
        currentHp_ += healing;
    }

    public int getCurrentHp()
    {
        return currentHp_;
    }

    public int getMaxHp()
    {
        return maxHp_;
    }

    public void addMaxHp(int additionalMaxHp)
    {
        maxHp_ += additionalMaxHp;
    }

    public void subtractMaxHp(int negatedMaxHp)
    {
        if(negatedMaxHp >= maxHp_)
        {
            negatedMaxHp = maxHp_ - 2;
        }
        maxHp_ -= negatedMaxHp;
        if(currentHp_>maxHp_)
        {
            currentHp_ = maxHp_;
        }
    }
}
