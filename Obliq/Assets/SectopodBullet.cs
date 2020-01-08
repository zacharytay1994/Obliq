using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectopodBullet : MonoBehaviour
{
    [SerializeField]
    public GameObject explosion_;
    [SerializeField]
    GameObject ring_;
    [SerializeField]
    int amount_ = 5;
    AudioManager am_;
    // Start is called before the first frame update
    void Start()
    {
        // spins bullet
        GetComponent<Rigidbody2D>().angularVelocity = 1000.0f;
        am_ = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (true)
        {
            //SpawnExplosion(collision.gameObject.transform.position);
            if (explosion_ != null)
            {
                am_.PlaySound("Explosion");
                for (int i = 0; i < amount_; i++)
                {
                    GameObject temp = Instantiate(explosion_);
                    temp.transform.position = transform.position;
                }
            }
            if (ring_ != null)
            {
                GameObject temp = Instantiate(ring_);
                temp.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void SpawnExplosion(Vector2 explosion_pos)
    {
        Instantiate(explosion_, explosion_pos, Quaternion.identity);
    }
}
