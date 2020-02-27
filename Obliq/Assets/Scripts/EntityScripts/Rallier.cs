using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rallier : MonoBehaviour
{
    [SerializeField]
    float in_range_ = 1.0f;
    [SerializeField]
    float speed_ = 1.0f;
    GameObject target_ = null;

    HealthComponent health_;
    Rigidbody2D rb_;
    SpawningScript spawner_;
    PointManager point_manager_;

    [SerializeField]
    float grow_rate_ = 50.0f;
    bool grown_ = false;
    float growth_ = 0.1f;
    public Vector2 heading_vector_ = new Vector2(0.0f, 0.0f);

    ZacsFindPath pf_;
    int state_ = 0; // 0 for player, 1 for rallier
    GameObject rallier = null;
    // Start is called before the first frame update
    [SerializeField]
    GameObject heal_effect_ = null;

    [SerializeField]
    float heal_interval_ = 5.0f;
    float heal_interval_counter_ = 0.0f;
    [SerializeField]
    int heal_amount = 1;
    [SerializeField]
    float heal_radius_ = 2.0f;
    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        health_ = GetComponent<HealthComponent>();
        target_ = GameObject.FindGameObjectWithTag("MainPlayer");
        point_manager_ = FindObjectOfType<PointManager>();
        pf_ = GetComponent<ZacsFindPath>();
        heal_radius_ = heal_radius_ * heal_radius_;
    }

    public void AttachSpawner(SpawningScript spawner)
    {
        spawner_ = spawner;
    }

    // Update is called once per frame
    void Update()
    {
        //if (target_ != null)
        //{
        //    Vector2 dir_vector = (Vector2)target_.transform.position - (Vector2)transform.position;
        //    if (dir_vector.magnitude < in_range_)
        //    {
        //        heading_vector_ = dir_vector.normalized;
        //        WalkToTarget(heading_vector_);
        //    }
        //}
        if (health_.currentHp_ <= 0)
        {
            if (spawner_ != null)
            {
                spawner_.GruntDied();
            }
            // point_manager_.AddKillPoints(100, 1);
            Destroy(gameObject);
        }
        if (heal_interval_counter_ < heal_interval_)
        {
            heal_interval_counter_ += Time.deltaTime;
        }
        else
        {
            if (heal_effect_ != null)
            {
                Instantiate(heal_effect_, transform.position, Quaternion.identity);
                // get all grunts in a circle around
                GameObject[] grunts = GameObject.FindGameObjectsWithTag("Grunt");
                foreach(GameObject g in grunts)
                {
                    if (((Vector2)g.transform.position - (Vector2)gameObject.transform.position).sqrMagnitude > heal_radius_)
                    {
                        continue;
                    }
                    HealthComponent health = g.GetComponent<HealthComponent>();
                    if (health.currentHp_ < health.maxHp_)
                    {
                        health.currentHp_ += heal_amount;
                    }
                }
            }
            heal_interval_counter_ = 0.0f;
        }
    }

    void WalkToTarget(Vector2 direction)
    {
        //Vector2 force_to_add = /*direction * speed_ +*/ gameObject.GetComponent<ObliqFlock>().GetFlockingForce();
        rb_.AddForce(direction * speed_, ForceMode2D.Force);
        //Debug.Log(gameObject.GetComponent<ObliqFlock>().GetFlockingForce());
        //rb_.AddForce(gameObject.GetComponent<ObliqFlock>().GetFlockingForce(), ForceMode2D.Force);
    }

    //public void TakeDamage(int damage)
    //{
    //    health_ -= damage;
    //}
}
