
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static GF;

public class Charger : MonoBehaviour
{
    // enemy variables
    bool hasCollided = false;
    public float move_distance_ = 10.0f;
    public GameObject target_reference_;
    public GameObject lesser_charger_reference_;
    public Vector2 heading;
    int spawn_buffer = 2;
    LayerMask layerMask;
    // entity reference
    Entity entity_reference_;
    HealthComponent health_;
    GameObject player;
    public bool find_path_ = false;
    ZacsFindPath zfp = null;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Walls");
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new ChargerIdleState());
        health_ = gameObject.GetComponent<HealthComponent>();
        player = GameObject.Find("Player");
        zfp = gameObject.GetComponent<ZacsFindPath>();
    }
  
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            player.GetComponent<HealthComponent>().TakeDamage(1);
            Physics2D.IgnoreLayerCollision(14, 16, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(14, 16, false);
        }
        //if(collision.gameObject.GetComponent<ImAProjectile>() != null){
        //    
        //    GC<Entity>(gameObject).TakeDamage(20); //temp magic no
        //}

        //if (collision.gameObject.GetComponent<LesserCharger>() == null)
        //{
        //    SpawnLesserChargers();
        //    entity_reference_.statemachine_.SetState(new ChargerIdleState());

        //}
    }    
    // Update is called once per frame
    void Update()
    {
        if (health_.getCurrentHp() <= 0)
        {
            Destroy(gameObject);
        }
        
        if (find_path_)
        {
            if (zfp != null)
            {
                zfp.SetMove(true);
            }
        }
        else
        {
            zfp.SetMove(false);
        }
    }
    public void SpawnLesserChargers()
    {
        GameObject LC1 = Instantiate(lesser_charger_reference_, new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y, -1.0f), gameObject.transform.rotation);
        GameObject LC2 = Instantiate(lesser_charger_reference_, new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, -1.0f), gameObject.transform.rotation);

    }
}
