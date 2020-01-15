using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntSpawnAnimation : MonoBehaviour
{
    GameObject spawner_ = null;
    [SerializeField]
    GameObject ring_effect_ = null;
    [SerializeField]
    GameObject particle_effect_ = null;

    [SerializeField]
    int number_of_particles_ = 10;
    [SerializeField]
    float particle_converge_time_ = 4.0f;
    [SerializeField]
    Vector2 random_strength_ = new Vector2(10.0f, 50.0f);
    [SerializeField]
    float spawn_time_ = 5.0f;
    [SerializeField]
    GameObject grunt_ = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init()
    {
        if (ring_effect_ != null && spawner_ != null && particle_effect_ != null)
        {
            // instantiate ring effect
            GameObject.Instantiate(ring_effect_, (Vector2)spawner_.transform.position, Quaternion.identity);
            // instantiate particle effects
            for (int i = 0; i < number_of_particles_; i++)
            {
                GameObject temp = GameObject.Instantiate(particle_effect_, (Vector2)spawner_.transform.position, Quaternion.identity);
                // set particle velocity
                if (temp.GetComponent<Rigidbody2D>() != null)
                {
                    temp.GetComponent<Rigidbody2D>().velocity = GF.RotateVector(new Vector2(0.0f, 1.0f), Random.Range(0.0f, 360.0f)) * Random.Range(random_strength_.x, random_strength_.y);
                }
                temp.GetComponent<GruntSpawnParticle>().SetConvergeData((Vector2)transform.position, particle_converge_time_);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // after spawn time, spawn grunt
        if (spawn_time_ > 0.0f)
        {
            spawn_time_ -= Time.deltaTime;
        }
        else
        {
            // spawn grunt
            GameObject.Instantiate(grunt_, transform.position, Quaternion.identity);
            // destroy self
            GameObject.Destroy(gameObject);
        }
    }

    public void SetSpawner(GameObject g)
    {
        spawner_ = g;
    }
}
