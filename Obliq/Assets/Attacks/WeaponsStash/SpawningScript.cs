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
    int waves_ = 1;
    int added_grunt_count_ = 0;
    [SerializeField]
    float spawn_interval_ = 5.0f;
    float spawn_interval_counter_ = 0.0f;
    [SerializeField]
    bool per_wave_ = true;
    // Start is called before the first frame update
    void Start()
    {
        spawn_interval_counter_ = spawn_interval_;
    }

    // Update is called once per frame
    void Update()
    {
        if (per_wave_)
        {
            SpawnWave();
        }
        else
        {
            SpawnGrunt();
        }
    }

    void SpawnGrunt()
    {
        if (grunt_ != null)
        {
            if (grunt_count_ < max_grunt_count_ && waves_ > 0)
            {
                GameObject temp = Instantiate(grunt_, new Vector3(transform.position.x, transform.position.y, -1.0f) + new Vector3(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 0.0f), Quaternion.identity);
                temp.GetComponent<TempGrunt>().AttachSpawner(this);
                grunt_count_++;
                added_grunt_count_++;
            }
            if (added_grunt_count_ >= max_grunt_count_)
            {
                waves_--;
                added_grunt_count_ = 0;
            }
        }
    }

    void SpawnWave()
    {
        if (spawn_interval_counter_ > spawn_interval_)
        {
            spawn_interval_counter_ = 0;
            if (grunt_ != null && waves_ > 0)
            {
                for (int i = 0; i < max_grunt_count_; i++)
                {
                    GameObject temp = Instantiate(grunt_, new Vector3(transform.position.x, transform.position.y, -1.0f) + new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.0f), Quaternion.identity);
                    temp.GetComponent<TempGrunt>().AttachSpawner(this);
                }
            }
            waves_--;
        }
        else
        {
            spawn_interval_counter_ += Time.deltaTime;
            return;
        }
    }
    
    public void GruntDied()
    {
        grunt_count_ -= 1;
    }
}
