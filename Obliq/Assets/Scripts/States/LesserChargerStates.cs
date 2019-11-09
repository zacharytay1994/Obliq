using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GF;

public class LesserChargerStates
{
    // smol charge 
}

public class LCIdle : State
{
    public override void Enter(GameObject owner)
    {
    }
    public override void Execute(GameObject owner)
    {
        Debug.Log("LC Entered idle state.");
        Debug.Log("test");
        // Get nearest good guy
        GameObject nearest_good_guy_ = GC<Entity>(owner).world_handler_reference_.GetNearestGoodGuy((Vector2)owner.transform.position);
            // if found
            if (nearest_good_guy_ != null)
            {
                // set target to nearest_good_guy
                GC<LesserCharger>(owner).target_reference_ = nearest_good_guy_;
                if (GC<Entity>(owner).health_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
                {
                    GC<Entity>(owner).statemachine_.ChangeState(new LCDead());
                }
            // if within range
                if ((GC<LesserCharger>(owner).target_reference_.transform.position - owner.transform.position).magnitude < 1.5f)//temp magic no for attack range
                {
                    // go into attack state
                    ChangeState(owner, new LCAttack());
                }
                // else go into move state
                else
                {
                    ChangeState(owner, new LCMove());
                }
            }
    }
    public override void Exit(GameObject owner) { }
}
public class LCMove : State
{
    public override void Enter(GameObject owner)
    {
        // get distance between target and this
        GC<ObliqPathfinding>(owner).target_ = GC<LesserCharger>(owner).target_reference_.transform.position;
        owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
        Debug.Log("LC Entered Move");
    }
    public override void Execute(GameObject owner)
    {
        if (GC<Entity>(owner).health_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new LCDead());
        }
        if ((GC<ObliqPathfinding>(owner).target_ - (Vector2)GC<LesserCharger>(owner).target_reference_.transform.position).magnitude > 5f)
        {
            GC<ObliqPathfinding>(owner).target_ = GC<LesserCharger>(owner).target_reference_.transform.position;
            owner.GetComponent<ObliqPathfinding>().StartPath(owner.GetComponent<ObliqPathfinding>().target_);
        }
        if ((GC<LesserCharger>(owner).target_reference_.transform.position - owner.transform.position).magnitude < 5f)//temp magic no (attack range)
        {
            // go into attack state
           
            ChangeState(owner, new LCAttack());
        }
     
    }
    public override void Exit(GameObject owner) { }
}

public class LCAttack : State
{
    float attack_rate = 2.0f;
    float next_damage_time = 0.0f;
    public override void Enter(GameObject owner) {
      
    }
    public override void Execute(GameObject owner) {
        Debug.Log("LC is attacking");
        if (GC<Entity>(owner).health_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new LCDead());
        }
        if ((GC<LesserCharger>(owner).target_reference_.transform.position - owner.transform.position).magnitude > 5f)
        {
            GC<Entity>(owner).statemachine_.ChangeState(new LCMove());
        }
        if (Time.time >= next_damage_time)
        {
            next_damage_time = Time.time + attack_rate;
            Debug.Log(GC<Entity>(GC<LesserCharger>(owner).target_reference_).health_);
            GC<Entity>(GC<LesserCharger>(owner).target_reference_).TakeDamage(1);
           
            owner.GetComponent<ObliqPathfinding>().StopPath();
        }

        
    }
    public override void Exit(GameObject owner) { }
}
public class LCDead : State
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
