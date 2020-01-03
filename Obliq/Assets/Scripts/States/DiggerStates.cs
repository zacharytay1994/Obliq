using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerStates 
{
    

}
public class DiggerIdleState : State
{
    public override void Enter(GameObject owner)
    {
        
    }
    public override void Execute(GameObject owner)
    {
        if(owner.GetComponent<Digger>().player != null)
        {
            if((owner.transform.position - owner.GetComponent<Digger>().player.transform.position).magnitude
                < owner.GetComponent<Entity>().detection_range_)
            {
                owner.GetComponent<BoxCollider2D>().enabled = false;
                
                owner.GetComponent<Entity>().statemachine_.ChangeState(new DiggerMoveState());
            }
        }
    }
    public override void Exit(GameObject owner)
    {

    }
}
public class DiggerMoveState : State
{
    public override void Enter(GameObject owner)
    {
        owner.GetComponent<ObliqPathfinding>().target_ = owner.GetComponent<Digger>().player.transform.position;
        Vector2 to_add = (owner.GetComponent<Digger>().player.transform.position - owner.transform.position).normalized;
        owner.GetComponent<ObliqPathfinding>().StartPath((Vector2)owner.GetComponent<Digger>().player.transform.position +
            (to_add * 1.5f));
    }
    public override void Execute(GameObject owner)
    {
        if (owner.GetComponent<Digger>().player != null)
        {
            
            
            if (owner.GetComponent<ObliqPathfinding>().reached_end_path_ == true)
            {
                owner.GetComponent<ObliqPathfinding>().StopPath();
                owner.GetComponent<Entity>().statemachine_.ChangeState(new DiggerAttackState());
            }
        }
      
    }
    public override void Exit(GameObject owner)
    {
        owner.GetComponent<BoxCollider2D>().enabled = true;
    }
}
public class DiggerAttackState : State {
    public override void Enter(GameObject owner)
    {
       
    }
    public override void Execute(GameObject owner)
    {
        owner.GetComponent<Digger>().FireBullets();
        owner.GetComponent<Entity>().statemachine_.ChangeState(new DiggerMoveState());
    }
    public override void Exit(GameObject owner)
    {
       
    }
}


