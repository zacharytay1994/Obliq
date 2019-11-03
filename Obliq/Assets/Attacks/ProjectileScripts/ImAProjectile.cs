using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImAProjectile : MonoBehaviour
{
    // Projectile variables basic
    public float speed_ = 5.0f;
    Rigidbody2D rb;

    public ProjectileMovement movement_ = ProjectileMovement.Straight;
    // amplitude, applicable to longitude and lengitude
    public float oscillation_ = 0.0f;
    public float amplitude_ = 0.0f;
    float osc_counter_ = 0.0f;
    Vector2 old_force_ = new Vector2(0.0f,0.0f);

    // target to fire at
    public ProjectileTarget target_ = ProjectileTarget.MouseDirection;
    // direction to fire at, only applicable to DirectionLong and DirectionShort
    public Vector2 targetted_direction_ = new Vector2(0.0f, 0.0f);
    // mouse direction limit, onlu applicable to MouseDirectionLimit
    public float mouse_direction_limit_ = 0.0f;

    // where to spawn the projectile
    public ProjectileSpawnLocation spawn_location_ = ProjectileSpawnLocation.RelativeSelf;
    // only applicable if RelativeSelf
    public float self_offset_ = 0.0f;

    public ProjectileSpawnStyle spawn_style_ = ProjectileSpawnStyle.None;
    public OnCollideBasic collide_basic_ = OnCollideBasic.None;
    public OnCollideEffect collide_effect_ = OnCollideEffect.None;
    public OnCollideSpawn collide_spawn_ = OnCollideSpawn.None;
    public CollideResult collide_result_ = CollideResult.Continue;
    public LocalMovement local_movement_ = LocalMovement.Fixed;

    // Projectile Variables
    Vector2 center_point_;
    Vector2 movement_heading_;
    // Start is called before the first frame update
    void Start()
    {
        // set stuff
        rb = gameObject.GetComponent<Rigidbody2D>();

        // set movement heading
        switch (target_)
        {
            case ProjectileTarget.MouseFollow:
            case ProjectileTarget.MousePoint:
            case ProjectileTarget.MouseDirection:
            case ProjectileTarget.MouseDirectionLimit:
                movement_heading_ = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)gameObject.transform.position).normalized;
                break;
            case ProjectileTarget.DirectionShort:
            case ProjectileTarget.DirectionLong:
                movement_heading_ = (targetted_direction_ - (Vector2)gameObject.transform.position).normalized;
                break;
            case ProjectileTarget.NearestEnemy:
                // to be filled
                break;
        }
        // set center point
        switch (spawn_location_)
        {
            case ProjectileSpawnLocation.RelativeSelf:
                center_point_ = (Vector2)gameObject.transform.position + (movement_heading_ * self_offset_);
                break;
            case ProjectileSpawnLocation.Mouse:
                center_point_ = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                break;
        }
        // set projectile at center point
        transform.position = center_point_;
    }

    // Update is called once per frame
    void Update()
    {
        // update movement heading based on target
        switch (target_)
        {
            case ProjectileTarget.MouseFollow:
                movement_heading_ = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
                break;
        }
        // update movement
        switch (movement_)
        {
            case ProjectileMovement.Straight:
                StraightMovement();
                break;
            case ProjectileMovement.Longitude:
                LongitudeMovement();
                break;
            case ProjectileMovement.Lengitude:
                LengitudeMovement();
                break;
            case ProjectileMovement.Spiral:
                SpiralMovement();
                break;
        }
    }

    //-------------------------------------------//
    // MOVEMENT LOGIC - updated every frame
    //-------------------------------------------//
    void StraightMovement()
    {
        // Apply constant force to bullet in straight direction
        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
    }

    void LongitudeMovement()
    {
        osc_counter_ = (osc_counter_ + oscillation_) % 6.284f;
        // counteract old force
        rb.AddForce(-old_force_, ForceMode2D.Force);
        Vector2 new_force = movement_heading_ * amplitude_ * Mathf.Sin(osc_counter_);
        // apply new force
        rb.AddForce(new_force, ForceMode2D.Force);
        // update old force
        old_force_ = new_force;
        // move towards target by speed
        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
    }

    void LengitudeMovement()
    {
        osc_counter_ = (osc_counter_ + oscillation_) % 6.284f;
        // move perpendicular to center heading
        Vector2 perpendicular_vector = new Vector2(movement_heading_.y, -movement_heading_.x).normalized;
        // counteract old force
        rb.AddForce(-old_force_, ForceMode2D.Force);
        Vector2 new_force = perpendicular_vector * amplitude_ * Mathf.Cos(osc_counter_);
        // apply new force
        rb.AddForce(new_force, ForceMode2D.Force);
        // update old force
        old_force_ = new_force;
        // move towards target by speed
        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
    }

    void SpiralMovement()
    {
        osc_counter_ = (osc_counter_ + oscillation_) % 6.284f;
        // move perpendicular to center heading
        Vector2 perpendicular_vector = new Vector2(movement_heading_.y, -movement_heading_.x).normalized;
        // counteract old force
        rb.AddForce(-old_force_, ForceMode2D.Force);
        Vector2 new_force = (perpendicular_vector * amplitude_ * Mathf.Cos(osc_counter_)) + (movement_heading_ * amplitude_ * Mathf.Sin(osc_counter_));
        // apply new force
        rb.AddForce(new_force, ForceMode2D.Force);
        // update old force
        old_force_ = new_force;
        // move towards target by speed
        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
    }
}
