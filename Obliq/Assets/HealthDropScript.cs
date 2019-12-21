using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDropScript : MonoBehaviour
{
    GameObject player; 
    [SerializeField] int collision_radius_;
    [SerializeField] int heal_amt_;
    bool is_healing_ = false;
    [SerializeField] float effect_delay_time;
    float effect_begin_time;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GainHealth();
    }
    
    public void GainHealth()
    {
        int final_heal_amt = heal_amt_;
        
        if(player != null)
        {
            if ((player.transform.position - gameObject.transform.position).magnitude < collision_radius_)
            {
                if (player.GetComponent<HealthComponent>().getCurrentHp() < player.GetComponent<HealthComponent>().getMaxHp())
                {
                    if (player.GetComponent<HealthComponent>().getCurrentHp() + heal_amt_ > player.GetComponent<HealthComponent>().getMaxHp())
                    {
                        final_heal_amt = (player.GetComponent<HealthComponent>().getCurrentHp() + heal_amt_) - player.GetComponent<HealthComponent>().getMaxHp();
                    }
                    if(is_healing_ == false)
                    {
                        effect_begin_time = Time.time;
                        is_healing_ = true;
                    }
                                     
                    if(Time.time - effect_begin_time >= effect_delay_time)
                    {
                        player.GetComponent<HealthComponent>().HealHp(final_heal_amt);                      
                        Destroy(gameObject);
                    }                                             
                }
            }
           
        }

    }
}
