﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodGuy : MonoBehaviour
{
    // entity reference
    Entity entity_reference_;

    // movement & action variables
    public int action_points_ = 3;
    int original_action_points_;
    public float distance_per_point_ = 5.0f;
    public int point_per_attack_ = 1;

    // flag for if GoodGuy is in GoodGuyIdleState
    public bool is_idle_ = true;

    // Start is called before the first frame update
    void Start()
    {
        original_action_points_ = action_points_;
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new GoodGuyIdle());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 20));
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-20, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -20));
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(20, 0));
        }
    }

    public void ExecuteTurn()
    {
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

    // sets radius circles to be inactive so it is no longer visible
    void SetMoveRadiusInactive(bool active)
    {
        GameObject.Find("World").GetComponent<WorldHandler>().SetMoveRadiusActive(true);
    }

    public void EndTurn()
    {
        entity_reference_.has_moved_ = true;
        action_points_ = original_action_points_;
    }
}