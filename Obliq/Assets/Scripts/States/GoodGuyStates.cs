using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static GF;

public class GoodGuyStates
{
    // maybe needed maybe not
}

public class GoodGuyAttack : State
{
    public override void Enter(GameObject owner)
    {
        // if no ap to attack, change to idle state
        if (owner.GetComponent<GoodGuy>().action_points_ <= 0)
        {
            Debug.Log("Entity no longer has AP to attack! Something is wrong.");
            ChangeState(owner, new GoodGuyIdle());
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
                    ChangeState(owner, new GoodGuyIdle());
                }
            }
        }
    }

    public override void Exit(GameObject owner) { }
}

public class GoodGuyMoveSelectState : State
{
    public override void Enter(GameObject owner)
    {
        owner.GetComponent<GoodGuy>().DisplayMoveDistance();
    }
    public override void Execute(GameObject owner)
    {
        // check if ap left, if none left, turn ends set state to default
        if (owner.GetComponent<GoodGuy>().action_points_ <= 0)
        {
            owner.GetComponent<GoodGuy>().EndTurn();
        }
        // if position if selected
        if (Input.GetMouseButton(0))
        {
            Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance_away = ((Vector2)owner.transform.position - mouse_position).magnitude;
            // tentative
            // if out of range
            if (distance_away > owner.GetComponent<GoodGuy>().distance_per_point_ * 
                owner.GetComponent<GoodGuy>().action_points_ * 
                owner.GetComponent<Entity>().unit_scale_per_range_)
            {
                Debug.Log("Selected point is out of range.");
            }
            // if more then 2 points worth away, subtract 3 ap, and change to move state
            else if (distance_away > owner.GetComponent<GoodGuy>().distance_per_point_ * 2 * owner.GetComponent<Entity>().unit_scale_per_range_)
            {
                owner.GetComponent<GoodGuy>().action_points_ -= 3;
                owner.GetComponent<ObliqPathfinding>().StartPath(mouse_position);
                ChangeState(owner, new GoodGuyMoveState());
            }
            // if more than 1 points worth away, subtract 2
            else if (distance_away > owner.GetComponent<GoodGuy>().distance_per_point_ * owner.GetComponent<Entity>().unit_scale_per_range_)
            {
                owner.GetComponent<GoodGuy>().action_points_ -= 2;
                owner.GetComponent<ObliqPathfinding>().StartPath(mouse_position);
                ChangeState(owner, new GoodGuyMoveState());
            } 
            else if (distance_away < owner.GetComponent<GoodGuy>().distance_per_point_ * owner.GetComponent<Entity>().unit_scale_per_range_)
            {
                owner.GetComponent<GoodGuy>().action_points_ -= 1;
                owner.GetComponent<ObliqPathfinding>().StartPath(mouse_position);
                ChangeState(owner, new GoodGuyMoveState());
            }
            // debug ui
            GameObject.Find("GoodGuyAP").GetComponent<Text>().text = owner.GetComponent<GoodGuy>().action_points_.ToString();
        }
    }
    public override void Exit(GameObject owner)
    {
        GameObject.Find("World").GetComponent<WorldHandler>().SetMoveRadiusActive(false);
    }
}

public class GoodGuyMoveState : State
{
    public override void Enter(GameObject owner) { }
    public override void Execute(GameObject owner)
    {
        // if finished current path
        if (owner.GetComponent<ObliqPathfinding>().reached_end_path_)
        {
            // change state to default state awaiting input for attack or move
            ChangeState(owner, new GoodGuyIdle());
        }
    }
    public override void Exit(GameObject owner) { }
}

public class GoodGuyIdle : State
{
    public override void Enter(GameObject owner)
    {
        owner.GetComponent<GoodGuy>().is_idle_ = true;
        if (owner.GetComponent<GoodGuy>().action_points_ <= 0)
        {
            owner.GetComponent<GoodGuy>().EndTurn();
        }
    }
    public override void Execute(GameObject owner)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeState(owner, new GoodGuyAttack());
        }
        // change state to move state
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (owner.GetComponent<GoodGuy>().action_points_ <= 0)
            {
                // debug log
                Debug.Log("Entity no longer has AP to move!");
                owner.GetComponent<GoodGuy>().EndTurn();
            }
            else
            {
                ChangeState(owner, new GoodGuyMoveSelectState());
            }
        }
    }
    public override void Exit(GameObject owner)
    {
        owner.GetComponent<GoodGuy>().is_idle_ = false;
    }
}
