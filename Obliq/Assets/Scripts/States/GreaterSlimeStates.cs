using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GF;

public class GreaterSlimeStates
{

}

public class GreaterSlimeMoveState : State
{
    GameObject closest_obj;

    Vector2 compare_vec;
    public override void Enter(GameObject owner)
    {
        closest_obj = owner.GetComponent<Entity>().world_handler_reference_.GetNearestGoodGuy(owner.transform.position);
        owner.GetComponent<ObliqPathfinding>().target_ = closest_obj.transform.position;

        GC<GreaterSlime>(owner).target_reference_ = closest_obj;
        Debug.Log(closest_obj);
    }

    public override void Execute(GameObject owner)
    {
        if ((GC<ObliqPathfinding>(owner).target_ - (Vector2)GC<GreaterSlime>(owner).target_reference_.transform.position).magnitude > 1.5)
        {
            GC<ObliqPathfinding>(owner).target_ = GC<GreaterSlime>(owner).target_reference_.transform.position;
            owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
        }
        if ((GC<GreaterSlime>(owner).target_reference_.transform.position - owner.transform.position).magnitude < 2.0f) //temp magic no attack range
        {
            GC<Entity>(owner).statemachine_.ChangeState(new GreaterSlimeAttackState());
        }
        if ((GC<GreaterSlime>(owner).target_reference_.transform.position - owner.transform.position).magnitude > GC<Entity>(owner).GetTrueRange())
        {
            owner.GetComponent<ObliqPathfinding>().StopPath();
            GC<Entity>(owner).statemachine_.ChangeState(new GreaterSlimeIdleState());
        }
    }

    public override void Exit(GameObject owner)
    {

    }
}

public class GreaterSlimeAttackState : State
{
    float attack_rate = 1.5f;
    float next_damage_time = 0.0f;
    public override void Enter(GameObject owner) { }
    public override void Execute(GameObject owner) {
        if ((GC<GreaterSlime>(owner).target_reference_.transform.position - owner.transform.position).magnitude > 1.5f)
        {
            GC<Entity>(owner).statemachine_.ChangeState(new GreaterSlimeMoveState());
        }
        if (Time.time >= next_damage_time)
        {
            next_damage_time = Time.time + attack_rate;
            GC<GreaterSlime>(owner).target_reference_.GetComponent<Entity>().TakeDamage(1.5f);
            Debug.Log("SLime" + GC<GreaterSlime>(owner).target_reference_.GetComponent<Entity>().health_);
            owner.GetComponent<ObliqPathfinding>().StopPath();
        }


    }
    public override void Exit(GameObject owner) { }
}

public class GreaterSlimeIdleState : State
{
    GameObject closest_obj;
    public override void Enter(GameObject owner)
    {

    }

    public override void Execute(GameObject owner)
    {
        closest_obj = GC<Entity>(owner).world_handler_reference_.GetNearestGoodGuy(owner.transform.position);
        if ((closest_obj.transform.position - owner.transform.position).magnitude < GC<Entity>(owner).GetTrueRange()) ;
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new GreaterSlimeMoveState());
        }
    }

    public override void Exit(GameObject owner) { }
}