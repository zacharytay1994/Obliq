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

    public void TakeDamage(int damage)
    {
        currentHp_ -= damage;
    }

    public void HealHp(int healing)
    {
        currentHp_ += healing;
    }
}
