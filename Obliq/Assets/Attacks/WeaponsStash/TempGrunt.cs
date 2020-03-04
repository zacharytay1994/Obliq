using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGrunt : MonoBehaviour
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
    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        health_ = GetComponent<HealthComponent>();
        target_ = GameObject.FindGameObjectWithTag("MainPlayer");
        point_manager_ = FindObjectOfType<PointManager>();
        transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
        pf_ = GetComponent<ZacsFindPath>();
    }

    public void AttachSpawner(SpawningScript spawner) 
    {
        spawner_ = spawner;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 1.0f && !grown_)
        {
            growth_ += grow_rate_ * Time.deltaTime;
            transform.localScale = new Vector3(growth_, growth_, 1.0f);
        }
        else
        {
            grown_ = true;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
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
        // if less than certain hp but not rallying state
        else if (health_.currentHp_ <= 1 && state_ != 1)
        {
            // get all rallying grunts
            GameObject[] ralliers = GameObject.FindGameObjectsWithTag("Rallier");
            if (ralliers.Length > 0)
            {
                float closest = float.MaxValue;
                float val = float.MaxValue;
                // find closest rallying grunt
                foreach(GameObject g in ralliers)
                {
                    val = ((Vector2)g.transform.position - (Vector2)gameObject.transform.position).sqrMagnitude;
                    if (val < closest)
                    {
                        closest = val;
                        rallier = g;
                    }
                }
            }
            // set state to rallying
            state_ = 1;
            pf_.path_update__delay_counter_ = 0.0f;
        }
        // if in rallying state send in transform information of rallier to pathfinding
        if (rallier != null && state_ == 1)
        {
            pf_.rallying_position_ = rallier.transform.position;
            pf_.state_ = state_;
        }
        // if healthy and in rallying state, switch back to attacking state
        if (health_.currentHp_ == health_.maxHp_ && state_ == 1)
        {
            state_ = 0;
            pf_.state_ = state_;
            pf_.path_update__delay_counter_ = 0.0f;
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
