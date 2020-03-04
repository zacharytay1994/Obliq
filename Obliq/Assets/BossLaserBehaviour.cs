using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserBehaviour : MonoBehaviour
{
    GameObject player_;
    [SerializeField] int laser_damage_;
    [SerializeField]
    float rotation_speed_;
    float z_rotate_;
    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        z_rotate_ += Time.deltaTime * (rotation_speed_);
        float angle = GF.AngleBetween(transform.position, player_.transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + z_rotate_));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == GameObject.Find("Player"))
        {         
            collision.gameObject.GetComponent<HealthComponent>().TakeDamage(laser_damage_);
            Destroy(gameObject);
        }
        else if(collision.gameObject != gameObject)
        {
            Destroy(gameObject);
        }
    }
        
    
}
