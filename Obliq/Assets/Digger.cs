using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{
    Entity entity_reference_;
    HealthComponent health_;
    public GameObject player;
   
    [SerializeField]
    GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new DiggerIdleState());
        health_ = gameObject.GetComponent<HealthComponent>();
        player = GameObject.Find("Player");
        Physics.IgnoreLayerCollision(15, 17);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FireBullets()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject weapon_bullet = Instantiate(bullet, gameObject.transform.GetChild(0).gameObject.transform.position, Quaternion.identity);
            weapon_bullet.GetComponent<ImAProjectile>().InitProj();
        }

    }
}
