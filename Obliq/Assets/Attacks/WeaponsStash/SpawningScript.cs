using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    LevelActivateTrigger lat_;
    [SerializeField]
    int grunt_spawn_limit_=10;
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
    [SerializeField]
    Vector2 random_offset_ = new Vector2(-1.0f, 1.0f);
    Animator anim_;
    // Start is called before the first frame update
    void Start()
    {
        spawn_interval_counter_ = spawn_interval_;
        anim_ = GetComponent<Animator>();
        lat_ = FindObjectOfType<LevelActivateTrigger>();
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
        if(gameObject.GetComponent<HealthComponent>() != null)
        {
            if (gameObject.GetComponent<HealthComponent>().getCurrentHp() <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void SpawnGrunt()
    {
        if (grunt_ != null)
        {
            if (grunt_count_ < max_grunt_count_ && waves_ > 0)
            {
                GameObject temp = Instantiate(grunt_, new Vector3(transform.position.x + Random.Range(random_offset_.x, random_offset_.y), transform.position.y + Random.Range(random_offset_.x, random_offset_.y), -1.0f) + new Vector3(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 0.0f), Quaternion.identity);
                temp.transform.SetParent(GameObject.Find("Enemy").transform);
                if (temp.GetComponent<TempGrunt>() != null)
                {
                    temp.GetComponent<TempGrunt>().AttachSpawner(this);
                }
                if (temp.GetComponent<GruntSpawnAnimation>() != null)
                {
                    temp.GetComponent<GruntSpawnAnimation>().SetSpawner(gameObject);
                    temp.GetComponent<GruntSpawnAnimation>().Init();
                }
                
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
        if (spawn_interval_counter_ > spawn_interval_ && lat_.level_activated_)
        {
            spawn_interval_counter_ = 0;
            if (grunt_ != null && waves_ > 0)
            {
                anim_.SetTrigger("IsSpawning");
                for (int i = 0; i < max_grunt_count_; i++)
                {
                    GameObject temp = Instantiate(grunt_, new Vector3(transform.position.x + Random.Range(random_offset_.x, random_offset_.y), transform.position.y + Random.Range(random_offset_.x, random_offset_.y), -1.0f) + new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.0f), Quaternion.identity);
                    if (temp.GetComponent<TempGrunt>() != null)
                    {
                        temp.GetComponent<TempGrunt>().AttachSpawner(this);
                    }
                    if (temp.GetComponent<GruntSpawnAnimation>() != null)
                    {
                        temp.GetComponent<GruntSpawnAnimation>().SetSpawner(gameObject);
                        temp.GetComponent<GruntSpawnAnimation>().Init();
                    }
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
        spawn_interval_counter_ = 0;
    }
}
