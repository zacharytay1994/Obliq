using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GF;
public class ChargerStates
{
   // nothing here, or is there
}

public class ChargerMoveState : State
{
    Vector2 closest_good_guy_position;
    Vector2 compare_vec;
    public override void Enter(GameObject owner)
    {
        owner.GetComponent<Charger>().target_reference_ = GameObject.Find("World").GetComponent<WorldHandler>().GetRandomGoodGuy();
        // if target was not found 
        if (owner.GetComponent<Charger>().target_reference_ == null)
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState());
            return;
        }
        // find position behind target
        Vector2 to_add = (owner.GetComponent<Charger>().target_reference_.transform.position - owner.transform.position).normalized;
        closest_good_guy_position =
            (Vector2)owner.GetComponent<Charger>().target_reference_.transform.position + (to_add * 2.0f); // temp magic number (how far behind target)
        // move charger to position
        owner.GetComponent<Rigidbody2D>().AddForce(to_add * 10000); //* 40);
        compare_vec = (Vector2)owner.transform.position - closest_good_guy_position;
            
    }
    public override void Execute(GameObject owner)
    {
        float charge_timer = Time.time;
        Debug.Log("Charger Move");
        // if overshoot the target
        if (Vector2.Dot(compare_vec, (Vector2)owner.transform.position - closest_good_guy_position) < 0)
        {
            //owner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
           // owner.GetComponent<Rigidbody2D>().angularVelocity = 0;
            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState());
        }
        if (GC<Rigidbody2D>(owner).velocity == Vector2.zero || Time.time - charge_timer > 3.0f)
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState()); 
        }
       /* 
        owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState());*/
    }
    public override void Exit(GameObject owner) {

    }
}


public class ChargerAttackState : State
{
    public override void Enter(GameObject owner) { }
    public override void Execute(GameObject owner) { }
    public override void Exit(GameObject owner) { }
}

public class ChargerIdleState : State
{
    float charge_start = Time.time;
    public override void Enter(GameObject owner)
    {
      
    }
    public override void Execute(GameObject owner)
    {

        Debug.Log("Charger Idle");
       // Debug.Log(Time.time - charge_start);
        if (Time.time - charge_start >= 3.0f)
        {
           
            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerMoveState());
        }
        else
        {
           /* while (GC<Rigidbody2D>(owner).velocity != Vector2.zero && GC<Rigidbody2D>(owner).angularVelocity > 0)
            {
                Debug.Log("Charger slowing down");
                GC<Rigidbody2D>(owner).velocity -= (GC<Rigidbody2D>(owner).velocity - Vector2.zero)/2;
                GC<Rigidbody2D>(owner).angularVelocity -= 20;
            }*/
        }
        
    }
    public override void Exit(GameObject owner) { }
}