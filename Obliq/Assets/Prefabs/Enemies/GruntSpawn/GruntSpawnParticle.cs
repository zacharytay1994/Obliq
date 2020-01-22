using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntSpawnParticle : MonoBehaviour
{
    Vector2 converge_position_ = Vector2.zero;
    float converge_time_ = 2.0f;
    Rigidbody2D rb_ = null;
    [SerializeField]
    float converge_speed_ = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (converge_time_ > 0.0f)
        {
            converge_time_ -= Time.deltaTime;
        }
        else
        {
            // converge to position
            // calculate vector to converge position
            Vector2 direction = (converge_position_ - (Vector2)transform.position).normalized;
            if (rb_ != null)
            {
                rb_.velocity += direction * converge_speed_ * Time.deltaTime;
            }
            float distance_ = (converge_position_ - (Vector2)transform.position).sqrMagnitude;
            if (distance_ < 0.1f)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void SetConvergeData(Vector2 c, float ct)
    {
        converge_position_ = c;
        converge_time_ = ct;
    }
}
