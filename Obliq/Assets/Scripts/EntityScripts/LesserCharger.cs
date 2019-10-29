using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserCharger : MonoBehaviour
{
    // lesser charger variables
    public GameObject target_reference_;
    int original_action_points_;
    public int action_points_ = 2;
    public float speed_ = 15.0f;
    // entity reference
    Entity entity_reference_;
    // Start is called before the first frame update
    void Start()
    {
        original_action_points_ = action_points_;
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new LCIdle());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EndTurn()
    {
        entity_reference_.has_moved_ = true;
        action_points_ = original_action_points_;
    }
}
