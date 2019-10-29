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
        Debug.Log("Entered idle state.");
    }
    public override void Execute(GameObject owner)
    {
        // if time to move
        if (!owner.GetComponent<Entity>().has_moved_)
        {
            // if enough AP
            if (GC<LesserCharger>(owner).action_points_ > 0)
            {
                // Get nearest good guy
                GameObject nearest_good_guy_ = GC<Entity>(owner).turn_manager_reference_.GetNearestGoodGuy((Vector2)owner.transform.position);
                // if found
                if (nearest_good_guy_ != null)
                {
                    // set target to nearest_good_guy
                    GC<LesserCharger>(owner).target_reference_ = nearest_good_guy_;
                    // if within range
                    if ((nearest_good_guy_.transform.position - owner.transform.position).magnitude < GC<Entity>(owner).GetTrueRange())
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
                // else idle state, and end turn
                else
                {
                    GC<LesserCharger>(owner).EndTurn();
                }
            }
            // else idle state, and end turn
            else
            {
                GC<LesserCharger>(owner).EndTurn();
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
        float distance = (GC<LesserCharger>(owner).target_reference_.transform.position - owner.transform.position).magnitude;
        float true_range = GC<LesserCharger>(owner).speed_ * GC<Entity>(owner).unit_scale_per_range_;
        // if unreachable with current action points
        if (distance > true_range * GC<LesserCharger>(owner).action_points_)
        {
            // find closest point there
            Vector2 to_add = ((Vector2)GC<LesserCharger>(owner).target_reference_.transform.position - (Vector2)owner.transform.position).normalized
                * true_range * GC<LesserCharger>(owner).action_points_;
            // empty action points as max distance travelled
            GC<LesserCharger>(owner).action_points_ = 0;
            // start pathing towards
            GC<ObliqPathfinding>(owner).StartPath((Vector2)owner.transform.position + to_add);
        }
        // if reachable
        else
        {
            // subtract action points
            GC<LesserCharger>(owner).action_points_ -= (int)(distance / true_range) + 1;
            // start pathing towards
            GC<ObliqPathfinding>(owner).StartPath(GC<LesserCharger>(owner).target_reference_.transform.position);
        }
    }
    public override void Execute(GameObject owner)
    {
        // if flag thrown that path was not found
        if (GC<ObliqPathfinding>(owner).path_not_found_)
        {
            GC<ObliqPathfinding>(owner).path_not_found_ = false;
            GC<LesserCharger>(owner).EndTurn();
        }
        // if reached target
        if (GC<ObliqPathfinding>(owner).reached_end_path_)
        {
            // go back to idle state
            ChangeState(owner, new LCIdle());
        }
    }
    public override void Exit(GameObject owner) { }
}

public class LCAttack : State
{
    public override void Enter(GameObject owner) { }
    public override void Execute(GameObject owner) { }
    public override void Exit(GameObject owner) { }
}