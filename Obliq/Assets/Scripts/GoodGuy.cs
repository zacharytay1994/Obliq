using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodGuy : MonoBehaviour
{
    // entity reference
    Entity entity_reference_;

    // movement & action variables
    public int action_points_ = 3;
    public float distance_per_point_ = 5.0f;
    public int point_per_attack_ = 1;

    // Start is called before the first frame update
    void Start()
    {
        entity_reference_ = gameObject.GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteTurn()
    {
        // change state to attack state
        if (Input.GetKeyDown(KeyCode.A))
        {
            entity_reference_.statemachine_.ChangeState(new GoodGuyAttack());
        }
        // change state to move state
        if (Input.GetKeyDown(KeyCode.M))
        {
            entity_reference_.statemachine_.ChangeState(new GoodGuyMoveSelectState());
            print("State move");
        }
        entity_reference_.statemachine_.Update();
    }

    public void DisplayMoveDistance()
    {
        SetMoveRadiusInactive(true);
        // get and set radius circles
        SpriteRenderer radius_one = GameObject.Find("RadiusOne").GetComponent<SpriteRenderer>();
        SpriteRenderer radius_two = GameObject.Find("RadiusTwo").GetComponent<SpriteRenderer>();
        SpriteRenderer radius_three = GameObject.Find("RadiusThree").GetComponent<SpriteRenderer>();
        radius_three.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 3);
        radius_two.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 2);
        radius_one.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1);
        float radius_size = distance_per_point_ * 2.0f * entity_reference_.unit_scale_per_range_;
        Vector2 size_to_set = new Vector2(radius_size, radius_size);
        radius_one.size = size_to_set;
        radius_two.size = size_to_set * 2;
        radius_three.size = size_to_set * 3;
    }

    void SetMoveRadiusInactive(bool active)
    {
        GameObject.Find("World").GetComponent<WorldHandler>().SetMoveRadiusInactive(true);
    }
}
