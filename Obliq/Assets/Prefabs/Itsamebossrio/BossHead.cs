using System.Collections;
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
    
    [SerializeField]
    float laser_attack_speed_;
    [SerializeField]
    float overall_laser_attack_speed_;
    [SerializeField]
    float laser_speed_;
    [SerializeField]
    GameObject laser_object_;

    float overall_laser_timer_;
    float laser_timer_;

    [SerializeField]
    float range_of_cone;
    [SerializeField]
    float cone_attack_speed;
    [SerializeField]
    float overall_cone_duration;
    
    
    [SerializeField]
    GameObject cone_lines_;
    float overall_cone_timer;
    float cone_timer;
    
    

    float charge_count = 0;
    float laser_count_ = 0;

    Vector2 target_cone;
    bool can_get_pos = true;

    bool charging;
    bool cone_;
    bool hover_;

    float dotprod;
   

    LineRenderer lr1;
    LineRenderer lr2;

    HealthComponent hc_;
    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.Find("Player");
        rb_ = GetComponent<Rigidbody2D>();
        hc_ = GetComponent<HealthComponent>();


        //draw_come_timer = 0;
        cone_timer = 0;
        laser_timer_ = 0;
        laser_count_ = 0;
        GameObject temp1 = GameObject.Instantiate(cone_lines_, transform);
        lr1 = temp1.GetComponent<LineRenderer>();

        GameObject temp2 = Instantiate(cone_lines_, transform);
        lr2 = temp2.GetComponent<LineRenderer>();
        cone_ = false;
        charging = true;
        hover_ = false;
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
        
        if (charging)
        {
            can_get_pos = true;
            if (inplace_charge_)
            {
                ExecuteCharge();
                Debug.Log("Charging");

            }
        }
        else if (cone_)
        {
            //float angle = Mathf.Acos(dotprod / dirFromPlayerToBoss.magnitude * 
            //    gameObject.transform.TransformDirection(Vector3.forward).magnitude);
            //dotprod = dirFromPlayerToBoss.magnitude * gameObject.transform.TransformDirection(Vector3.forward).magnitude *
            //     Mathf.Cos(angle);
          
            dotprod = Vector3.Dot(player_.transform.forward, gameObject.transform.forward);
            Debug.Log(dotprod + "Dot");

            if (dotprod > 0.9)
            {
               
                if (charge_count > 1 && activate_)
                {
                    ConeAttack();
                }
            }
                     
        }
        else if (hover_)
        {
            can_get_pos = true;
            if (inplace_)
            {
                InplaceHover();
                FireLasers();
               
                Debug.Log("Firing lasers");                
            }
            
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
    void DrawCone(Vector2 from,Vector2 to, LineRenderer lr)
    {                 
        lr.SetPosition(0, from);
        lr.SetPosition(1, Vector3.Lerp(from, to, 0.25f));                                       
    }
    void ResetLine(LineRenderer lr)
    {
        lr.SetPosition(1, lr.GetPosition(0));
    }
    public void ConeAttack()
    {
        if(overall_cone_timer <= 0)
        {
            target_cone = player_.transform.position;
        }
        overall_cone_timer += Time.deltaTime;
       
        if (overall_cone_timer <= overall_cone_duration)
        {
            cone_timer += Time.deltaTime;
        }

        if (cone_timer > cone_attack_speed)
        {
            Debug.Log("cone");

            //gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GF.AngleBetween(new Vector2(0.0f, 1.0f), (Vector2)player_.transform.position - (Vector2)gameObject.transform.position));
            //DrawCone(gameObject.transform.position, new Vector2(player_.transform.position.x - 5, player_.transform.position.y -5), lr1);
            //DrawCone(gameObject.transform.position, new Vector2(player_.transform.position.x + 5, player_.transform.position.y + 5), lr2);
            

           //Vector2 target = new Vector2(direction.x + Random.Range(-range_of_cone, range_of_cone), direction.y + Random.Range(-range_of_cone, range_of_cone)).normalized;
           // Vector2 target = new Vector2(direction.x , direction.y);
           // Debug.Log(target + "Position");

            GameObject bullet = Instantiate(laser_object_, (Vector2)gameObject.transform.position + ((gameObject.GetComponent<CircleCollider2D>().radius + 2.0f) *
                ((Vector2)target_cone - (Vector2)gameObject.transform.position).normalized), Quaternion.identity);
            Vector2 final_dir = new Vector2((target_cone.x - gameObject.transform.position.x) + Random.Range(-range_of_cone, range_of_cone), 
                (target_cone.y - gameObject.transform.position.y) + Random.Range(-range_of_cone, range_of_cone));
            Vector2 max_dir = new Vector2((target_cone.x - gameObject.transform.position.x) + -range_of_cone,
                (target_cone.y - gameObject.transform.position.y) + -range_of_cone);
            Vector2 min_dir = new Vector2((target_cone.x - gameObject.transform.position.x) + range_of_cone,
                (target_cone.y - gameObject.transform.position.y) + range_of_cone);
            lr1.SetPosition(0, gameObject.transform.position);
            lr1.SetPosition(1, Vector3.Lerp(gameObject.transform.position, max_dir.normalized, 0.5f));    
            bullet.GetComponent<Rigidbody2D>().velocity =(final_dir).normalized * laser_speed_;
            cone_timer = 0;
        }
        if (overall_cone_timer >= overall_laser_attack_speed_)
        {
            overall_cone_timer = 0;
            charge_count = 0;
            cone_ = false;
            charging = true;
            can_get_pos = true;
            
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
    public void FireLasers()
    {
        overall_laser_timer_ += Time.deltaTime;
        if(overall_laser_timer_ <= overall_laser_attack_speed_)
        {
            laser_timer_ += Time.deltaTime;
        }
        gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GF.AngleBetween(new Vector2(0.0f, 1.0f), (Vector2)player_.transform.position - (Vector2)gameObject.transform.position));
        if (laser_timer_ > laser_attack_speed_)
        {
           
            GameObject bullet = Instantiate(laser_object_, (Vector2)gameObject.transform.position + ((gameObject.GetComponent<CircleCollider2D>().radius + 2.0f) *
            ((Vector2)player_.transform.position - (Vector2)gameObject.transform.position).normalized), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = ((Vector2)player_.transform.position - (Vector2)gameObject.transform.position).normalized * laser_speed_;
            laser_timer_ = 0;
        }

        if (overall_laser_timer_ >= overall_laser_attack_speed_)
        {
            hover_ = false;
            charging = true;
            overall_laser_timer_ = 0;
        }

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
            charge_count++;
            if(charge_count > 1)
            {
                
                cone_ = true;
            }
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
        charging = false;
        hover_ = true;
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
