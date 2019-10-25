using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObliqEnemy : MonoBehaviour
{
    // enemy variables
    public float move_distance_ = 10.0f;
    public GameObject target_reference_;
    // entity reference
    Entity entity_reference_;

    // Start is called before the first frame update
    void Start()
    {
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new ChargerIdleState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
