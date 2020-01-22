using System.Collections;
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

    [Space(10)]
    [SerializeField] GameObject strike_zone_;
    [SerializeField] float attack_duration_;
    [SerializeField] float dash_strength;

    [SerializeField]float dash_cooldown_; //time in between dash
    [SerializeField] float dash_duration_;
    float next_dash_time = 0.0f;
    float dash_start;
    float dash_timer;
    public float speed_modifier_ = 1; 

    SemiCircleMelee melee_;
    public float stored_angle_ = 0.0f;

    [SerializeField] public float invincibility_time_;
    bool is_invincible_;
    float invincibility_start_time_;

    float trail_active_time_ = 0.0f;

    [SerializeField] GameObject dash_particle_ = null;

    AudioManager am_;
    [SerializeField] string dash_sound_;
    [SerializeField]
    float invuln_dash_ = 0.4f;
    float invuln_dash_counter_ = 0.0f;
    bool invuln_ = false;
    [SerializeField]
    float dash_damage_ = 1.0f;
    [SerializeField]
    float dash_radius_ = 2.0f;
    List<Collider2D> dash_enemy_list = new List<Collider2D>();
   
    // Start is called before the first frame update
    void Start()
    {
        am_ = FindObjectOfType<AudioManager>();
        rb2d_ = GetComponent<Rigidbody2D>();
        camera_ = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (ball_follow_ != null)
        {
            GameObject temp = (GameObject)Instantiate(ball_follow_, transform.position, Quaternion.identity);
            temp.GetComponent<BallFollowPlayer>().InitBall(ball_offset_, gameObject);
        }

        melee_ = new SemiCircleMelee(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //ResolveMeleeAttack();
        if (Time.time >= trail_active_time_)
        {
            GetComponent<TrailRenderer>().emitting = false;
        }
        else
            GetComponent<TrailRenderer>().emitting = true;
    }

    private void FixedUpdate()
    {
        //PlayerFacingDirection();
        UpdateFacingDirection();
        PlayerMovement();

        DashDamage();
    }

    public void DashDamage()
    {
        if (invuln_)
        {
            Collider2D[] temp_cols_ = Physics2D.OverlapCircleAll(transform.position, dash_damage_);
            foreach (Collider2D c in temp_cols_)
            {
                if (!dash_enemy_list.Contains(c))
                {
                    if (c.gameObject.GetComponent<HealthComponent>() != null && c.gameObject.layer == 14) {
                        c.gameObject.GetComponent<HealthComponent>().TakeDamage(1);
                    }
                    dash_enemy_list.Add(c);
                }
            }
        }
    }

    float melee_timer_;

    void ResolveMeleeAttack()
    {
        if (strike_zone_.activeInHierarchy)
        {
            melee_timer_ -= Time.deltaTime;
        }
        if (melee_timer_ <= 0)
        {
            strike_zone_.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            InitiateMeleeAttack();
        }
    }

    void InitiateMeleeAttack()
    {
        strike_zone_.SetActive(true);
        melee_timer_ = attack_duration_;
    }

    void PlayerFacingDirection()
    {
        Vector2 mouseLocation = Input.mousePosition;
        mouseLocation = camera_.ScreenToWorldPoint(mouseLocation);
        float angle = AngleBetween(transform.position, mouseLocation);
        transform.rotation = Quaternion.Euler(0, 0, angle);
      
    }

    void UpdateFacingDirection()
    {
        Vector2 mouseLocation = Input.mousePosition;
        mouseLocation = camera_.ScreenToWorldPoint(mouseLocation);
        stored_angle_ = AngleBetween(transform.position, mouseLocation);
    }

    float AngleBetween(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y-b.y, a.x-b.x )*Mathf.Rad2Deg+90;
    }

    void PlayerMovement()
    {
        if (invuln_)
        {
            if (invuln_dash_counter_ < invuln_dash_)
            {
                invuln_dash_counter_ += Time.deltaTime;
            }
            else
            {
                invuln_ = false;
                invuln_dash_counter_ = 0.0f;
                // reactivate player (14) enemy (16) collision 
                Physics2D.IgnoreLayerCollision(14, 16, false);
                dash_enemy_list.Clear();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= next_dash_time)
        {
            //am_.PlaySound(dash_sound_);
            //rb2d_.AddForce(heading_ * (dash_strength * dash_duration_));
            GameObject temp2 = GameObject.Find("Dashbar");
            temp2.GetComponent<DashFill>().dashbar();
            rb2d_.velocity = (Vector3)((heading_) * dash_strength) * dash_duration_;
            next_dash_time = Time.time + dash_cooldown_;
            invincibility_start_time_ = Time.time;
            trail_active_time_ = Time.time + dash_duration_ / 3;
            GameObject temp = Instantiate(dash_particle_);
            temp.transform.position = transform.position;
            GetComponent<ForceGruntsAway>().PushAllEnemies();
            invuln_ = true;
            // deactivate player (14) enemy (16) collision
            Physics2D.IgnoreLayerCollision(14, 16, true);
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                heading_.y += player_acceleration_ * speed_modifier_;
            }
            if (Input.GetKey(KeyCode.S))
            {
                heading_.y -= player_acceleration_ * speed_modifier_;
            }
            if (Input.GetKey(KeyCode.A))
            {
                heading_.x -= player_acceleration_ * speed_modifier_;
            }
            if (Input.GetKey(KeyCode.D))
            {
                heading_.x += player_acceleration_* speed_modifier_;
            }
        }
        heading_.x = heading_.x - ((1 - player_decceleration_) * heading_.x);
        heading_.y = heading_.y - ((1 - player_decceleration_) * heading_.y);
       
        //rb2d_.velocity = heading_;
        rb2d_.AddForce(heading_, ForceMode2D.Force);
    }

    public float GetAcceleration()
    {
        return player_acceleration_;
    }

    public void SetAcceleration(float acceleration_)
    {
        player_acceleration_ = acceleration_;
    }

    public bool isInvincible()
    {
        if (Time.time - invincibility_start_time_ > invincibility_time_ * Time.deltaTime)
        {
            return false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            return true;
        }
    }
}
