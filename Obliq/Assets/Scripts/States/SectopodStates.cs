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
        closest_obj = GC<Entity>(owner).world_handler_reference_.GetNearestGoodGuy(owner.transform.position);

        if ((closest_obj.transform.position - owner.transform.position).magnitude < GC<Entity>(owner).GetTrueRange())// if within range
        {
            GC<Sectopod>(owner).target_reference_ = closest_obj;
            GC<ObliqPathfinding>(owner).target_ = closest_obj.transform.position;
            GC<Entity>(owner).statemachine_.ChangeState(new SectopodAttackState());
        }

    }
    public override void Exit(GameObject owner)
    {
    }
}
public class SectopodAttackState : State
{
    public override void Enter(GameObject owner)
    {
        Debug.Log("Sectopod Attack state");
    }
    public override void Execute(GameObject owner)
    {
        GC<ObliqPathfinding>(owner).target_ = GC<Sectopod>(owner).target_reference_.transform.position;

        if ((GC<Sectopod>(owner).target_reference_.transform.position - owner.transform.position).magnitude < 2.0f) //temp magic no
        {
            GC<Sectopod>(owner).target_reference_.GetComponent<Entity>().TakeDamage(1);//temporary hit scan should be projectile
            Debug.Log(GC<Sectopod>(owner).target_reference_.GetComponent<Entity>().health_);
            owner.GetComponent<ObliqPathfinding>().StopPath();
        }
        else
        {
          
            owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
        }
        if ((GC<Sectopod>(owner).target_reference_.transform.position - owner.transform.position).magnitude > GC<Entity>(owner).GetTrueRange())
        {
            ChangeState(owner, new SectopodIdleState());
        }
    }
    public override void Exit(GameObject owner)
    {
    }
}
