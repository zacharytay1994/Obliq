using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GF;

public class SectopodStates 
{
   
}
public class SectopodIdleState : State
{
    GameObject closest_obj;

    Vector2 random_wander;
    bool first_wander = true;
    public override void Enter(GameObject owner)
    {
        
    }
    public override void Execute(GameObject owner)
    {
        GameObject objective = GameObject.Find("Bomb");
        GC<RaycastAttack>(owner).RemoveLine(owner);
        closest_obj = GC<Entity>(owner).world_handler_reference_.GetNearestGoodGuy(owner.transform.position);
        
        if (GC<Entity>(owner).health_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodDeadState());
        }
        if(objective != null)
        {
            float numbe = (closest_obj.transform.position - objective.transform.position).magnitude;
            float number2 = GC<Entity>(owner).GetTrueRange();
           
            if (((Vector2)closest_obj.transform.position - (Vector2)objective.transform.position).magnitude < GC<Entity>(owner).GetTrueRange())// if within range
            {
                GC<Sectopod>(owner).target_reference_ = closest_obj;
                GC<ObliqPathfinding>(owner).target_ = closest_obj.transform.position;
                GC<Entity>(owner).statemachine_.ChangeState(new SectopodMoveState());
            }
            else if (GC<ObliqPathfinding>(owner).reached_end_path_ || first_wander == true)
            {
                first_wander = false;
                random_wander = new Vector2(Random.Range(objective.transform.position.x - GC<Entity>(owner).GetTrueRange(),
                    objective.transform.position.x + GC<Entity>(owner).GetTrueRange()), Random.Range(objective.transform.position.y - GC<Entity>(owner).GetTrueRange(),
                    objective.transform.position.y + GC<Entity>(owner).GetTrueRange()));

                GC<ObliqPathfinding>(owner).target_ = random_wander;
                GC<ObliqPathfinding>(owner).StartPath(GC<ObliqPathfinding>(owner).target_);

            }
        }
        else
        {
            if (((Vector2)closest_obj.transform.position - (Vector2)owner.transform.position).magnitude < GC<Entity>(owner).GetTrueRange())// if within range
            {
                GC<Sectopod>(owner).target_reference_ = closest_obj;
                GC<ObliqPathfinding>(owner).target_ = closest_obj.transform.position;
                GC<Entity>(owner).statemachine_.ChangeState(new SectopodMoveState());
            }
            else if (GC<ObliqPathfinding>(owner).reached_end_path_ || first_wander == true)
            {
                first_wander = false;
                random_wander = new Vector2(Random.Range(owner.transform.position.x - GC<Entity>(owner).GetTrueRange(),
                    owner.transform.position.x + GC<Entity>(owner).GetTrueRange()), Random.Range(owner.transform.position.y - GC<Entity>(owner).GetTrueRange(),
                    owner.transform.position.y + GC<Entity>(owner).GetTrueRange()));

                GC<ObliqPathfinding>(owner).target_ = random_wander;
                GC<ObliqPathfinding>(owner).StartPath(GC<ObliqPathfinding>(owner).target_);

            }
        }
            
        
    }
        
      

    
    public override void Exit(GameObject owner)
    {
    }
}
public class SectopodMoveState : State
{
    float movementBuffer = 1.5f;
    public override void Enter(GameObject owner)
    {
        owner.GetComponent<ObliqPathfinding>().StopPath();
        owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
    }
    public override void Execute(GameObject owner)
    {
        GameObject objective = GameObject.Find("Bomb");
        Debug.Log("Sectopod Moving");
        GC<RaycastAttack>(owner).Attack(owner, GC<Sectopod>(owner).target_reference_);
        
        if (GC<Entity>(owner).health_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodDeadState());
        }
        if (((Vector2)GC<ObliqPathfinding>(owner).target_ - (Vector2)GC<Sectopod>(owner).target_reference_.transform.position).magnitude > movementBuffer) //magic buffer value
        {
            // to move towards a point, buffer is to make the pathfinding less sensitive 
            GC<ObliqPathfinding>(owner).target_ = GC<Sectopod>(owner).target_reference_.transform.position;
            owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
        }
        if (((Vector2)GC<Sectopod>(owner).target_reference_.transform.position - (Vector2)owner.transform.position).magnitude < GC<Entity>(owner).attack_range_)
        //in range to attack
        {
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodAttackState());
        }
        if(objective != null){
            if (((Vector2)GC<Sectopod>(owner).target_reference_.transform.position - (Vector2)objective.transform.position).magnitude > GC<Entity>(owner).GetTrueRange())
            {
                //target out of range 
                owner.GetComponent<ObliqPathfinding>().StopPath();
                GC<Entity>(owner).statemachine_.ChangeState(new SectopodIdleState());
            }
        }
        else
        {
            if (((Vector2)GC<Sectopod>(owner).target_reference_.transform.position - (Vector2)owner.transform.position).magnitude > GC<Entity>(owner).GetTrueRange())
            {
                //target out of range 
                owner.GetComponent<ObliqPathfinding>().StopPath();
                GC<Entity>(owner).statemachine_.ChangeState(new SectopodIdleState());
            }
        }
       
    }
    public override void Exit(GameObject owner)
    {

    }
}

public class SectopodAttackState : State
{
    float attack_rate; // amt of time to charge

    float next_damage_time;
   
