using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ImAProjectile : MonoBehaviour
{
    // Projectile variables basic
    [SerializeField]
    float speed_ = 5.0f;
    [SerializeField]
    float acceleration_ = 0.0f;
    [SerializeField]
    float max_speed_ = 0.0f;
    [Header("1. ADVANCED SPEED BEHAVIOUR")]
    [SerializeField]
    bool custom_speed_ = false;
    [SerializeField]
    bool custom_speed_loop_ = false;
    [SerializeField]
    float decceleration_ = 0.0f;
    [SerializeField]
    float acceleration_time_ = 0.0f;
    [SerializeField]
    float decceleration_time_ = 0.0f;
    float acceleration_counter_ = 0.0f;
    float decceleration_counter_ = 0.0f;
    bool accelerating_ = true;
    [Header("2. FORCE TYPE")]
    [SerializeField]
    ProjectileForceType force_type_ = ProjectileForceType.Constant;
    Rigidbody2D rb;
    bool initialized_ = false;
    [Header("3. LIFETIME BEHAVIOUR")]
    [SerializeField]
    bool persistent_ = false;
    [SerializeField]
    float lifespan_ = 0.0f;
    //[SerializeField]
    //bool destroy_on_collide_ = false;
    [SerializeField]
    LayerMask on_collide_mask_ = 10;
    [SerializeField]
    float destroy_countdown_ = 0.0f;
    bool to_be_destroyed_ = false;

    [Header("4. PROJECTILE MOVEMENT BEHAVIOUR")]
    [SerializeField]
    ProjectileMovement movement_ = ProjectileMovement.Straight;
    [SerializeField]
    float local_speed_ = 0.0f;
    [Header("4.1 Wave/Spiral Movement")]
    [SerializeField]
    bool random_spread_ = false;
    [SerializeField]
    [Range(0.0f, 1.571f)]
    float spread_range_ = 0.0f;

    // amplitude, applicable to longitude and lengitude
    [SerializeField]
    bool stable_ = true;
    [SerializeField]
    float lon_oscillation_ = 0.1f;
    [SerializeField]
    float len_oscillation_ = 0.1f;
    float lon_osc_counter_ = 0.0f;
    float len_osc_counter_ = 0.0f;
    // random heading, random length, applicalble to random
    [Header("4.2 Random Movement")]
    [Tooltip("radius to find new random point")]
    [SerializeField]
    float random_range_ = 2.0f; // range of selecting random point
    [Tooltip("time in seconds before new random")]
    [SerializeField]
    float random_time_ = 0.4f; // duration before finding new random
    [SerializeField]
    float random_min_angle_ = 145.0f;
    [SerializeField]
    float random_angle_end_ = 70;
    [SerializeField]
    float turn_speed_ = 100.0f;
    Vector2 random_heading_ = new Vector2(0.0f, 0.0f);
    float random_counter_ = 0.0f;
    

    // target to fire at
    [Header("5. PROJECTILE TARGET SELECT")]
    [SerializeField]
    public ProjectileTarget target_ = ProjectileTarget.MouseDirection;
    // direction to fire at, only applicable to DirectionLong and DirectionShort
    [SerializeField]
    public Vector2 specified_direction_ = new Vector2(0.0f, 0.0f);
    // if direction should be limited
    [SerializeField]
    [Tooltip("if the projectile travel distance should be limited")]
    bool limited_ = false;
    // mouse direction limit, onlu applicable to MouseDirectionLimit
    [SerializeField]
    float time_limit_ = 0.0f;
    float limit_counter_ = 0.0f;
    // for target mouse_point_
    Vector2 mouse_point_ = new Vector2(0.0f, 0.0f);
    Vector2 original_direction_ = new Vector2(0.0f, 0.0f);
    bool stopped_ = false;
    List<ImAProjectile> proj_list_ = new List<ImAProjectile>();
    List<GameObject> go_list_ = new List<GameObject>();

    // where to spawn the projectile
    [Header("6. SPAWN LOCATION")]
    [SerializeField]
    [Tooltip("where the projectile should spawn")]
    ProjectileSpawnLocation spawn_location_ = ProjectileSpawnLocation.RelativeSelf;
    // only applicable if RelativeSelf
    [SerializeField]
    float self_offset_ = 0.0f;
    [Tooltip("if true, offset relative to self heading, specify mouse_offset_ otherwise")]
    [SerializeField]
    bool mouse_self_heading_ = true;
    [SerializeField]
    Vector2 mouse_offset_ = new Vector2(0.0f, 0.0f);

    [Header("7. SPAWNING TRIGGERS")]
    // the local orientation used for spawning projectiles
    [SerializeField]
    float local_orientation_ = 0.0f;
    [SerializeField]
    bool follow_mouse_ = false;
    [SerializeField]
    public float spawn_delay_ = 0.0f;
    public bool start_spawning_ = false;
    [Header("To Spin or Not To Spin")]
    [SerializeField]
    bool spin_ = false;
    [SerializeField]
    float spin_rate_ = 0.0f;
    [Header("7.1 Time Trigger")]
    [SerializeField]
    [Tooltip("trigger this spawning effect after a time delay")]
    bool time_trigger_ = false;
    [SerializeField]
    float active_time_ = 0.0f;
    float active_counter_ = 0.0f;
    [SerializeField]
    ProjectileSpawnPattern tt_style_ = ProjectileSpawnPattern.None;
    [SerializeField]
    int tt_density_ = 0;
    [SerializeField]
    float tt_angle_ = 0.0f;
    [SerializeField]
    ProjectileSpawnStyle tt_sstyle_ = ProjectileSpawnStyle.Stream;
    float tt_stream_counter_ = 0.0f;
    [SerializeField]
    float tt_stream_rate_ = 0.0f;
    bool tt_bursting_ = true;
    [SerializeField]
    float tt_burst_rest_timer_ = 0.0f;
    [SerializeField]
    float tt_burst_timer_ = 0.0f;
    float tt_burst_rest_counter_ = 0.0f;
    float tt_burst_counter_ = 0.0f;
    [SerializeField]
    float time_trigger_delay_ = 0.0f;
    [SerializeField]
    GameObject tt_prefab_ = null;
    [SerializeField]
    [Tooltip("for prefabs that don't have this script attached, define offset here")]
    float tt_np_offset_ = 0.0f;
    [SerializeField]
    [Tooltip("for prefabs that don't have this script attached, define impulse speed here")]
    float tt_np_speed_ = 0.0f;
    float time_delay_counter_ = 0.0f;
    [Header("7.2 Continuous Trigger")]
    [SerializeField]
    [Tooltip("trigger this spawning effect at a fixed spawn rate")]
    bool continuous_trigger_ = false;
    [SerializeField]
    ProjectileSpawnPattern cont_style_ = ProjectileSpawnPattern.None;
    [SerializeField]
    int cont_density_ = 0;
    [SerializeField]
    float cont_angle_ = 0.0f;
    [SerializeField]
    ProjectileSpawnStyle cont_sstyle_ = ProjectileSpawnStyle.Stream;
    float cont_stream_counter_ = 0.0f;
    [SerializeField]
    float cont_stream_rate_ = 0.0f;
    bool cont_bursting_ = true;
    [SerializeField]
    float cont_burst_rest_timer_ = 0.0f;
    [SerializeField]
    float cont_burst_timer_ = 0.0f;
    float cont_burst_rest_counter_ = 0.0f;
    float cont_burst_counter_ = 0.0f;
    //[SerializeField]
    //float time_interval_ = 0.0f;
    [SerializeField]
    GameObject cont_prefab_ = null;
    [SerializeField]
    [Tooltip("for prefabs that don't have this script attached, define offset here")]
    float cont_np_offset_ = 0.0f;
    [SerializeField]
    [Tooltip("for prefabs that don't have this script attached, define impulse speed here")]
    float cont_np_speed_ = 0.0f;
    //float interval_counter_ = 0.0f;
    [Header("7.3 Collide Trigger")]
    [SerializeField]
    [Tooltip("trigger this spawning effect on collision")]
    bool collide_trigger_ = false;
    [SerializeField]
    [Tooltip("specified layer mask to check for collisions")]
    LayerMask layer_to_collide_ = 10;
    [SerializeField]
    float col_active_time_ = 0.0f;
    float col_active_counter_ = 0.0f;
    bool collided_ = false;
    [SerializeField]
    ProjectileSpawnPattern colt_style_ = ProjectileSpawnPattern.None;
    [SerializeField]
    int colt_density_ = 0;
    [SerializeField]
    float colt_angle_ = 0.0f;
    [SerializeField]
    ProjectileSpawnStyle colt_sstyle_ = ProjectileSpawnStyle.Stream;
    float colt_stream_counter_ = 0.0f;
    [SerializeField]
    float colt_stream_rate_ = 0.0f;
    bool colt_bursting_ = true;
    [SerializeField]
    float colt_burst_rest_timer_ = 0.0f;
    [SerializeField]
    float colt_burst_timer_ = 0.0f;
    float colt_burst_rest_counter_ = 0.0f;
    float colt_burst_counter_ = 0.0f;
    [SerializeField]
    GameObject colt_prefab_ = null;
    [SerializeField]
    [Tooltip("for prefabs that don't have this script attached, define offset here")]
    float colt_np_offset_ = 0.0f;
    [SerializeField]
    [Tooltip("for prefabs that don't have this script attached, define impulse speed here")]
    float colt_np_speed_ = 0.0f;

    // debug
    //int debug_counter_ = 0;

    // Projectile Variables
    Vector2 center_point_ = new Vector2(0.0f, 0.0f);
    public Vector2 movement_heading_ = new Vector2(0.0f, 0.0f);

    bool paused_ = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitProj()
    {
        cont_stream_counter_ = cont_stream_rate_;
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
            case ProjectileTarget.MousePoint:
                mouse_point_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                movement_heading_ = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)gameObject.transform.position).normalized;
                if (movement_ == ProjectileMovement.Straight)
                {
                    original_direction_ = movement_heading_;
                    rb.velocity += movement_heading_ * speed_;
                }
                break;
            case ProjectileTarget.MouseFollow:
            case ProjectileTarget.MouseDirection:
                movement_heading_ = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)gameObject.transform.position).normalized;
                break;
            case ProjectileTarget.SpecifyDirection:
                movement_heading_ = specified_direction_;
                break;
            //case ProjectileTarget.NearestEnemy:
            //    // to be filled
            //    break;
        }
        // set center point
        switch (spawn_location_)
        {
            case ProjectileSpawnLocation.RelativeSelf:
                center_point_ = (Vector2)gameObject.transform.position + (movement_heading_ * self_offset_);
                break;
            case ProjectileSpawnLocation.Mouse:
                if (mouse_self_heading_)
                {
                    center_point_ = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + (movement_heading_ * self_offset_);
                }
                else
                {
                    center_point_ = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + (mouse_offset_);
                }
                break;
        }
        // set projectile at center point
        transform.position = center_point_;
        initialized_ = true;

        if (force_type_ == ProjectileForceType.Impulse)
        {
            Vector2 tempered_movement_heading = movement_heading_;
            if (random_spread_)
            {
                // get random angle
                float random_angle = Random.Range(-spread_range_, spread_range_);
                // rotate vector by amount
                tempered_movement_heading = new Vector2((tempered_movement_heading.x * Mathf.Cos(random_angle) + tempered_movement_heading.x * -Mathf.Sin(random_angle)),
                    (tempered_movement_heading.y * Mathf.Sin(random_angle) + tempered_movement_heading.y * Mathf.Cos(random_angle)));
            }
            rb.AddForce(tempered_movement_heading * speed_, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (initialized_ && !paused_)
        {
            // update life
            if (!persistent_)
            {
                if (lifespan_ > 0.0f)
                {
                    lifespan_ -= Time.deltaTime;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            if (to_be_destroyed_)
            {
                if (destroy_countdown_ > 0.0f)
                {
                    destroy_countdown_ -= Time.deltaTime;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            // update speed - not to be confused with linear drag, this is speed of the 'system'
            if (!custom_speed_)
            {
                speed_ = speed_ < max_speed_ ? speed_ + acceleration_ : max_speed_;
            }
            else if (custom_speed_)
            {
                if (accelerating_)
                {
                    if (acceleration_counter_ < acceleration_time_)
                    {
                        acceleration_counter_ += Time.deltaTime;
                        speed_ = speed_ < max_speed_ ? speed_ + acceleration_ : max_speed_;
                    }
                    else
                    {
                        accelerating_ = false;
                        acceleration_counter_ = 0.0f;
                    }
                }
                else
                {
                    if (decceleration_counter_ > decceleration_time_)
                    {
                        if (custom_speed_loop_)
                        {
                            accelerating_ = true;
                            decceleration_counter_ = 0.0f;
                        }
                    }
                    else
                    {
                        decceleration_counter_ += Time.deltaTime;
                        speed_ = speed_ > 0.0f ? speed_ - decceleration_ : 0.0f;
                    }
                }
            }

            // update movement heading based on target
            switch (target_)
            {
                case ProjectileTarget.MouseFollow:
                    movement_heading_ = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
                    break;
                case ProjectileTarget.MousePoint:
                    if (movement_ == ProjectileMovement.Straight)
                    {
                        // current heading
                        Vector2 current_heading = mouse_point_ - (Vector2)transform.position;
                        if (Vector2.Dot(current_heading, original_direction_) < 0 && !stopped_)
                        {
                            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                            stopped_ = true;
                        }
                    }
                    else
                    {
                        movement_heading_ = (mouse_point_ - (Vector2)gameObject.transform.position).normalized;
                    }
                    break;
                case ProjectileTarget.SpecifyDirection:
                    movement_heading_ = specified_direction_;
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
                case ProjectileMovement.RandomStraight:
                    RandomMovementStraight();
                    break;
                case ProjectileMovement.RandomSmooth:
                    RandomMovementSmooth();
                    break;
            }

            if (follow_mouse_)
            {
                local_orientation_ = GetOrientation();
            }

            // Process spawning behaviour
            if (spawn_delay_ > 0)
            {
                spawn_delay_ -= Time.deltaTime;
            }
            else
            {
                start_spawning_ = true;
            }
            if (time_trigger_ && start_spawning_)
            {
                if (time_delay_counter_ < time_trigger_delay_)
                {
                    time_delay_counter_ += Time.deltaTime;
                }
                else
                {
                    if (active_counter_ < active_time_)
                    {
                        switch (tt_style_)
                        {
                            case ProjectileSpawnPattern.Circle:
                                CircularBurstStream(tt_sstyle_, tt_density_, tt_prefab_, ref tt_stream_counter_, ref tt_stream_rate_,
                                    ref tt_bursting_, ref tt_burst_rest_counter_, tt_burst_rest_timer_, ref tt_burst_counter_, tt_burst_timer_,
                                    tt_np_offset_, tt_np_speed_);
                                break;
                            case ProjectileSpawnPattern.Cone:
                                ConalBurstStream(tt_sstyle_, tt_density_, tt_angle_, tt_prefab_, ref tt_stream_counter_, ref tt_stream_rate_,
                                    ref tt_bursting_, ref tt_burst_rest_counter_, tt_burst_rest_timer_, ref tt_burst_counter_, tt_burst_timer_,
                                    tt_np_offset_, tt_np_speed_);
                                break;
                            case ProjectileSpawnPattern.None:
                                SingularBurstStream(tt_sstyle_, tt_prefab_, ref tt_stream_counter_, ref tt_stream_rate_,
                                    ref tt_bursting_, ref tt_burst_rest_counter_, tt_burst_rest_timer_, ref tt_burst_counter_, tt_burst_timer_);
                                break;
                        }
                        active_counter_ += Time.deltaTime;
                    }
                    else
                    {
                        time_trigger_ = false;
                    }
                }
            }
            if (continuous_trigger_ && start_spawning_)
            {
                switch (cont_style_)
                {
                    case ProjectileSpawnPattern.Circle:
                        CircularBurstStream(cont_sstyle_, cont_density_, cont_prefab_, ref cont_stream_counter_, ref cont_stream_rate_,
                            ref cont_bursting_, ref cont_burst_rest_counter_, cont_burst_rest_timer_, ref cont_burst_counter_, cont_burst_timer_,
                            cont_np_offset_, cont_np_speed_);
                        break;
                    case ProjectileSpawnPattern.Cone:
                        ConalBurstStream(cont_sstyle_, cont_density_, cont_angle_, cont_prefab_, ref cont_stream_counter_, ref cont_stream_rate_,
                            ref cont_bursting_, ref cont_burst_rest_counter_, cont_burst_rest_timer_, ref cont_burst_counter_, cont_burst_timer_,
                            cont_np_offset_, cont_np_speed_);
                        break;
                    case ProjectileSpawnPattern.None:
                        SingularBurstStream(cont_sstyle_, cont_prefab_, ref cont_stream_counter_, ref cont_stream_rate_,
                            ref cont_bursting_, ref cont_burst_rest_counter_, cont_burst_rest_timer_, ref cont_burst_counter_, cont_burst_timer_);
                        break;
                }
            }
            if (collide_trigger_ && start_spawning_)
            {
                if (collided_)
                {
                    if (col_active_counter_ < col_active_time_)
                    {
                        switch (colt_style_)
                        {
                            case ProjectileSpawnPattern.Circle:
                                CircularBurstStream(colt_sstyle_, colt_density_, colt_prefab_, ref colt_stream_counter_, ref colt_stream_rate_,
                                    ref colt_bursting_, ref colt_burst_rest_counter_, colt_burst_rest_timer_, ref colt_burst_counter_, colt_burst_timer_,
                                    colt_np_offset_, colt_np_speed_);
                                break;
                            case ProjectileSpawnPattern.Cone:
                                ConalBurstStream(colt_sstyle_, colt_density_, colt_angle_, colt_prefab_, ref colt_stream_counter_, ref colt_stream_rate_,
                                    ref colt_bursting_, ref colt_burst_rest_counter_, colt_burst_rest_timer_, ref colt_burst_counter_, colt_burst_timer_,
                                    colt_np_offset_, colt_np_speed_);
                                break;
                            case ProjectileSpawnPattern.None:
                                SingularBurstStream(colt_sstyle_, colt_prefab_, ref colt_stream_counter_, ref colt_stream_rate_,
                                    ref colt_bursting_, ref colt_burst_rest_counter_, colt_burst_rest_timer_, ref colt_burst_counter_, colt_burst_timer_);
                                break;
                        }
                        col_active_counter_ += Time.deltaTime;
                    }
                    else
                    {
                        collide_trigger_ = false;
                    }
                }
            }

            // update spinning behaviour
            if (spin_)
            {
                local_orientation_ -= spin_rate_ * Time.deltaTime;
                local_orientation_ = local_orientation_ > 360.0f ? local_orientation_ - 360.0f : local_orientation_;
                local_orientation_ = local_orientation_ < 0.0f ? 360.0f - local_orientation_ : local_orientation_;
            }

            // update rotation
            float temp_angle = -Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                rb.MoveRotation(temp_angle);
            }
        }
    }

    //-------------------------------------------//
    // MOVEMENT LOGIC - updated every frame
    //-------------------------------------------//
    void StraightMovement()
    {
        // Apply constant force to bullet in straight direction
        if (force_type_ == ProjectileForceType.Constant) { MoveMovementHeading(); }
    }

    void LongitudeMovement()
    {
        lon_osc_counter_ = (lon_osc_counter_ + lon_oscillation_) % 6.284f;
        // counteract old force - 100.0f is there to ensure resultant vector is long enough to use for calculation
        Vector2 new_force = movement_heading_ * 100.0f * Mathf.Sin(lon_osc_counter_);
        // apply new force
        rb.velocity = stable_ ? ((Vector2)rb.velocity + new_force).normalized * local_speed_ : (Vector2)rb.velocity + new_force.normalized * local_speed_;
        // move towards target by speed
        if (force_type_ == ProjectileForceType.Constant) { MoveMovementHeading(); }
    }

    void LengitudeMovement()
    {
        len_osc_counter_ = (len_osc_counter_ + len_oscillation_) % 6.284f;
        // move perpendicular to center heading
        Vector2 perpendicular_vector = new Vector2(movement_heading_.y, -movement_heading_.x).normalized;
        Vector2 new_force = perpendicular_vector * 100.0f * Mathf.Cos(len_osc_counter_);
        // apply new force
        rb.velocity = stable_ ? ((Vector2)rb.velocity + new_force).normalized * local_speed_ : (Vector2)rb.velocity + new_force.normalized * local_speed_;
        // move towards target by speed
        if (force_type_ == ProjectileForceType.Constant) { MoveMovementHeading(); }
    }

    void SpiralMovement()
    {
        lon_osc_counter_ = (lon_osc_counter_ + lon_oscillation_) % 6.284f;
        len_osc_counter_ = (len_osc_counter_ + len_oscillation_) % 6.284f;
        // move perpendicular to center heading
        Vector2 perpendicular_vector = new Vector2(movement_heading_.y, -movement_heading_.x).normalized;
        Vector2 new_force = (perpendicular_vector * 100.0f * Mathf.Cos(len_osc_counter_)) + (movement_heading_ * 100.0f * Mathf.Sin(lon_osc_counter_));
        // apply new force
        rb.velocity = stable_ ? ((Vector2)rb.velocity + new_force).normalized * local_speed_ : (Vector2)rb.velocity + new_force.normalized * local_speed_;
        // move towards target by speed
        if (force_type_ == ProjectileForceType.Constant) { MoveMovementHeading(); }
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
        // cancel old velocity heading to keep straight path
        rb.velocity -= -random_heading_ * Mathf.Min(((Vector2)rb.velocity).magnitude, local_speed_);
        // apply random force
        rb.velocity = ((Vector2)rb.velocity + random_heading_).normalized * local_speed_;
        // move towards target by speed
        if (force_type_ == ProjectileForceType.Constant) { MoveMovementHeading(); }
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
        Vector2 temp_vel = (Vector2)rb.velocity;
        temp_vel = temp_vel.normalized;
        Vector2 current_velocity = (Vector2)transform.position + temp_vel * local_speed_;
        Vector2 desired_velocity = (Vector2)transform.position + random_heading_ * local_speed_;
        Vector2 steering = (desired_velocity - current_velocity).normalized;
        steering *= turn_speed_;
        // apply random force
        Vector2 new_force = temp_vel * local_speed_ + steering;
        rb.velocity = ((Vector2)rb.velocity + new_force).normalized * local_speed_;
        // move towards target by speed
        if (force_type_ == ProjectileForceType.Constant) { MoveMovementHeading(); }
    }

    // General functions
    void MoveMovementHeading()
    {
        Vector2 tempered_movement_heading = movement_heading_;
        if (random_spread_)
        {
            // get random angle
            float random_angle = Random.Range(-spread_range_, spread_range_);
            // rotate vector by amount
            tempered_movement_heading = new Vector2((tempered_movement_heading.x * Mathf.Cos(random_angle) + tempered_movement_heading.x * -Mathf.Sin(random_angle)),
                (tempered_movement_heading.y * Mathf.Sin(random_angle) + tempered_movement_heading.y * Mathf.Cos(random_angle)));
        }
        switch (target_)
        {
            case ProjectileTarget.MousePoint:
                if (!(movement_ == ProjectileMovement.Straight))
                {
                    rb.AddForce(tempered_movement_heading * speed_, ForceMode2D.Force);
                }
                break;
            case ProjectileTarget.MouseFollow:
                rb.AddForce(tempered_movement_heading * speed_, ForceMode2D.Force);
                break;
            case ProjectileTarget.MouseDirection:
            case ProjectileTarget.SpecifyDirection:
                if (!limited_)
                {
                    rb.AddForce(tempered_movement_heading * speed_, ForceMode2D.Force);
                }
                else
                {
                    if (!stopped_)
                    {
                        if (limit_counter_ < time_limit_)
                        {
                            limit_counter_ += Time.deltaTime;
                            rb.AddForce(tempered_movement_heading * speed_, ForceMode2D.Force);
                        }
                        else
                        {
                            // get direction of velocity
                            float direction_magnitude = Vector2.Dot((Vector2)rb.velocity, movement_heading_);
                            Vector2 direction_velocity = tempered_movement_heading * direction_magnitude;
                            rb.velocity -= direction_velocity;
                            stopped_ = true;
                        }
                    }
                }
                break;
        }
    }

    void InstantiateCircularPrefabs(int density, GameObject projprefab, float offset, float speed)
    {
        // if it is a ImAProjectile
        if (projprefab.GetComponent<ImAProjectile>() != null)
        {
            for (int i = 0; i < density; i++)
            {
                GameObject temp = (GameObject)Instantiate(projprefab, transform.position, Quaternion.identity);
                proj_list_.Add(temp.GetComponent<ImAProjectile>());
            }
            InitSpawnedCircleProjectiles(density);
            foreach (ImAProjectile z in proj_list_)
            {
                z.InitProj();
            }
            proj_list_.Clear();
        }
        else
        {
            for (int i = 0; i < density; i++)
            {
                GameObject temp = (GameObject)Instantiate(projprefab, transform.position, Quaternion.identity);
                go_list_.Add(temp);
            }
            InitNonProjectileCirclePrefab(density, offset, speed);
            go_list_.Clear();
        }
    }

    void InitSpawnedCircleProjectiles(int density)
    {
        float angle = 6.284f / density;
        float first_angle = local_orientation_ * (3.142f/180.0f);
        foreach (ImAProjectile i in proj_list_)
        {
            // get angle
            first_angle += angle;
            i.target_ = ProjectileTarget.SpecifyDirection;
            i.specified_direction_ = new Vector2((specified_direction_.x * Mathf.Cos(first_angle)) + (specified_direction_.y * -Mathf.Sin(first_angle)), ((specified_direction_.x * Mathf.Sin(first_angle)) + (specified_direction_.y * Mathf.Cos(first_angle))));
        }
    }

    void InitNonProjectileCirclePrefab(int density, float offset, float speed)
    {
        float angle = 6.284f / density;
        float first_angle = local_orientation_ * (3.142f / 180.0f);
        foreach (GameObject g in go_list_)
        {
            // get angle
            first_angle += angle;
            Vector2 direction = new Vector2((specified_direction_.x * Mathf.Cos(first_angle)) + (specified_direction_.y * -Mathf.Sin(first_angle)), ((specified_direction_.x * Mathf.Sin(first_angle)) + (specified_direction_.y * Mathf.Cos(first_angle)))).normalized;
            g.transform.position = (Vector2)g.transform.position + direction * offset;
            g.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        }
    }

    void InstantiateConalPrefabs(int density, float angle, GameObject projprefab, float offset, float speed)
    {
        // if it is a ImAProjectile
        if (projprefab.GetComponent<ImAProjectile>() != null)
        {
            for (int i = 0; i < density; i++)
            {
                GameObject temp = (GameObject)Instantiate(projprefab, transform.position, Quaternion.identity);
                proj_list_.Add(temp.GetComponent<ImAProjectile>());
            }
            InitSpawnedConalPrefabs(density, angle * (3.142f / 180.0f));
            foreach (ImAProjectile z in proj_list_)
            {
                z.InitProj();
            }
            proj_list_.Clear();
        }
        else
        {
            for (int i = 0; i < density; i++)
            {
                GameObject temp = (GameObject)Instantiate(projprefab, transform.position, Quaternion.identity);
                go_list_.Add(temp);
            }
            InitNonProjectileConalPrefabs(density, angle * (3.142f / 180.0f), offset, speed);
            go_list_.Clear();
        }
    }

    void InitSpawnedConalPrefabs(int density, float angle)
    {
        float lo_rad = local_orientation_ * (3.142f / 180.0f);
        Vector2 start_heading = new Vector2();
        if (follow_mouse_)
        {
            Vector2 mouse_direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
            start_heading = new Vector2((mouse_direction.x * Mathf.Cos(-angle / 2) + mouse_direction.y * -Mathf.Sin(-angle / 2)), (mouse_direction.x * Mathf.Sin(-angle / 2) + mouse_direction.y * Mathf.Cos(-angle / 2)));
        }
        else
        {
            start_heading = new Vector2((0 * Mathf.Cos(lo_rad - (angle / 2))) + (1 * -Mathf.Sin(lo_rad - (angle / 2))), ((0 * Mathf.Sin(lo_rad - (angle / 2))) + (1 * Mathf.Cos(lo_rad - (angle / 2)))));
        }
        float increment_angle = angle / (float)(density - 1);
        float temp_angle = 0.0f;
        // angle to rotate to
        for (int i = 0; i < density; i++, temp_angle += increment_angle)
        {
            proj_list_[i].target_ = ProjectileTarget.SpecifyDirection;
            proj_list_[i].specified_direction_ = new Vector2((start_heading.x * Mathf.Cos(temp_angle)) + (start_heading.y * -Mathf.Sin(temp_angle)), ((start_heading.x * Mathf.Sin(temp_angle)) + (start_heading.y * Mathf.Cos(temp_angle))));
        }
    }

    void InitNonProjectileConalPrefabs(int density, float angle, float offset, float speed)
    {
        float lo_rad = local_orientation_ * (3.142f / 180.0f);
        Vector2 start_heading = new Vector2();
        if (follow_mouse_)
        {
            Vector2 mouse_direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
            start_heading = new Vector2((mouse_direction.x * Mathf.Cos(-angle / 2) + mouse_direction.y * -Mathf.Sin(-angle / 2)), (mouse_direction.x * Mathf.Sin(-angle / 2) + mouse_direction.y * Mathf.Cos(-angle / 2)));
        }
        else
        {
            start_heading = new Vector2((0 * Mathf.Cos(lo_rad - (angle / 2))) + (1 * -Mathf.Sin(lo_rad - (angle / 2))), ((0 * Mathf.Sin(lo_rad - (angle / 2))) + (1 * Mathf.Cos(lo_rad - (angle / 2)))));
        }
        
        float increment_angle = angle / (float)(density - 1);
        float temp_angle = 0.0f;
        // angle to rotate to
        for (int i = 0; i < density; i++, temp_angle += increment_angle)
        {
            Vector2 direction = new Vector2((start_heading.x * Mathf.Cos(temp_angle)) + (start_heading.y * -Mathf.Sin(temp_angle)), ((start_heading.x * Mathf.Sin(temp_angle)) + (start_heading.y * Mathf.Cos(temp_angle)))).normalized;
            go_list_[i].transform.position = (Vector2)go_list_[i].transform.position + direction * offset;
            go_list_[i].GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        }
    }

    void InstantiateNonePattern(GameObject projprefab)
    {
        // check if has ImAProjectile
        GameObject temp = (GameObject)Instantiate(projprefab, transform.position, Quaternion.identity);
        if (temp.GetComponent<ImAProjectile>() != null)
        {
            temp.GetComponent<ImAProjectile>().specified_direction_ = specified_direction_;
            temp.GetComponent<ImAProjectile>().InitProj();
        }
    }

    bool StreamTrigger(ref float counter, float timer)
    {
        if (counter < timer)
        {
            counter += Time.deltaTime;
            return false;
        }
        else
        {
            counter = 0.0f;
            return true;
        }
    }

    bool BurstFlag(ref bool flag, ref float burstrestcounter, float burstresttimer, ref float burstcounter, float bursttimer)
    {
        bool result = false;
        if (flag)
        {
            if (burstcounter < bursttimer)
            {
                burstcounter += Time.deltaTime;
                result = true;
            }
            else
            {
                burstcounter = 0.0f;
                flag = false;
            }
        }
        else
        {
            if (burstrestcounter < burstresttimer)
            {
                burstrestcounter += Time.deltaTime;
                result = false;
            }
            else
            {
                burstrestcounter = 0.0f;
                flag = true;
            }
        }
        return result;
    }

    // Wrappers
    void CircularBurstStream(ProjectileSpawnStyle style,
        int density, GameObject prefab,
        ref float streamcounter, ref float streamrate,
        ref bool bursting, ref float burstrestcounter, float burstresttimer, ref float burstcounter, float bursttimer,
        float offset, float speed)
    {
        if (style == ProjectileSpawnStyle.Stream)
        {
            if (StreamTrigger(ref streamcounter, streamrate))
            {
                InstantiateCircularPrefabs(density, prefab, offset, speed);
            }
        }
        else if (style == ProjectileSpawnStyle.Burst)
        {
            if (BurstFlag(ref bursting, ref burstrestcounter, burstresttimer, ref burstcounter, bursttimer))
            {
                if (StreamTrigger(ref streamcounter, streamrate))
                {
                    InstantiateCircularPrefabs(density, prefab, offset, speed);
                }
            }
            else
            {
                streamcounter = 0.0f;
            }
        }
    }

    void ConalBurstStream(ProjectileSpawnStyle style,
        int density, float angle, GameObject prefab,
        ref float streamcounter, ref float streamrate,
        ref bool bursting, ref float burstrestcounter, float burstresttimer, ref float burstcounter, float bursttimer,
        float offset, float speed)
    {
        if (style == ProjectileSpawnStyle.Stream)
        {
            if (StreamTrigger(ref streamcounter, streamrate))
            {
                InstantiateConalPrefabs(density, angle, prefab, offset, speed);
            }
        }
        else if (style == ProjectileSpawnStyle.Burst)
        {
            if (BurstFlag(ref bursting, ref burstrestcounter, burstresttimer, ref burstcounter, bursttimer))
            {
                if (StreamTrigger(ref streamcounter, streamrate))
                {
                    InstantiateConalPrefabs(density, angle, prefab, offset, speed);
                }
            }
            else
            {
                streamcounter = 0.0f;
            }
        }
    }

    void SingularBurstStream(ProjectileSpawnStyle style, 
        GameObject prefab,
        ref float streamcounter, ref float streamrate,
        ref bool bursting, ref float burstrestcounter, float burstresttimer, ref float burstcounter, float bursttimer)
    {
        if (style == ProjectileSpawnStyle.Stream)
        {
            if (StreamTrigger(ref streamcounter, streamrate))
            {
                InstantiateNonePattern(prefab);
            }
        }
        else if (style == ProjectileSpawnStyle.Burst)
        {
            if (BurstFlag(ref bursting, ref burstrestcounter, burstresttimer, ref burstcounter, bursttimer))
            {
                if (StreamTrigger(ref streamcounter, streamrate))
                {
                    InstantiateNonePattern(prefab);
                }
            }
            else
            {
                streamcounter = 0.0f;
            }
        }
    }
    float GetOrientation()
    {
        float opposite = gameObject.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float adjacent = -(gameObject.transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        return Mathf.Atan(opposite / adjacent) * (180.0f / 3.142f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (layer_to_collide_ == (layer_to_collide_ | (1 << collision.gameObject.layer)))
        {
            collided_ = true;
        }
        if (on_collide_mask_ == (on_collide_mask_ | (1 << collision.gameObject.layer)))
        {
            to_be_destroyed_ = true;
        }
    }

    public void SetPause(bool b)
    {
        paused_ = b;
    }
}
