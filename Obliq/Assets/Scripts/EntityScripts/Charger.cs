using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    // enemy variables
    bool hasCollided = false;
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        hasCollided = true;   
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (hasCollided == true)
            hasCollided = false;
            entity_reference_.statemachine_.SetState(new ChargerIdleState());        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (hasCollided == true)
            hasCollided = false;
        entity_reference_.statemachine_.SetState(new ChargerIdleState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
