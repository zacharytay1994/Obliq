using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImAProjectile : MonoBehaviour
{
    // Projectile variables basic
    public float speed_ = 5.0f;
    public ProjectileForceType force_type_ = ProjectileForceType.Constant;
    Rigidbody2D rb;

    [Header("PROJECTILE MOVEMENT BEHAVIOUR")]
    public ProjectileMovement movement_ = ProjectileMovement.Straight;
    [Header("Wave/Spiral Movement")]
    // amplitude, applicable to longitude and lengitude
    public float lon_oscillation_ = 0.1f;
    public float lon_amplitude_ = 500.0f;
    public float len_oscillation_ = 0.1f;
    public float len_amplitude_ = 200.0f;
    float lon_osc_counter_ = 0.0f;
    float len_osc_counter_ = 0.0f;
    Vector2 old_force_ = new Vector2(0.0f,0.0f);
    // random heading, random length, applicalble to random
    [Header("Random Movement")]
    [Tooltip("speed between random points")]
    public float random_speed_ = 300.0f;
    [Tooltip("radius to find new random point")]
    public float random_range_ = 2.0f; // range of selecting random point
    [Tooltip("time in seconds before new random")]
    public float random_time_ = 0.4f; // duration before finding new random
    public float random_min_angle_ = 145.0f;
    public float random_angle_end_ = 70;
    public float turn_speed_ = 100.0f;
    Vector2 random_heading_ = new Vector2(0.0f, 0.0f);
    float random_counter_ = 0.0f;

    // target to fire at
    [Header("PROJECTILE TARGET SELECT")]
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
        // random movement stuff
        random_counter_ = random_time_;
        random_angle_end_ = Mathf.Abs(random_angle_end_);
        random_angle_end_ = random_angle_end_ % 361;
        random_angle_end_ *= (3.142f / 180.0f);
        random_min_angle_ *= (3.142f / 180.0f);
        float random_range = Random.Range(0.0f, 6.284f);
        random_heading_ = new Vector2(-Mathf.Sin(random_range), Mathf.Cos(random_range));

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
        if (force_type_ == ProjectileForceType.Constant)
        {
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
                case ProjectileMovement.RandomStraight:
                    RandomMovementStraight();
                    break;
                case ProjectileMovement.RandomSmooth:
                    RandomMovementSmooth();
                    break;
            }
        }
        else if (force_type_ == ProjectileForceType.Impulse) { }
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
        lon_osc_counter_ = (lon_osc_counter_ + lon_oscillation_) % 6.284f;
        // counteract old force
        rb.AddForce(-old_force_, ForceMode2D.Force);
        Vector2 new_force = movement_heading_ * lon_amplitude_ * Mathf.Sin(lon_osc_counter_);
        // apply new force
        rb.AddForce(new_force, ForceMode2D.Force);
        // update old force
        old_force_ = new_force;
        // move towards target by speed
        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
    }

    void LengitudeMovement()
    {
        len_osc_counter_ = (len_osc_counter_ + len_oscillation_) % 6.284f;
        // move perpendicular to center heading
        Vector2 perpendicular_vector = new Vector2(movement_heading_.y, -movement_heading_.x).normalized;
        // counteract old force
        rb.AddForce(-old_force_, ForceMode2D.Force);
        Vector2 new_force = perpendicular_vector * len_amplitude_ * Mathf.Cos(len_osc_counter_);
        // apply new force
        rb.AddForce(new_force, ForceMode2D.Force);
        // update old force
        old_force_ = new_force;
        // move towards target by speed
        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
    }

    void SpiralMovement()
    {
        lon_osc_counter_ = (lon_osc_counter_ + lon_oscillation_) % 6.284f;
        len_osc_counter_ = (len_osc_counter_ + len_oscillation_) % 6.284f;
        // move perpendicular to center heading
        Vector2 perpendicular_vector = new Vector2(movement_heading_.y, -movement_heading_.x).normalized;
        // counteract old force
        rb.AddForce(-old_force_, ForceMode2D.Force);
        Vector2 new_force = (perpendicular_vector * len_amplitude_ * Mathf.Cos(len_osc_counter_)) + (movement_heading_ * lon_amplitude_ * Mathf.Sin(lon_osc_counter_));
        // apply new force
        rb.AddForce(new_force, ForceMode2D.Force);
        // update old force
        old_force_ = new_force;
        // move towards target by speed
        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
    }

    void RandomMovementStraight()
    {
        if (random_counter_ < random_time_)
        {
            random_counter_ += Time.deltaTime;
        }
        else
        {
            random_counter_ = 0.0f;
            // get random heading and point
            float random_angle = Random.Range(0.0f, random_angle_end_) + random_min_angle_;
            random_heading_ = new Vector2((random_heading_.x * Mathf.Cos(random_angle)) + (random_heading_.y * -Mathf.Sin(random_angle)),
                (random_heading_.x * Mathf.Sin(random_angle) + random_heading_.y * Mathf.Cos(random_angle)));
        }
        // apply random force
        rb.AddForce(-old_force_, ForceMode2D.Force);
        Vector2 new_force = random_heading_ * random_speed_;
        rb.AddForce(new_force, ForceMode2D.Force);
        old_force_ = new_force;
        // move towards target by speed
        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
    }

    void RandomMovementSmooth()
    {
        if (random_counter_ < random_time_)
        {
            random_counter_ += Time.deltaTime;
        }
        else
        {
            random_counter_ = 0.0f;
            // get random heading and point
            float random_angle = Random.Range(0.0f, random_angle_end_) + random_min_angle_;
            random_heading_ = new Vector2((random_heading_.x * Mathf.Cos(random_angle)) + (random_heading_.y * -Mathf.Sin(random_angle)),
                (random_heading_.x * Mathf.Sin(random_angle) + random_heading_.y * Mathf.Cos(random_angle)));
        }
        // incremental vector
        Vector2 current_velocity = (Vector2)transform.position + rb.velocity.normalized * random_speed_;
        Vector2 desired_velocity = (Vector2)transform.position + random_heading_ * random_speed_;
        Vector2 steering = (desired_velocity - current_velocity).normalized;
        steering *= turn_speed_;
        // apply random force
        rb.AddForce(-old_force_, ForceMode2D.Force);
        Vector2 new_force = rb.velocity.normalized * random_speed_ + steering;
        rb.AddForce(new_force, ForceMode2D.Force);
        old_force_ = new_force;
        // move towards target by speed
        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
    }
}
