using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // reference to turn manager
    public TurnManager turn_manager_reference_;
    public bool has_moved_ = false;
    float mouse_radius_ = 1.0f;

    // entity variables
    public float unit_scale_per_range_ = 0.2f;
    public float health_ = 20.0f;
    public float attack_damage_ = 5.0f;
    public float attack_range_ = 5.0f;

    public Statemachine statemachine_;

    private void Awake()
    {
        statemachine_ = new Statemachine(gameObject);
        statemachine_.SetState(new SomeDefault());
        turn_manager_reference_ = GameObject.Find("World").GetComponent<TurnManager>();
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
}
