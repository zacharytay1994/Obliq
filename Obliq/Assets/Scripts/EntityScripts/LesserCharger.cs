using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserCharger : MonoBehaviour
{
    // lesser charger variables
    public GameObject target_reference_;
   
    public float speed_ = 5.0f;
    // entity reference
    Entity entity_reference_;
    // Start is called before the first frame update
    void Start()
    {
        
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new LCIdle());
    }

    // Update is called once per frame
    void Update()
    {

    }
   
}
