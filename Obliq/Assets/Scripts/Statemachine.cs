using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statemachine
{
    State current_state_;
    GameObject owner_;

    public Statemachine(GameObject owner)
    {
        owner_ = owner;
    }

    public void ChangeState(State newstate)
    {
        if (current_state_ != null)
        {
            current_state_.Exit(owner_);
            current_state_ = newstate;
            newstate.Enter(owner_);
        }
    }

    public void Update()
    {
        if (current_state_ != null)
        {
            current_state_.Execute(owner_);
        }
    }
}

public abstract class State
{
    public State() { }
    public abstract void Enter(GameObject owner);
    public abstract void Exit(GameObject owner);
    public abstract void Execute(GameObject owner);
}
