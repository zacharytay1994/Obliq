﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Camera camera_;
    [SerializeField] float player_acceleration_;
    [SerializeField] float player_decceleration_;
    [SerializeField] float player_rotation_acceleration_;
    Rigidbody2D rb2d_;
    Vector2 heading_ = new Vector2(0.0f, 0.0f);

    [SerializeField]
    GameObject ball_follow_ = null;
    [SerializeField]
    float ball_offset_ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d_ = GetComponent<Rigidbody2D>();
        camera_ = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (ball_follow_ != null)
        {
            GameObject temp = (GameObject)Instantiate(ball_follow_, transform.position, Quaternion.identity);
            temp.GetComponent<BallFollowPlayer>().InitBall(ball_offset_, gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        PlayerFacingDirection();
        PlayerMovement();
    }

    void PlayerFacingDirection()
    {
        Vector2 mouseLocation = Input.mousePosition;
        mouseLocation = camera_.ScreenToWorldPoint(mouseLocation);
        float angle = AngleBetween(transform.position, mouseLocation);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    float AngleBetween(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y-b.y, a.x-b.x )*Mathf.Rad2Deg+90;
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            heading_.y += player_acceleration_;
        }
        if (Input.GetKey(KeyCode.S))
        {
            heading_.y -= player_acceleration_;
        }
        if (Input.GetKey(KeyCode.A))
        {
            heading_.x -= player_acceleration_;
        }
        if (Input.GetKey(KeyCode.D))
        {
            heading_.x += player_acceleration_;
        }

        heading_.x = heading_.x - ((1 - player_decceleration_) * heading_.x);
        heading_.y = heading_.y - ((1 - player_decceleration_) * heading_.y);

        rb2d_.velocity = heading_;
    }
}