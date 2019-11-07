using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Debug.Log(closest_obj);
    }

    public override void Execute(GameObject owner)
    {
        owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
    }

    public override void Exit(GameObject owner)
    {

    }
}

public class GreaterSlimeAttackState : State
{
    public override void Enter(GameObject owner) { }
    public override void Execute(GameObject owner) { }
    public override void Exit(GameObject owner) { }
}

public class GreaterSlimeIdleState : State
{
    public override void Enter(GameObject owner)
    {

    }

    public override void Execute(GameObject owner)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new GreaterSlimeMoveState());
        }
    }

    public override void Exit(GameObject owner) { }
}