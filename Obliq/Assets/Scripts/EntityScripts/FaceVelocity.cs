using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceVelocity : MonoBehaviour
{
    Rigidbody2D rb;
    bool active_ = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //rb.angularVelocity = 0.0f;
        //rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active_)
        {
            RotateImageToVelocity();
        }
    }

    void RotateImageToVelocity()
    {
        // get angle
        float angle = AngleBetween(transform.position, (Vector2)transform.position + rb.velocity);
        gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    float AngleBetween(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg + 90;
    }

    public void SetActive(bool b)
    {
        active_ = b;
    }
}
