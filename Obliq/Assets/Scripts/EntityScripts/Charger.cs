
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

        //Physics2D.IgnoreLayerCollision(20, 14, true);
        Physics2D.IgnoreLayerCollision(20, 17, true);
        Physics2D.IgnoreLayerCollision(20, 23, true);

    }
  
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            player.GetComponent<HealthComponent>().TakeDamage(gameObject.GetComponent<DamageEnemy>().damage_);
            Physics2D.IgnoreLayerCollision(20, 16, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(20, 16, false);
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
        //if (collision.gameObject != player)
        //{
        //    Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.collider, true);
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

        MarkCheck();
    }
    public void SpawnLesserChargers()
    {
        GameObject LC1 = Instantiate(lesser_charger_reference_, new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y, -1.0f), gameObject.transform.rotation);
        GameObject LC2 = Instantiate(lesser_charger_reference_, new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, -1.0f), gameObject.transform.rotation);
        if (LC1.GetComponent<GruntSpawnAnimation>() != null && LC2.GetComponent<GruntSpawnAnimation>())
        {
            LC1.GetComponent<GruntSpawnAnimation>().SetSpawner(gameObject);
            LC1.GetComponent<GruntSpawnAnimation>().Init();
            LC2.GetComponent<GruntSpawnAnimation>().SetSpawner(gameObject);
            LC2.GetComponent<GruntSpawnAnimation>().Init();
        }
    }

    public void MarkPlayer(bool b)
    {
        if (player.GetComponent<MarkedByGrunt>() != null)
        {
            player.GetComponent<MarkedByGrunt>().SetMark(b);
        }
    }

    public void MarkCheck()
    {
        LayerMask layerMask = LayerMask.GetMask("Walls");

        RaycastHit2D isHit = Physics2D.Raycast(transform.position,
                ((Vector2)player.transform.position - (Vector2)transform.position).normalized,
             ((Vector2)transform.position -
             (Vector2)player.transform.position).magnitude, layerMask);

        if (isHit.collider == null)
        {
            player.GetComponent<MarkedByGrunt>().SetMark(true);
        }
    }
}