    bool is_charging;
    bool laser_aimed;
    DamagePopup damage_popup = GameObject.Find("World").GetComponent<DamagePopup>();
    public override void Enter(GameObject owner)
    {
        Debug.Log("Sectopod Attack state");
        is_charging = true;
        laser_aimed = false;
        
        if (next_damage_time == 0)
        {
            next_damage_time = Time.time + attack_rate;

        }
        owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
      
        
    }
    public override void Execute(GameObject owner)       
    {
        attack_rate = owner.GetComponent<RaycastAttack>().attack_rate_;
        GameObject objective = GameObject.Find("Bomb");
        GC<ObliqPathfinding>(owner).target_ = GC<Sectopod>(owner).target_reference_.transform.position;
        if (GC<Entity>(owner).health_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodDeadState());
        }
        if ((GC<Sectopod>(owner).target_reference_.transform.position - owner.transform.position).magnitude
            < GC<Entity>(owner).attack_range_ || laser_aimed) 
        {
            GC<RaycastAttack>(owner).Attack(owner, GC<Sectopod>(owner).target_reference_);
            
            if (Time.time >=  GC<RaycastAttack> (owner).next_line_thicken_time && !is_charging)//start charging
            {
                GC<RaycastAttack>(owner).next_line_contract_time = Time.time + GC<RaycastAttack>(owner).line_contract_rate_;
                GC<RaycastAttack>(owner).LineContract(GC<LineRenderer>(owner), GC<RaycastAttack>(owner).line_contract_increment_);
                if (GC<LineRenderer>(owner).startWidth <= GC<RaycastAttack>(owner).initial_width_)
                {
                    GC<LineRenderer>(owner).startWidth = GC<RaycastAttack>(owner).initial_width_;
                    GC<LineRenderer>(owner).endWidth = GC<RaycastAttack>(owner).initial_width_;
                    

                    is_charging = true;
                }
            }
            else if (Time.time >= GC<RaycastAttack>(owner).next_line_thicken_time && is_charging) // line thickens up till attack 
            {
                GC<RaycastAttack>(owner).next_line_thicken_time = Time.time + GC<RaycastAttack>(owner).line_thicken_rate_;
                GC<RaycastAttack>(owner).LineExpand(GC<LineRenderer>(owner), GC<RaycastAttack>(owner).line_thicken_increment_);
             
                laser_aimed = true;
            }
            if (Time.time >= next_damage_time && laser_aimed)
            {
                next_damage_time = Time.time + attack_rate;
                
                if (GC<RaycastAttack>(owner).Attack(owner, GC<Sectopod>(owner).target_reference_) != null)
                {
                    /*GC<Sectopod>(owner).target_reference_.GetComponent<HealthComponent>().TakeDamage(1);
                    Debug.Log(GC<Sectopod>(owner).target_reference_.GetComponent<HealthComponent>().currentHp_);*/
                    GameObject object_hit = GC<RaycastAttack>(owner).Attack(owner, GC<Sectopod>(owner).target_reference_);
                    if(object_hit.GetComponent<BloodEffect>() != null)
                    {
                        object_hit.GetComponent<BloodEffect>().DrawBlood(object_hit.transform.position - owner.transform.position);
                    }

                    //object_hit.GetComponent<HealthComponent>().TakeDamage(owner.GetComponent<RaycastAttack>().damage_);
                    //Instantiate explosion bullet here
                    // owner.GetComponent<InitProjSpawner>().GetWeapon().GetComponent<ImAProjectile>().FireForSetTime(0.11f);
                    owner.GetComponent<Sectopod>().FireBullet();
                    
                }

                is_charging = false;
                laser_aimed = false;
            }

            owner.GetComponent<ObliqPathfinding>().StopPath();
        }
        else if (objective != null)
        {       
        if ((GC<Sectopod>(owner).target_reference_.transform.position - objective.transform.position).magnitude > GC<Entity>(owner).GetTrueRange()
          )
            {
                owner.GetComponent<ObliqPathfinding>().StopPath();
                GC<RaycastAttack>(owner).RemoveLine(owner);
                GC<Entity>(owner).statemachine_.ChangeState(new SectopodIdleState());
            }
        }
        else if (!laser_aimed)
        {
            GC<LineRenderer>(owner).startWidth = GC<RaycastAttack>(owner).initial_width_;
            GC<LineRenderer>(owner).endWidth = GC<RaycastAttack>(owner).initial_width_;
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodMoveState());
        }
        else if ((GC<Sectopod>(owner).target_reference_.transform.position - owner.transform.position).magnitude > GC<Entity>(owner).GetTrueRange() &&
            objective == null)
        {
            owner.GetComponent<ObliqPathfinding>().StopPath();
            GC<RaycastAttack>(owner).RemoveLine(owner);
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodIdleState());
        }
       

               
    }
    public override void Exit(GameObject owner)
    {
    }
}

public class SectopodDeadState : State
{
    public override void Enter(GameObject owner)
    {
        GC<Rigidbody2D>(owner).velocity = Vector2.zero;
        GC<Rigidbody2D>(owner).angularVelocity = 0;
    }
    public override void Execute(GameObject owner)
    {
        //insert death anim
        //destroy obj  
        Debug.Log("Sectopod Dead");
        Object.Destroy(owner, 0);
    }
    public override void Exit(GameObject owner)
    {

    }
}
