using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCooldown : MonoBehaviour
{
    GameObject gun_;
    ImAProjectile fire_script_;
    [SerializeField] float cooldown_timer_base_;
    public float timer_ = 0;
    // Start is called before the first frame update
    void Start()
    {
        fire_script_ = gameObject.GetComponent<ImAProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fire_script_.start_spawning_)
        {
            timer_ = cooldown_timer_base_;
        }
        if(timer_>0)
        {
            timer_ -= Time.deltaTime;
        }
    }
}
