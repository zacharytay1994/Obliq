using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    public bool boss_dead_;
    // Start is called before the first frame update
    void Start()
    {
        boss_dead_ = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss_dead_)
        {
            Destroy(gameObject);
        }
    }
}
