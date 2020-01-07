using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectopodBullet : MonoBehaviour
{
    [SerializeField]
    public GameObject explosion_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnExplosion(collision.gameObject.transform.position);
        Destroy(gameObject);
    }
    void SpawnExplosion(Vector2 explosion_pos)
    {
        Instantiate(explosion_, explosion_pos, Quaternion.identity);
    }
}
