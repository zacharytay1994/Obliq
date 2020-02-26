using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveIndicator : MonoBehaviour
{
    // Target to point to
    GameObject objective_;

    // How far the indicator is from the player
    [SerializeField]
    public float offset_amount_;

    // Initialise some variables
    Vector3 target_position_, player_position_;
    GameObject pointer_;

    public GameObject GetObjective()
    {
        return objective_;
    }

    public void SetObjective(GameObject g)
    {
        objective_ = g;
    }
    // Start is called before the first frame update
    void Awake()
    {
        // Pointer
        pointer_ = gameObject.transform.GetChild(0).gameObject;

        // Offset amount
        //offset_amount_ = 5;
    }

    // Update is called once per frame
    void Update()
    {
        // If objective is not destroyed
        if (objective_ != null)
        {
            if (objective_.name == "CapturePoint")
            {
                if (objective_.GetComponent<CapturePoint>().captured_)
                {
                    pointer_.SetActive(false);
                }
                else
                {
                    pointer_.SetActive(true);
                }
            }
            else
            {
                // If target is disabled, disable pointer. If target is enabled, activate indicator.
                if (objective_.activeSelf == false)
                {
                    pointer_.SetActive(false);
                }
                else if (objective_.activeSelf == true)
                {
                    pointer_.SetActive(true);
                }
            }

            // Player position
            player_position_ = GameObject.Find("Player").transform.position;

            // Position of target
            target_position_ = objective_.transform.position;

            // Set pointer position to unit vector from player to objective
            Vector3 position_offset = Vector3.Normalize(target_position_ - player_position_);
            pointer_.transform.position = player_position_ + (position_offset * offset_amount_);

            // Get direction to target
            Vector2 direction = new Vector2
                (target_position_.x - pointer_.transform.position.x, target_position_.y - pointer_.GetComponent<Transform>().position.y);

            // Set direction to target
            pointer_.GetComponent<Transform>().up = direction;
        }

        // If target is destroyed, disable pointer
        else
        {
            pointer_.SetActive(false);
        }
    }
}