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
    public override void Enter(GameObject owner)
    {
    }
    public override void Execute(GameObject owner)
    {
        GameObject objective = GameObject.Find("Entities/Objective");
       // Debug.Log("sectopod idle");
        closest_obj = GC<Entity>(owner).world_handler_reference_.GetNearestGoodGuy(owner.transform.position);

        if ((closest_obj.transform.position - objective.transform.position).magnitude < GC<Entity>(owner).GetTrueRange())// if within range
        {
            GC<Sectopod>(owner).target_reference_ = closest_obj;
            GC<ObliqPathfinding>(owner).target_ = closest_obj.transform.position;
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodMoveState());
        }

    }
    public override void Exit(GameObject owner)
    {
    }
}
public class SectopodMoveState : State
{
    public override void Enter(GameObject owner)
    {
        owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
    }
    public override void Execute(GameObject owner)
    {
        GameObject objective = GameObject.Find("Entities/Objective");
        // Debug.Log("Sectopod Moving");
        if ((GC<ObliqPathfinding>(owner).target_ - (Vector2)GC<Sectopod>(owner).target_reference_.transform.position).magnitude > 1.5)
        {
            GC<ObliqPathfinding>(owner).target_ = GC<Sectopod>(owner).target_reference_.transform.position;
            owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
        }
        if ((GC<Sectopod>(owner).target_reference_.transform.position - owner.transform.position).magnitude < 2.0f) //temp magic no
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
        GameObject objective = GameObject.Find("Entities/Objective");
        GC<ObliqPathfinding>(owner).target_ = GC<Sectopod>(owner).target_reference_.transform.position;
        
        if ((GC<Sectopod>(owner).target_reference_.transform.position - owner.transform.position).magnitude < 1.5f) //temp magic no (attack_range)
        {
            if(Time.time >= next_damage_time)
            {
                next_damage_time = Time.time + attack_rate;
                GC<Sectopod>(owner).target_reference_.GetComponent<Entity>().TakeDamage(1);//temporary hit scan should be projectile
               // Debug.Log(GC<Sectopod>(owner).target_reference_.GetComponent<Entity>().health_ + "SECTOPOD DMG");
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
