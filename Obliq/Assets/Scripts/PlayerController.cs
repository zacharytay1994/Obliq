using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Camera camera_;
    [SerializeField] float player_acceleration_;
    Rigidbody2D rb2d_;
    Vector2 heading_ = new Vector2(0.0f, 0.0f);
    [SerializeField] float player_max_speed_;
    // Start is called before the first frame update
    void Start()
    {
        rb2d_ = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        PlayerFacingDirection();


        if (Input.GetKeyDown(KeyCode.W))
        {
            heading_.y += 1;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            heading_.y -= 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            heading_.y -= 1;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            heading_.y += 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            heading_.x -= 1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            heading_.x += 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            heading_.x += 1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            heading_.x -= 1;
        }
        rb2d_.AddForce(heading_ * player_acceleration_, ForceMode2D.Force);
        if(rb2d_.velocity.magnitude>player_max_speed_)
        {
            rb2d_.velocity = rb2d_.velocity.normalized * player_max_speed_;
        }


    }

    float PlayerFacingDirection()
    {
        Vector2 mouseLocation = Input.mousePosition;
        mouseLocation = camera_.ScreenToWorldPoint(mouseLocation);
        float angle = AngleBetween(transform.position, mouseLocation);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        return angle;
    }

    float AngleBetween(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y-b.y, a.x-b.x )*Mathf.Rad2Deg + 90;
    }
}
