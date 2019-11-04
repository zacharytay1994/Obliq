using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ImAProjectile : MonoBehaviour
{
    // Projectile variables basic
    public float speed_ = 5.0f;
    public ProjectileForceType force_type_ = ProjectileForceType.Constant;
    public bool is_spawned_prefab_ = false;
    Rigidbody2D rb;
    bool initialized_ = false;

    [Header("PROJECTILE MOVEMENT BEHAVIOUR")]
    public ProjectileMovement movement_ = ProjectileMovement.Straight;
    public float local_speed_ = 0.0f;
    [Header("Wave/Spiral Movement")]
    // amplitude, applicable to longitude and lengitude
    public float lon_oscillation_ = 0.1f;
    public float len_oscillation_ = 0.1f;
    float lon_osc_counter_ = 0.0f;
    float len_osc_counter_ = 0.0f;
    // random heading, random length, applicalble to random
    [Header("Random Movement")]
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
    public Vector2 specified_direction_ = new Vector2(0.0f, 0.0f);
    // if direction should be limited
    public bool limited_ = false;
    // mouse direction limit, onlu applicable to MouseDirectionLimit
    public float time_limit_ = 0.0f;
    float limit_counter_ = 0.0f;
    // for target mouse_point_
    Vector2 mouse_point_ = new Vector2(0.0f, 0.0f);
    Vector2 original_direction_ = new Vector2(0.0f, 0.0f);
    bool stopped_ = false;
    List<ImAProjectile> proj_list_ = new List<ImAProjectile>();
    public int circle_density_ = 0;

    // where to spawn the projectile
    [Header("SPAWN LOCATION")]
    public ProjectileSpawnLocation spawn_location_ = ProjectileSpawnLocation.RelativeSelf;
    // only applicable if RelativeSelf
    public float self_offset_ = 0.0f;
    [Tooltip("if true, offset relative to self heading, specify mouse_offset_ otherwise")]
    public bool mouse_self_heading_ = true;
    public Vector2 mouse_offset_ = new Vector2(0.0f, 0.0f);

    [Header("SPAWNING TRIGGERS")]
    public bool time_trigger_ = false;
    public ProjectileSpawnPattern tt_style_ = ProjectileSpawnPattern.Single;
    public ProjectileSpawnStyle tt_sstyle_ = ProjectileSpawnStyle.Single;
    public float time_delay_ = 0.0f;
    public string tt_prefab_ = "";
    public bool continuous_trigger_ = false;
    public ProjectileSpawnPattern cont_style_ = ProjectileSpawnPattern.Single;
    public ProjectileSpawnStyle cont_sstyle_ = ProjectileSpawnStyle.Single;
    public float time_interval_ = 0.0f;
    public string cont_prefab_ = "";
    float interval_counter_ = 0.0f;
    public bool collide_trigger_ = false;
    public ProjectileSpawnPattern colt_style_ = ProjectileSpawnPattern.Single;
    public ProjectileSpawnStyle colt_sstyle_ = ProjectileSpawnStyle.Single;
    public string colt_prefab_ = "";

    [Header("SPAWN STYLE")]
    public ProjectileSpawnStyle spawn_style_ = ProjectileSpawnStyle.Single;
    public OnCollideBasic collide_basic_ = OnCollideBasic.None;
    public OnCollideEffect collide_effect_ = OnCollideEffect.None;
    public OnCollideSpawn collide_spawn_ = OnCollideSpawn.None;
    public CollideResult collide_result_ = CollideResult.Continue;
    public LocalMovement local_movement_ = LocalMovement.Fixed;

    // debug
    int debug_counter_ = 0;

    // Projectile Variables
    Vector2 center_point_ = new Vector2(0.0f, 0.0f);
    Vector2 movement_heading_ = new Vector2(0.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitProj()
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
            case ProjectileTarget.CircularDirection:
                //center_point_ = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //InstantiateCircularPrefabs(10);
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
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized_)
        {
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
                case ProjectileTarget.CircularDirection:
                    debug_counter_++;
                    if (debug_counter_ > 200)
                    {
                        InstantiateCircularPrefabs(10);
                        debug_counter_ = 0;
                    }
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
    }

    //-------------------------------------------//
    // MOVEMENT LOGIC - updated every frame
    //-------------------------------------------//
    void StraightMovement()
    {
        // Apply constant force to bullet in straight direction
        MoveMovementHeading(0);
    }

    void LongitudeMovement()
    {
        lon_osc_counter_ = (lon_osc_counter_ + lon_oscillation_) % 6.284f;
        // counteract old force - 100.0f is there to ensure resultant vector is long enough to use for calculation
        Vector2 new_force = movement_heading_ * 100.0f * Mathf.Sin(lon_osc_counter_);
        // apply new force
        rb.velocity = ((Vector2)rb.velocity + new_force).normalized * local_speed_;
        // move towards target by speed
        MoveMovementHeading(0);
    }

    void LengitudeMovement()
    {
        len_osc_counter_ = (len_osc_counter_ + len_oscillation_) % 6.284f;
        // move perpendicular to center heading
        Vector2 perpendicular_vector = new Vector2(movement_heading_.y, -movement_heading_.x).normalized;
        Vector2 new_force = perpendicular_vector * 100.0f * Mathf.Cos(len_osc_counter_);
        // apply new force
        rb.velocity = ((Vector2)rb.velocity + new_force).normalized * local_speed_;
        // move towards target by speed
        MoveMovementHeading(0);
    }

    void SpiralMovement()
    {
        lon_osc_counter_ = (lon_osc_counter_ + lon_oscillation_) % 6.284f;
        len_osc_counter_ = (len_osc_counter_ + len_oscillation_) % 6.284f;
        // move perpendicular to center heading
        Vector2 perpendicular_vector = new Vector2(movement_heading_.y, -movement_heading_.x).normalized;
        Vector2 new_force = (perpendicular_vector * 100.0f * Mathf.Cos(len_osc_counter_)) + (movement_heading_ * 100.0f * Mathf.Sin(lon_osc_counter_));
        // apply new force
        rb.velocity = ((Vector2)rb.velocity + new_force).normalized * local_speed_;
        // move towards target by speed
        MoveMovementHeading(0);
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
        MoveMovementHeading(0);
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
        MoveMovementHeading(0);
    }

    // General functions
    void MoveMovementHeading(int forcemode)
    {
        if (forcemode == 0)
        {
            switch (target_)
            {
                case ProjectileTarget.MousePoint:
                    if (!(movement_ == ProjectileMovement.Straight))
                    {
                        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
                    }
                    break;
                case ProjectileTarget.MouseFollow:
                    rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
                    break;
                case ProjectileTarget.MouseDirection:
                case ProjectileTarget.SpecifyDirection:
                    if (!limited_)
                    {
                        rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
                    }
                    else
                    {
                        if (!stopped_)
                        {
                            if (limit_counter_ < time_limit_)
                            {
                                limit_counter_ += Time.deltaTime;
                                rb.AddForce(movement_heading_ * speed_, ForceMode2D.Force);
                            }
                            else
                            {
                                // get direction of velocity
                                float direction_magnitude = Vector2.Dot((Vector2)rb.velocity, movement_heading_);
                                Vector2 direction_velocity = movement_heading_ * direction_magnitude;
                                rb.velocity -= direction_velocity;
                                stopped_ = true;
                            }
                        }
                    }
                    break;
            }
        }
        else
        {
            rb.AddForce(movement_heading_ * speed_, ForceMode2D.Impulse);
        }
    }

    void InstantiateCircularPrefabs(int density)
    {
        GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Attacks/Projectiles/" + "TestPrefabProj" + ".prefab", typeof(GameObject));
        for (int i = 0; i < density; i++)
        {
            GameObject temp = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
            //temp.GetComponent<ImAProjectile>().InitProj();
            proj_list_.Add(temp.GetComponent<ImAProjectile>());
        }
        InitSpawnedProjectiles(density);
        foreach(ImAProjectile z in proj_list_)
        {
            z.InitProj();
        }
        proj_list_.Clear();
    }

    void InitSpawnedProjectiles(int density)
    {
        float angle = 6.284f / density;
        float first_angle = 0.0f;
        foreach (ImAProjectile i in proj_list_)
        {
            // get angle
            first_angle += angle;
            i.specified_direction_ = new Vector2((specified_direction_.x * Mathf.Cos(first_angle)) + (specified_direction_.y * -Mathf.Sin(first_angle)), ((specified_direction_.x * Mathf.Sin(first_angle)) + (specified_direction_.y * Mathf.Cos(first_angle))));
        }
    }
}
