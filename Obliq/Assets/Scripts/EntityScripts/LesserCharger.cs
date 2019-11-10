using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GF;

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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ImAProjectile>() != null)
        {
            Debug.Log("Enemy hit");
            GC<Entity>(gameObject).TakeDamage(20); //temp magic no
        }
    }
        // Update is called once per frame
        void Update()
    {

    }
   
}
