using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombKnockback : MonoBehaviour
{
    [SerializeField]
    float radius_ = 5.0f;
    [SerializeField]
    float strength_ = 100.0f;
    [SerializeField]
    string player_name_ = "";
    GameObject player_to_ignore_ = null;
    // Start is called before the first frame update
    void Start()
    {
        player_to_ignore_ = GameObject.Find(player_name_);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ForcePush()
    {
        Collider2D[] collider_array = Physics2D.OverlapCircleAll(transform.position, radius_);
        foreach (Collider2D c in collider_array)
        {
            // check if collider is player
            if (c.gameObject == player_to_ignore_ || c.gameObject == gameObject)
            {
                continue;
            }
            // calculate push vector
            Vector2 push_vector = (Vector2)c.transform.position - (Vector2)transform.position;
            float length_ = push_vector.magnitude;
            float push_ratio = Mathf.Min(1 - (length_ / radius_), 0.5f);
            Vector2 push_vector_normalized = push_vector.normalized;
            if (c.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                c.gameObject.GetComponent<Rigidbody2D>().AddForce(push_vector_normalized * push_ratio * strength_, ForceMode2D.Impulse);
            }
        }
    }
}
