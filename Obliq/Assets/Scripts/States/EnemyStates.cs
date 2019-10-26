using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates
{
   // nothing here, or is there
}

// -------------------------------------------------------//
// CHARGER STATES
// -------------------------------------------------------//
public class ChargerMoveState : State
{
    Vector2 closest_good_guy_position;
    Vector2 compare_vec;
    public override void Enter(GameObject owner)
    {
        owner.GetComponent<ObliqEnemy>().target_reference_ = GameObject.Find("World").GetComponent<TurnManager>().GetRandomGoodGuy();
        // if target was not found 
        if (owner.GetComponent<ObliqEnemy>().target_reference_ == null)
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState());
            return;
        }
        // find position behind target
        Vector2 to_add = (owner.GetComponent<ObliqEnemy>().target_reference_.transform.position - owner.transform.position).normalized;
        closest_good_guy_position =
            (Vector2)owner.GetComponent<ObliqEnemy>().target_reference_.transform.position + (to_add * 2.0f); // temp magic number (how far behind target)
        // move charger to position
        owner.GetComponent<Rigidbody2D>().AddForce(to_add * 20000);
        compare_vec = (Vector2)owner.transform.position - closest_good_guy_position;
            
    }
    public override void Execute(GameObject owner)
    {
        // if overshoot the target
        if (Vector2.Dot(compare_vec, (Vector2)owner.transform.position - closest_good_guy_position) < 0)
        {
            owner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            owner.GetComponent<Rigidbody2D>().angularVelocity = 0;
            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState());
        }
    }
    public override void Exit(GameObject owner) { }
}

public class ChargerAttackState : State
{
    public override void Enter(GameObject owner) { }
    public override void Execute(GameObject owner) { }
    public override void Exit(GameObject owner) { }
}

public class ChargerIdleState : State
{
    public override void Enter(GameObject owner) { }
    public override void Execute(GameObject owner)
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerMoveState());
        }
    }
    public override void Exit(GameObject owner) { }
}
