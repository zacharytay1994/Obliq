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
       
        if ((GC<LesserCharger>(owner).target_reference_.transform.position - owner.transform.position).magnitude < 1.5f)//temp magic no (attack range)
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
        Debug.Log("LC is attacking");
    }
    public override void Execute(GameObject owner) {   
        
        if (Time.time >= next_damage_time)
        {
            next_damage_time = Time.time + attack_rate;
            GC<LesserCharger>(owner).target_reference_.GetComponent<Entity>().TakeDamage(2);
            Debug.Log("LC DMG" + GC<LesserCharger>(owner).target_reference_.GetComponent<Entity>().health_);
            owner.GetComponent<ObliqPathfinding>().StopPath();
        }

        if((GC<LesserCharger>(owner).target_reference_.transform.position - owner.transform.position).magnitude > 1.5f){
            GC<Entity>(owner).statemachine_.ChangeState(new LCMove());
        }
    }
    public override void Exit(GameObject owner) { }
}