using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSetLimit : MonoBehaviour
{
    public int total_grunt_count_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        total_grunt_count_ = 0;
        for(int i = 0;i<transform.childCount;i++)
        {
            total_grunt_count_ += transform.GetChild(i).GetComponent<SpawningScript>().grunt_count_;
        }
    }
}
