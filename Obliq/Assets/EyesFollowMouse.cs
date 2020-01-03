using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesFollowMouse : MonoBehaviour
{
    Vector3 mouse_pos_;
    [SerializeField] GameObject eyes_;
    Vector3 eyes_pos_;
    [SerializeField] float eyes_left_limit_;
    [SerializeField] float eyes_right_limit_;
    [SerializeField] float eyes_up_limit_;
    [SerializeField] float eyes_down_limit_;
    [SerializeField] float eye_move_rate_;
    Vector3 new_eyes_pos_;
    // Start is called before the first frame update
    void Start()
    {
        eyes_pos_ = eyes_.transform.localPosition;
        new_eyes_pos_ = eyes_pos_;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouse_pos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(new_eyes_pos_.x>mouse_pos_.x && new_eyes_pos_.x>eyes_pos_.x-eyes_left_limit_)
        {
            new_eyes_pos_ += new Vector3(-eye_move_rate_, 0, 0);
        }
        else if (new_eyes_pos_.x<mouse_pos_.x && new_eyes_pos_.x< eyes_pos_.x + eyes_right_limit_)
        {
            new_eyes_pos_ += new Vector3( eye_move_rate_, 0, 0);
        }

        if (new_eyes_pos_.y > mouse_pos_.y && new_eyes_pos_.y > eyes_pos_.y-eyes_down_limit_)
        {
            new_eyes_pos_ += new Vector3(0, -eye_move_rate_, 0);
        }
        else if (new_eyes_pos_.y < mouse_pos_.y && new_eyes_pos_.y < eyes_pos_.y+eyes_up_limit_)
        {
            new_eyes_pos_ += new Vector3(eyes_pos_.x, eye_move_rate_, 0);
        }

        eyes_.transform.localPosition = new_eyes_pos_;

    }
}
