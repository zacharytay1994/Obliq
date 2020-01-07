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

    // Start is called before the first frame update
    void Awake()
    {
        currentHp_ = maxHp_;
        
    }

    private void Update()
    {
    }
  
    public void TakeDamage(int damage)
    {
        if (currentHp_ > 0 && !isInvincible())
        {
            currentHp_ -= damage;
            invincibility_start_time_ = Time.time;
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
