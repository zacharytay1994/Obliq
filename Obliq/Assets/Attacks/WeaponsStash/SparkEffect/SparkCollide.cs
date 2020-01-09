using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkCollide : MonoBehaviour
{
    [SerializeField]
    GameObject spark_ = null;
    [SerializeField]
    int intensity_ = 0;
    [SerializeField]
    Vector2 splatter_upper_lower = new Vector2(0.0f, 0.0f);
    [SerializeField]
    float angle_range_ = 0.0f;
    Vector2 non_zero_vel_ = new Vector2(0.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        angle_range_ = angle_range_ * (3.142f / 180.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 test_vel = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (test_vel.x > 0 || test_vel.y > 0)
        {
            non_zero_vel_ = test_vel;
        }
    }

    public void Spark(Vector2 direction2)
    {
        Vector2 direction = -non_zero_vel_;
        for (int i = 0; i < intensity_; i++)
        {
            // get random angle
            Vector2 new_vector = Random.Range(0, 2) == 0 ? GF.RotateVector(direction, Random.Range(0.0f, angle_range_)).normalized :
                GF.RotateVector(direction, Random.Range(0.0f, -angle_range_)).normalized;
            if (spark_ != null)
            {
                GameObject temp = Instantiate(spark_, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
                temp.GetComponent<Rigidbody2D>().AddForce(new_vector * Random.Range(splatter_upper_lower.x, splatter_upper_lower.y), ForceMode2D.Impulse);
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Spark(-non_zero_vel_);
    //}
    //private void OnDestroy()
    //{

    //}
}
