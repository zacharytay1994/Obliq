﻿using System.Collections;
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
   
    float charge_timer;
    public override void Enter(GameObject owner)
    {
         charge_timer = Time.time;
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
            (Vector2)owner.GetComponent<Charger>().target_reference_.transform.position + (to_add * 1.5f); // temp magic number (how far behind target)
        // move charger to position
        owner.GetComponent<Rigidbody2D>().AddForce(to_add * 20000); //* 40);
        owner.GetComponent<LineRenderer>().SetPosition(0, (Vector2)owner.transform.position);
       
        owner.GetComponent<LineRenderer>().SetPosition(1, closest_good_guy_position);
        compare_vec = (Vector2)owner.transform.position - closest_good_guy_position;
            
    }
    public override void Execute(GameObject owner)
    {

        Debug.Log("Charger Move");
        // if overshoot the target
        owner.GetComponent<LineRenderer>().SetPosition(0, (Vector2)owner.transform.position);
        if (GC<HealthComponent>(owner).currentHp_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new ChargerDeadState());
        }
        if (Vector2.Dot(compare_vec, (Vector2)owner.transform.position - (Vector2)closest_good_guy_position) < 0)
        {

            //owner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // owner.GetComponent<Rigidbody2D>().angularVelocity = 0;
            GC<Charger>(owner).SpawnLesserChargers();

            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState());
        }
        if (GC<Rigidbody2D>(owner).velocity == Vector2.zero || Time.time - charge_timer > 3.0f)
        {
            GC<Charger>(owner).SpawnLesserChargers();
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
        if(GameObject.Find("World").GetComponent<WorldHandler>().GetRandomGoodGuy() != null)
        {
            owner.GetComponent<Charger>().target_reference_ = GameObject.Find("World").GetComponent<WorldHandler>().GetRandomGoodGuy();
            Vector2 to_add = (owner.GetComponent<Charger>().target_reference_.transform.position - owner.transform.position).normalized;
            Vector2 closest_good_guy_position =
                (Vector2)owner.GetComponent<Charger>().target_reference_.transform.position + (to_add * 1.5f); // temp magic number (how far behind target)
            owner.GetComponent<LineRenderer>().SetPosition(0, (Vector2)owner.transform.position);
            owner.GetComponent<LineRenderer>().SetPosition(1, closest_good_guy_position);
        }
        else
        {
            owner.GetComponent<LineRenderer>().SetPosition(0, (Vector2)owner.transform.position);
            owner.GetComponent<LineRenderer>().SetPosition(1, (Vector2)owner.transform.position);
        }
        
        if (Time.time - charge_start >= 3.0f)//magic no
        {
           
                                                                                                          // move charger to position
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
        if (GC<HealthComponent>(owner).currentHp_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new ChargerDeadState());
        }

    }
    public override void Exit(GameObject owner) { }
}
public class ChargerDeadState : State
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
        Debug.Log("Charger dead");
            Object.Destroy(owner, 0);
    }
    public override void Exit(GameObject owner)
    {

    }
}
