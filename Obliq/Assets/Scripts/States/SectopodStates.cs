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
           
        closest_obj = GC<Entity>(owner).world_handler_reference_.GetNearestGoodGuy(owner.transform.position);
        if (GC<Entity>(owner).health_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new ChargerDeadState());
        }
        if ((closest_obj.transform.position - objective.transform.position).magnitude < GC<Entity>(owner).GetTrueRange())// if within range
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
        // Debug.Log("Sectopod Moving");
        if (GC<Entity>(owner).health_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new ChargerDeadState());
        }
        if ((GC<ObliqPathfinding>(owner).target_ - (Vector2)GC<Sectopod>(owner).target_reference_.transform.position).magnitude > movementBuffer) //magic buffer value
        {
            GC<ObliqPathfinding>(owner).target_ = GC<Sectopod>(owner).target_reference_.transform.position;
            owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
        }
        if ((GC<Sectopod>(owner).target_reference_.transform.position - owner.transform.position).magnitude < 5.0f) //temp magic no attack range
        {
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodAttackState());
        }
        if ((GC<Sectopod>(owner).target_reference_.transform.position - objective.transform.position).magnitude > GC<Entity>(owner).GetTrueRange())
        {
            owner.GetComponent<ObliqPathfinding>().StopPath();
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodIdleState());
        }
    }
    public override void Exit(GameObject owner)
    {

    }
}

public class SectopodAttackState : State
{
    float attack_rate = 1.0f;
    float next_damage_time = 0.0f;
    public override void Enter(GameObject owner)
    {
        Debug.Log("Sectopod Attack state");
        owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);

    }
    public override void Execute(GameObject owner)       
    {
        GameObject objective = GameObject.Find("Bomb");
        GC<ObliqPathfinding>(owner).target_ = GC<Sectopod>(owner).target_reference_.transform.position;
        if (GC<Entity>(owner).health_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new ChargerDeadState());
        }
        if ((GC<Sectopod>(owner).target_reference_.transform.position - owner.transform.position).magnitude < 5f) //temp magic no (attack_range)
        {
            if(Time.time >= next_damage_time)
            {
                next_damage_time = Time.time + attack_rate;
                GC<Sectopod>(owner).target_reference_.GetComponent<Entity>().TakeDamage(1);//temporary hit scan should be projectile
                Debug.Log(GC<Sectopod>(owner).target_reference_.GetComponent<Entity>().health_);
            }
            owner.GetComponent<ObliqPathfinding>().StopPath();
        }
        else if ((GC<Sectopod>(owner).target_reference_.transform.position - objective.transform.position).magnitude > GC<Entity>(owner).GetTrueRange())
        {
            owner.GetComponent<ObliqPathfinding>().StopPath();
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodIdleState());
        }
        else
        {
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodMoveState());
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


        Object.Destroy(owner, 0);


    }
    public override void Exit(GameObject owner)
    {

    }
}
