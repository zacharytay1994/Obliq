using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRecticle : MonoBehaviour
{
    [SerializeField]
    float offset_ = 0.0f;
    Vector2 basis_vector_ = new Vector2(0.0f, 1.0f);
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
        UpdateAngle();
        UpdatePosition();
    }

    void UpdateAngle()
    {
        if (player_ != null)
        {
            transform.rotation = Quaternion.Euler(0, 0, player_controller_.stored_angle_);
        }
    }

    void UpdatePosition()
    {
        float rad_angle = player_controller_.stored_angle_ * (3.142f / 180.0f);
        // rotate basis vector
        Vector2 rotated_vector = new Vector2((basis_vector_.x * Mathf.Cos(rad_angle) + basis_vector_.y * -Mathf.Sin(rad_angle)), 
            (basis_vector_.x * Mathf.Sin(rad_angle) + basis_vector_.y * Mathf.Cos(rad_angle)));
        Vector2 offset_vector = (Vector2)player_.transform.position + rotated_vector.normalized * offset_;
        transform.position = new Vector3(offset_vector.x, offset_vector.y, -1.0f);
    }
}
