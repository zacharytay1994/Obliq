﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead : MonoBehaviour
{
    GameObject player_;

    BossBody bb_;
    bool attached_ = true;
    bool activate_ = false;

    float left_;
    float right_;
    float top_;
    float bottom_;

    bool rigid_lock_ = true;
    Rigidbody2D rb_;

    Vector2 initial_random_position_ = Vector2.zero;
    bool not_reached_ = true;
    bool inplace_ = false;
    bool inplace_charge_ = false;
    string side_ = "";
    [SerializeField]
    float move_speed_ = 5.0f;
    [SerializeField]
    float hover_speed_ = 5.0f;

    [SerializeField]
    float hover_distance_ = 1.0f;

    string attack_phase_ = "";

    // charge phase
    [SerializeField]
    float charge_force_ = 5.0f;
    bool start_charge_ = false;
    bool charged_ = false;
    [SerializeField]
    float charge_duration_ = 3.0f;
    float charge_counter_ = 0.0f;

    HealthComponent hc_;
    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.Find("Player");
        rb_ = GetComponent<Rigidbody2D>();
        hc_ = GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activate_)
        {
            WalkToInitialPosition();
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0.0f, 0.0f, GF.AngleBetween(new Vector2(0.0f, 1.0f), (Vector2)player_.transform.position - (Vector2)gameObject.transform.position)),
                Mathf.PingPong(Time.time,
                6 * Time.deltaTime));
        }
        if (inplace_)
        {
            InplaceHover();
        }
        if (inplace_charge_)
        {
            ExecuteCharge();
        }
        if (hc_.currentHp_ <= 0)
        {
            bb_.SetPhase(2);
            GameObject.Destroy(gameObject);
        }
    }
    public void Init(BossBody bb)
    {
        bb_ = bb;
    }

    public bool Attached()
    {
        return attached_;
    }

    public void Activate()
    {
        attached_ = false;
        SelectRandomPosition();
        activate_ = true;
    }

    public void SetBounds(float left, float right, float top, float bottom)
    {
        left_ = left;
        right_ = right;
        top_ = top;
        bottom_ = bottom;
    }

    public void SelectRandomPosition()
    {
        int i = Random.Range(0, 4);
        switch (i)
        {
            case 0:
                initial_random_position_ = new Vector2(left_ - hover_distance_, gameObject.transform.position.y);
                side_ = "left";
                break;
            case 1:
                initial_random_position_ = new Vector2(right_ + hover_distance_, gameObject.transform.position.y);
                side_ = "right";
                break;
            case 2:
                initial_random_position_ = new Vector2(gameObject.transform.position.x, top_ + hover_distance_);
                side_ = "top";
                break;
            case 3:
                initial_random_position_ = new Vector2(gameObject.transform.position.x, bottom_ - hover_distance_);
                side_ = "bottom";
                break;
        }
    }

    public void WalkToInitialPosition()
    {
        if (!inplace_)
        {
            // calculate direction
            Vector2 direction = initial_random_position_ - (Vector2)gameObject.transform.position;
            if (direction.magnitude > 0.5f)
            {
                gameObject.transform.position += (Vector3)(direction.normalized * move_speed_ * Time.deltaTime);
            }
            else
            {
                inplace_ = true;
                inplace_charge_ = true;
            }
        }
    }

    public void InplaceHover()
    {
        Vector2 pos = (Vector2)gameObject.transform.position;
        switch (side_) {
            case "left":
            case "right":
                if (pos.y > top_)
                {
                    gameObject.transform.position = new Vector3(pos.x, top_, gameObject.transform.position.z);
                    hover_speed_ *= -1;
                    return;
                }
                if (pos.y < bottom_)
                {
                    gameObject.transform.position = new Vector3(pos.x, bottom_, gameObject.transform.position.z);
                    hover_speed_ *= -1;
                    return;
                }
                gameObject.transform.position = new Vector3(pos.x, pos.y + hover_speed_ * Time.deltaTime, gameObject.transform.position.z);
                break;
            case "top":
            case "bottom":
                if (pos.x > right_)
                {
                    gameObject.transform.position = new Vector3(right_, pos.y, gameObject.transform.position.z);
                    hover_speed_ *= -1;
                    return;
                }
                if (pos.x < left_)
                {
                    gameObject.transform.position = new Vector3(left_, pos.y, gameObject.transform.position.z);
                    hover_speed_ *= -1;
                    return;
                }
                gameObject.transform.position = new Vector3(pos.x + hover_speed_ * Time.deltaTime, pos.y, gameObject.transform.position.z);
                break;
        }
     }

    public void Idle()
    {

    }

    public void ExecuteCharge()
    {
        if (!start_charge_)
        {
            gameObject.GetComponent<GolemSucking>().StartSucking();
            start_charge_ = true;
        }
        if (charge_counter_ < charge_duration_)
        {
            charge_counter_ += Time.deltaTime;
        }
        else if (!charged_)
        {
            charged_ = true;
            // boost to player
            inplace_ = false;
            rb_.AddForce(((Vector2)player_.transform.position - (Vector2)gameObject.transform.position).normalized * charge_force_ * Time.deltaTime, ForceMode2D.Impulse);
        }
        else
        {
            Vector3 pos = gameObject.transform.position;
            if (gameObject.transform.position.x < (left_ - hover_distance_ - 0.5f))
            {
                rb_.velocity = Vector3.zero;
                gameObject.transform.position = new Vector3(left_ - hover_distance_, pos.y, pos.z);
                side_ = "left";
                ResetCharge();
            }
            else if (gameObject.transform.position.x > (right_ + hover_distance_ + 0.5f))
            {
                rb_.velocity = Vector3.zero;
                gameObject.transform.position = new Vector3(right_ + hover_distance_, pos.y, pos.z);
                side_ = "right";
                ResetCharge();
            }
            else if (gameObject.transform.position.y > (top_ + hover_distance_ + 0.5f))
            {
                rb_.velocity = Vector3.zero;
                gameObject.transform.position = new Vector3(pos.x, top_ + hover_distance_, pos.z);
                side_ = "top";
                ResetCharge();
            }
            else if (gameObject.transform.position.y < (bottom_ - hover_distance_ - 0.5f))
            {
                rb_.velocity = Vector3.zero;
                gameObject.transform.position = new Vector3(pos.x, bottom_ - hover_distance_, pos.z);
                side_ = "bottom";
                ResetCharge();
            }
        }
    }

    void ResetCharge()
    {
        start_charge_ = false;
        charge_counter_ = 0.0f;
        charged_ = false;
        inplace_ = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player_)
        {
            player_.GetComponent<HealthComponent>().TakeDamage(gameObject.GetComponent<DamageEnemy>().damage_);
        }
    }
}