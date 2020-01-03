using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
    LightningParticleScript : MonoBehaviour
{
    [SerializeField]
    float life_span = 1.0f;
    float random_rotate_speed_ = 0.0f;
    int random_rotate_direction_ = 0;
    float random_scale_x_ = 0.0f;
    float random_scale_y_ = 0.0f;
    float random_color_ = 0.0f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        random_rotate_speed_ = Random.Range(45, 90);
        random_rotate_direction_ = Random.Range(0, 2);
        random_scale_x_ = Random.Range(0.01f, 0.1f);
        random_scale_y_ = Random.Range(0.01f, 0.1f);
        random_color_ = Random.Range(0.0f, 0.5f);
        random_rotate_direction_ = random_rotate_direction_ == 0 ? -1 : 1;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, random_color_, 0.0f);
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.angularVelocity = random_rotate_direction_ * random_rotate_speed_;
        transform.localScale = new Vector3(random_scale_x_, random_scale_y_, 1.0f);
        Destroy(gameObject, life_span);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void RandomlyRotate()
    {

    }
}
