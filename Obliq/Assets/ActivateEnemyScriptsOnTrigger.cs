using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemyScriptsOnTrigger : MonoBehaviour
{
    public bool enemy_script_enabled_ = false;
    // Start is called before the first frame update
    void Start()
    {
        enemy_script_enabled_ = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MainPlayer"))
        {
            enemy_script_enabled_ = true;
        }
    }
}
