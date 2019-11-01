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
        closest_obj = owner.GetComponent<Entity>().world_handler_reference_.GetNearestGoodGuy(owner.transform.position);
        owner.GetComponent<ObliqPathfinding>().target_ = closest_obj.transform.position;

        if ((closest_obj.transform.position - owner.transform.position).magnitude < GC<Entity>(owner).GetTrueRange())// if within range
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new SectopodAttackState());
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
        Debug.Log("Attack state");
    }
    public override void Execute(GameObject owner)
    {
    }
    public override void Exit(GameObject owner)
    {
    }
}