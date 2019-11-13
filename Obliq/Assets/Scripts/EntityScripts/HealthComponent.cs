using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public int maxHp_;
    [SerializeField] public int currentHp_;

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
}
