using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodGuyStates
{
    // empty
}

public class GoodGuyAttack : State
{
    public override void Enter(GameObject owner)
    {
        // if no ap to attack, change to idle state
        if (owner.GetComponent<GoodGuy>().action_points_ <= 0)
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new SomeDefault());
            Debug.Log("Entity no longer has AP to attack! Something is wrong.");
        }
    }

    public override void Execute(GameObject owner)
    {
        if (owner.GetComponent<Entity>().turn_manager_reference_ != null)
        {
            // if mouse selects something
            GameObject temp = owner.GetComponent<Entity>().turn_manager_reference_.GetEntityAtMouse();
            Entity entity_reference = owner.GetComponent<Entity>();
            GoodGuy good_guy_reference = owner.GetComponent<GoodGuy>();
            if (temp != null)
            {
                // if enemy within range
                if ((temp.transform.position - owner.transform.position).magnitude
                    < entity_reference.attack_range_)
                {
                    // whack enemy
                    temp.GetComponent<Entity>().TakeDamage(entity_reference.attack_damage_);
                    // subtract ab
                    good_guy_reference.action_points_ -= good_guy_reference.point_per_attack_;
                }
            }
        }
    }

    public override void Exit(GameObject owner)
    {

    }
}

public class GoodGuyMoveSelectState : State
{
    public override void Enter(GameObject owner)
    {
        // if no ap to attack, change to idle state
        if (owner.GetComponent<GoodGuy>().action_points_ <= 0)
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new SomeDefault());
            // debug log
            Debug.Log("Entity no longer has AP to move! Something is wrong.");
        }
        owner.GetComponent<GoodGuy>().DisplayMoveDistance();
    }
    public override void Execute(GameObject owner)
    {
    }
    public override void Exit(GameObject owner) { }
}
