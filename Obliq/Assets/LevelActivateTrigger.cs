using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActivateTrigger : MonoBehaviour
{
    public bool level_activated_;
    // Start is called before the first frame update
    void Start()
    {
        level_activated_ = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MainPlayer"))
        {
            level_activated_ = true;
        }   
    }
}
