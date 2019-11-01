using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovement : MonoBehaviour
{
    // movement variables
    public float speed_ = 10.0f;
    public Vector2 heading_;
    Vector2 movement_heading_ = new Vector2Int(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeadingVector();
        Vector3 rotate = new Vector3(0.0f, 0.0f, GetOrientation());
        transform.eulerAngles = rotate;
        if (Input.GetKeyDown(KeyCode.A))
        {
            movement_heading_.x -= 1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            movement_heading_.x += 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            movement_heading_.x += 1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            movement_heading_.x -= 1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            movement_heading_.y += 1;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            movement_heading_.y -= 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            movement_heading_.y -= 1;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            movement_heading_.y += 1;
        }
        gameObject.transform.position += (Vector3)((Vector2)movement_heading_.normalized * speed_);
    }

    void UpdateHeadingVector()
    {
        heading_ = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
    }

    float GetOrientation()
    {
        float opposite = -(gameObject.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        float adjacent = gameObject.transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        return Mathf.Atan(opposite / adjacent) * (180.0f / 3.142f);
    }
}
