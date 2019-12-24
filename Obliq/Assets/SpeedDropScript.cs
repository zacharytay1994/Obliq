using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDropScript : MonoBehaviour
{
    GameObject player;
    [SerializeField] int collision_radius_;
    [SerializeField] int speed_amt_;
    bool is_effecting_ = false;
    [SerializeField] float effect_duration_;
    [SerializeField] float effect_delay_time_;
    float effect_begin_time_;

    float initial_value_;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        initial_value_ = player.GetComponent<PlayerController>().speed_modifier_;
    }

    // Update is called once per frame
    void Update()
    {
        GainSpeed();
        CheckRemoveSpeed();
    }
    void GainSpeed()
    {
        if (player != null)
        {
            if ((player.transform.position - gameObject.transform.position).magnitude < collision_radius_)
            {
                if(is_effecting_ == false)
                {
                    effect_begin_time_ = Time.time;
                    is_effecting_ = true;
                }
                if (Time.time - effect_begin_time_ >= effect_delay_time_)
                {
                    player.GetComponent<PlayerController>().speed_modifier_ = speed_amt_;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
             
            }
        }
    }
    void CheckRemoveSpeed()
    {
        if (Time.time - (effect_begin_time_ + effect_delay_time_) >= effect_duration_ && is_effecting_ == true)
        {
            player.GetComponent<PlayerController>().speed_modifier_ = initial_value_;
            is_effecting_ = false;
            Destroy(gameObject);
        }
    }
}
