using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // temp flag for enemy entities
    public bool start_ = false;
    // reference to turn manager
    public WorldHandler world_handler_reference_;
    // instance statemachine
    public Statemachine statemachine_;
    // if the entity has finished moving on its turn
    public bool has_moved_ = false;
    // the mouse radius when selecting entities on mouse click
    float mouse_radius_ = 1.0f;
 

    // entity variables
    public float unit_scale_per_range_ = 0.2f;  // the world size scale
    public float health_ = 20.0f;               
    public float attack_damage_ = 5.0f;
    [SerializeField] public float attack_range_ = 5.0f;
    [SerializeField]public float detection_range_ = 5.0f;

    private void Awake()
    {
        statemachine_ = new Statemachine(gameObject);
        statemachine_.SetState(new SomeDefault());
        world_handler_reference_ = GameObject.Find("World").GetComponent<WorldHandler>();

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool OnSelect(Vector2 position)
    {
        if ((position - (Vector2)gameObject.transform.position).magnitude < mouse_radius_)
        {
            return true;
        }
        return false;
    }

    public bool GetHasMoved()
    {
        return has_moved_;
    }

    // action functions
    public void TakeDamage(float dmg)
    {
        health_ -= dmg;
    }

    // misc function
    public float GetTrueRange() //i'm using this for detection range
    {
        return detection_range_ * unit_scale_per_range_;
    }
    
}
