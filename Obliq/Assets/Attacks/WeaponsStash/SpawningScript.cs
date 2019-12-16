using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    int grunt_count_ = 0;
    [SerializeField]
    int max_grunt_count_ = 10;
    [SerializeField]
    GameObject grunt_ = null;

    [SerializeField]
    float grunt_spawn_duration_ = 0.0f;

    float grunt_spawn_timer_ = 0;
        
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnGrunt();
        if(grunt_spawn_timer_<= grunt_spawn_duration_)
        {
            grunt_spawn_timer_ += Time.timeScale;
        }   
    }

    void SpawnGrunt()
    {
        if (grunt_ != null)
        {
            if (grunt_count_ < max_grunt_count_ && grunt_spawn_timer_>=grunt_spawn_duration_)
            {
                GameObject temp = Instantiate(grunt_, new Vector3(transform.position.x, transform.position.y, -1.0f) + new Vector3(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 0.0f), Quaternion.identity);
                temp.GetComponent<TempGrunt>().AttachSpawner(this);
                grunt_count_++;
                grunt_spawn_timer_ = 0;
            }
        }
    }
    
    public void GruntDied()
    {
        grunt_count_ -= 1;
        grunt_spawn_timer_ = 0;
    }
}
