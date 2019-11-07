using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sectopod : MonoBehaviour
{
    public GameObject target_reference_;
    // entity reference
    Entity entity_reference_;
    // Start is called before the first frame update
    void Start()
    {
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new SectopodIdleState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
