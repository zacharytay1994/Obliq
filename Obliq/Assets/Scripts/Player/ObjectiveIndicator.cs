using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveIndicator : MonoBehaviour
{
    [SerializeField]
    Camera camera_;
    [SerializeField]
    GameObject objective_;

    Vector2 target_position_;
    RectTransform pointer_rect_transform_;
    GameObject pointer_;

    // Start is called before the first frame update
    void Start()
    {
        target_position_ = objective_.GetComponent<Transform>().position;
        pointer_rect_transform_ = GameObject.Find("Pointer").GetComponent<RectTransform>();
        pointer_ = GameObject.Find("Pointer");
    }

    // Update is called once per frame
    void Update()
    {
        float border_size_ = 15f;
        Vector3 target_position_screen_point_ = Camera.main.WorldToScreenPoint(target_position_);
        bool is_off_screen_ = target_position_screen_point_.x <= 0 || target_position_screen_point_.x >= Screen.width ||
            target_position_screen_point_.y <= 0 || target_position_screen_point_.y >= Screen.height;

        // If target is off-screen, activate indicator
        if (is_off_screen_)
        {
            // Activate indicator
            pointer_.SetActive(true);

            // Rotate indicator to target
            RotateToTarget();

            // Set indicator at edge of screen
            Vector3 capped_target_screen_position_ = target_position_screen_point_;
            if (capped_target_screen_position_.x <= border_size_)
                capped_target_screen_position_.x = border_size_;
            if (capped_target_screen_position_.x >= Screen.width - border_size_)
                capped_target_screen_position_.x = Screen.width - border_size_;
            if (capped_target_screen_position_.y <= border_size_)
                capped_target_screen_position_.y = border_size_;
            if (capped_target_screen_position_.y >= Screen.height - border_size_)
                capped_target_screen_position_.y = Screen.height - border_size_;

            Vector3 pointer_world_position_ = camera_.ScreenToWorldPoint(capped_target_screen_position_);
            pointer_rect_transform_.position = pointer_world_position_;
            pointer_rect_transform_.localPosition = new Vector3(pointer_rect_transform_.localPosition.x, pointer_rect_transform_.localPosition.y);
        }

        // If target can be seen, disable indicator
        else
        {
            pointer_.SetActive(false);
        }
    }

    // Rotate indicator to target
    void RotateToTarget()
    {
        // Get direction to target
        Vector2 direction = new Vector2
            (target_position_.x - pointer_.GetComponent<Transform>().position.x, target_position_.y - pointer_.GetComponent<Transform>().position.y);

        // Set direction to target
        pointer_.GetComponent<Transform>().up = direction;
    }
}
