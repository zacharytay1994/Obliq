using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collider other)
    {
        if(other.gameObject == GameObject.Find("Player"))
        {
            other.GetComponent<HealthComponent>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
