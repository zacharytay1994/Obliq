using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissileBehaviour : MonoBehaviour
{
    GameObject player_;
    [SerializeField]
    float bullet_speed_;
    [SerializeField]
    int damage_;
    [SerializeField]
    float rotation_speed_;
    [SerializeField]
    GameObject explosion_effect_;
    float z_rotate_ = 0;

    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        z_rotate_ += Time.deltaTime * (rotation_speed_);
        float angle = GF.AngleBetween(transform.position, player_.transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + z_rotate_));
        MissileMovement();
    }
    public void MissileMovement()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = (player_.transform.position - gameObject.transform.position).normalized * bullet_speed_;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player_)
        {
            Instantiate(explosion_effect_, transform.position, Quaternion.identity);
            player_.GetComponent<HealthComponent>().TakeDamage(damage_);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 19)
        {
            Instantiate(explosion_effect_, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
