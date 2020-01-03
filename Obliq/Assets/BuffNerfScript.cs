using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffNerfScript : MonoBehaviour
{
    GameObject player;
    [SerializeField] int collision_radius_;
    [SerializeField] int effect_amt_;
    bool is_effecting_ = false;
    [SerializeField] float effect_duration_;
    [SerializeField] float effect_delay_time_;
    float effect_begin_time_;

    float initial_value_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }   
    void GainSpeed()
    {
        if (player != null)
        {
            if ((player.transform.position - gameObject.transform.position).magnitude < collision_radius_)
            {
                if (is_effecting_ == false)
                {
                    effect_begin_time_ = Time.time;
                    is_effecting_ = true;
                }
                if (Time.time - effect_begin_time_ >= effect_delay_time_)
                {
                    player.GetComponent<PlayerController>().speed_modifier_ = effect_amt_;
                    Destroy(gameObject);
                }
            }
        }
    }
}
