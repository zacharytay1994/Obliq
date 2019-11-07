using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollowPlayer : MonoBehaviour
{
    float offset_ = 0.0f;
    GameObject player_ = null;
    bool initialized_ = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized_)
        {
            // get vector from player to mouse
            Vector2 direction = (Vector2)((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)player_.transform.position).normalized;
            transform.position = (Vector2)player_.transform.position + direction * offset_;
        }
    }

    public void InitBall(float offset, GameObject gameobject)
    {
        offset_ = offset;
        player_ = gameobject;
        initialized_ = true;
    }

    void FollowPlayerDirection()
    {

    }
}
