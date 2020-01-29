
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGudSuc : MonoBehaviour
{
    [SerializeField]
    GameObject suc_target_;
    [SerializeField]
    GameObject particle_;
    List<GameObject> particle_list_ = new List<GameObject>();
    [SerializeField]
    Vector2 suc_min_max_ = Vector2.zero;
    [SerializeField]
    float particle_speed_ = 10.0f;
    [SerializeField]
    float reach_threshold_ = 0.1f;

    bool suc = false;
    [SerializeField]
    float particle_amount_ = 10.0f;
    [SerializeField]
    float spawn_interval_ = 0.1f;
    float interval_counter_ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAllParticles();
        if (suc)
        {
            if (interval_counter_ > spawn_interval_)
            {
                for (int i = 0; i < particle_amount_; i++)
                {
                    CreateParticleAroundSucTarget();
                }
                interval_counter_ = 0.0f;
            }
            else
            {
                interval_counter_ += Time.deltaTime;
            }
        }
    }

    void CreateParticleAroundSucTarget()
    {
        if (particle_ == null || suc_target_ == null)
        {
            return;
        }

        // get random spawn position around target
        Vector2 random_vec = GF.RotateVector(new Vector2(0.0f, 1.0f), Random.Range(0.0f, 360.0f));
        Vector2 particle_position = (Vector2)suc_target_.transform.position + random_vec * Random.Range(suc_min_max_.x, suc_min_max_.y);

        // spawn particle and add to particle list
        GameObject p = GameObject.Instantiate(particle_, particle_position, Quaternion.identity);
        particle_list_.Add(p);
    }

    void UpdateAllParticles()
    {
        foreach (GameObject g in particle_list_)
        {
            // make particle travel towards this object
            // get vector between particle and object
            Vector2 direction = ((Vector2)gameObject.transform.position - (Vector2)g.gameObject.transform.position);
            // check if particle is within threshold
            if (direction.magnitude > reach_threshold_)
            {
                // move
                g.transform.position += (Vector3)(direction.normalized * particle_speed_);
            }
            else
            {
                // if reached threshold, remove from list and destroy
                GameObject temp = g;
                particle_list_.Remove(g);
                GameObject.Destroy(g);
            }
        }
    }

    public void Suc(bool b)
    {
        suc = b;
    }
}
