using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStates
{
    // something here
}

public class GoodGuyAttack : State
{
    public override void Enter(GameObject owner)
    {
        // if no ap to attack, change to idle state
        if (owner.GetComponent<Entity>().action_points_ <= 0)
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new GoodGuyIdleState());
        }
    }

    public override void Execute(GameObject owner)
    {
        if (owner.GetComponent<Entity>().turn_manager_reference_ != null)
        {
            // if mouse selects something
            GameObject temp = owner.GetComponent<Entity>().turn_manager_reference_.GetEntityAtMouse();
            Entity owner_entity = owner.GetComponent<Entity>();
            if (temp != null)
            {
                // if enemy within range
                if ((temp.transform.position - owner.transform.position).magnitude 
                    < owner_entity.attack_range_)
                {
                    // whack enemy
                    temp.GetComponent<Entity>().TakeDamage(owner_entity.attack_damage_);
                    // subtract ab
                    owner_entity.action_points_ -= owner_entity.point_per_attack_;
                }
            }
        }
    }

    public override void Exit(GameObject owner)
    {

    }
}

public class GoodGuyIdleState : State
{
    public override void Enter(GameObject owner) { }
    public override void Execute(GameObject owner) { }
    public override void Exit(GameObject owner) { }
}
