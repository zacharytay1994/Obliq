using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserBehaviour : MonoBehaviour
{
    [SerializeField] int laser_damage_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == GameObject.Find("Player"))
        {
            
            collision.gameObject.GetComponent<HealthComponent>().TakeDamage(laser_damage_);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
        
    
}
