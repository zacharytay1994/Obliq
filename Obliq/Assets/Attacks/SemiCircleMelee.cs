using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiCircleMelee
{
    GameObject owner_;
    Collider2D[] collider_array_ = new Collider2D[50];
    ContactFilter2D filter_;

    public SemiCircleMelee(GameObject owner)
    {
        filter_.NoFilter();
        owner_ = owner;
    }
    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RepelCollider();
        }
    }

    void RepelCollider()
    {
        // get all colliders that overlap owner's collider
        owner_.GetComponent<CircleCollider2D>().OverlapCollider(filter_, collider_array_);
        foreach (Collider2D c in collider_array_)
        {
            if (c != null)
            {
                if (c.gameObject != owner_)
                {
                    // calculate vector
                    Vector2 force_vector = (c.gameObject.transform.position - owner_.transform.position).normalized;
                    // if in front of player
                    //if (Vector2.Dot(owner_.GetComponent<TempMovement>().heading_, (c.transform.position - owner_.transform.position)) > 0)
                    //{
                    //    c.gameObject.GetComponent<Rigidbody2D>().AddForce(force_vector * 30.0f, ForceMode2D.Impulse);
                    //}
                }
            }
        }
    }
}
