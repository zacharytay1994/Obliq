using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHp_;
    [SerializeField] private int currentHp_;

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
        if (currentHp_ > 0)
        {
            currentHp_ -= damage;
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
