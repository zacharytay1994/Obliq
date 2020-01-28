using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenStates : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class WardenIdleState : State
{
    
    public override void Enter(GameObject owner)
    {
       
    }
    public override void Execute(GameObject owner)
    {
        if ((owner.GetComponent<Sectopod>().player_.transform.position - owner.transform.position).magnitude <= owner.GetComponent<Entity>().detection_range_)
        {
            owner.GetComponent<Sectopod>().target_reference_ = owner.GetComponent<Sectopod>().player_;
            owner.GetComponent<Entity>().statemachine_.ChangeState(new WardenAttackingState());
        }
    }
    public override void Exit(GameObject owner)
    {
        
    }
}
public class WardenAttackingState : State
{
    
    public override void Enter(GameObject owner)
    {

    }
    public override void Execute(GameObject owner)
    {
        
    }
    public override void Exit(GameObject owner)
    {

    }
}


