using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // reference to turn manager
    public TurnManager turn_manager_reference_;
    bool has_moved_ = false;
    float mouse_radius_ = 10.0f;

    // entity variables
    public float attack_damage_ = 5.0f;
    public float attack_range_ = 5.0f;
    float health_ = 20.0f;

    public Statemachine statemachine_;

    // movement & action variables
    public int action_points_ = 3;
    int distance_per_point_ = 5;
    public int point_per_attack_ = 1;

    // Start is called before the first frame update
    void Start()
    {
        statemachine_ = new Statemachine(gameObject);
        turn_manager_reference_ = GameObject.Find("World").GetComponent<TurnManager>();
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

    public void ExecuteTurn()
    {
        if (Input.GetKeyDown("A"))
        {
            statemachine_.ChangeState(new GoodGuyAttack());
        }
        statemachine_.Update();
    }

    // action functions
    public void TakeDamage(float dmg)
    {
        health_ -= dmg;
    }
}
