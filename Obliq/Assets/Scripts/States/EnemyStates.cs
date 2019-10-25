using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates
{
   // nothing here
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
        //// get distance set reachable distance
        //if ((closest_good_guy_position - (Vector2)owner.transform.position).magnitude > owner.GetComponent<ObliqEnemy>().move_distance_) {
        //    within_range_ = (closest_good_guy_position - (Vector2)owner.transform.position).magnitude - owner.GetComponent<ObliqEnemy>().move_distance_;
        //}
        //// tentative distance
        //Vector2 movement_vector = (closest_good_guy_position - (Vector2)owner.transform.position).normalized * owner.GetComponent<ObliqEnemy>().move_distance_;
        //// set target
        //owner.GetComponent<ObliqPathfinding>().target_ = (Vector2)owner.transform.position + movement_vector;
        //owner.GetComponent<ObliqPathfinding>().StartPath(closest_good_guy_position);
        owner.GetComponent<Rigidbody2D>().AddForce(to_add * 20000);
        compare_vec = (Vector2)owner.transform.position - closest_good_guy_position;
            
    }
    public override void Execute(GameObject owner)
    {
        //// if reached target, change state
        //if (owner.GetComponent<ObliqPathfinding>().reached_end_path_)
        //{
        //    // if remaining distance is more then attack range, end turn
        //    if (within_range_ > owner.GetComponent<Entity>().attack_range_)
        //    {
        //        owner.GetComponent<Entity>().has_moved_ = true;
        //        owner.GetComponent<Entity>().statemachine_.ChangeState(new SomeDefault());
        //    }
        //    // if attackable
        //    else
        //    {
        //        owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerAttackState());
        //    }

        //}
        //if (owner.GetComponent<ObliqPathfinding>().reached_end_path_)
        //{
        //    owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState());
        //}
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
    public override void Enter(GameObject owner)
    {
        // 
    }
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
