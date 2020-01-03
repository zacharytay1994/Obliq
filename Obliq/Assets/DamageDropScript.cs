using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDropScript : MonoBehaviour
{
    GameObject player;
    [SerializeField] int collision_radius_;
    [SerializeField] int dmg_amt_;
    bool is_effecting_ = false;
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
        
    }
    void GainDamage()
    {
        if(player != null)
        {
           if(player.GetComponent<WeaponScript>().GetWeapon() != null)
            {

            }
        }
    }
}
