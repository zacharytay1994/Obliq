using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GF;

public class Charger : MonoBehaviour
{
    // enemy variables
    bool hasCollided = false;
    public float move_distance_ = 10.0f;
    public GameObject target_reference_;
    public GameObject lesser_charger_reference_;
    int spawn_buffer = 3;
    // entity reference
    Entity entity_reference_;
    HealthComponent health_;

    // Start is called before the first frame update
    void Start()
    {
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new ChargerIdleState());
        health_ = gameObject.GetComponent<HealthComponent>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.GetComponent<ImAProjectile>() != null){
        //    Debug.Log("Enemy hit");
        //    GC<Entity>(gameObject).TakeDamage(20); //temp magic no
        //}
        
        //if (collision.gameObject.GetComponent<LesserCharger>() == null)
        //{
        //    SpawnLesserChargers();
        //    entity_reference_.statemachine_.SetState(new ChargerIdleState());

        //}


    }
    void OnCollisionStay2D(Collision2D collision)
    {
      
    }
  

    // Update is called once per frame
    void Update()
    {
        if (health_.getCurrentHp() <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void SpawnLesserChargers()
    {
            GameObject LC1 = Instantiate(lesser_charger_reference_, new Vector2(gameObject.transform.position.x + spawn_buffer, gameObject.transform.position.y),
               gameObject.transform.rotation);
        GameObject LC2 = Instantiate(lesser_charger_reference_, new Vector2(gameObject.transform.position.x - spawn_buffer, gameObject.transform.position.y),
        gameObject.transform.rotation);

        //GC<Entity>(gameObject).world_handler_reference_.enemies_.Add(LC1);
        //GC<Entity>(gameObject).world_handler_reference_.enemies_.Add(LC2);
    }
}
