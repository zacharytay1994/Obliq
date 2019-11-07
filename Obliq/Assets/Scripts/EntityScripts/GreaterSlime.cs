using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterSlime : MonoBehaviour
{
    public float move_distance_ = 10.0f;
    public GameObject target_reference_;
    // entity reference
    Entity entity_reference_;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initializing states");
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new GreaterSlimeIdleState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
