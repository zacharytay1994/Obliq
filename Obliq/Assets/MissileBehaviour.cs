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
    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float angle = GF.AngleBetween(transform.position, player_.transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
            player_.GetComponent<HealthComponent>().TakeDamage(damage_);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 19)
        {
            Destroy(gameObject);
        }
    }
}
