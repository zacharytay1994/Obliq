using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloScript : MonoBehaviour
{
    GameObject player_;
    PlayerController player_controller_;
    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.Find("Player");
        if (player_ != null)
        {
            player_controller_ = player_.GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        UpdateAngle();
    }

    void FollowPlayer()
    {
        if (player_ != null)
        {
            transform.position = new Vector3(player_.transform.position.x, player_.transform.position.y, -1);
        }
    }

    void UpdateAngle()
    {
        if (player_ != null)
        {
            transform.rotation = Quaternion.Euler(0, 0, player_controller_.stored_angle_);
        }
    }
}
