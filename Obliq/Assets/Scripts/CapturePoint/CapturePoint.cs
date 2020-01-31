
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    [SerializeField]
    float time_to_capture_;
    float capture_time_elapsed_;
    public bool captured_;
    bool played_effect_;

    [SerializeField]
    float capture_radius_;
    float capture_rate_;
    [SerializeField]
    float max_capture_rate_;
    [SerializeField]
    float color_change_rate_;

    [SerializeField]
    float rotation_speed_;
    float z_rotate_ = 0;

    [SerializeField]
    GameObject capture_particles_;
    GameObject player;

    Color original_color_;

    // Start is called before the first frame update
    void Start()
    {
        original_color_ = gameObject.GetComponent<SpriteRenderer>().color;
        capture_time_elapsed_ = Time.deltaTime;
        capture_rate_ = 1;
        captured_ = false;
        played_effect_ = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (capture_time_elapsed_ >= time_to_capture_)
        {
            captured_ = true;
        }

        if (captured_ && !played_effect_)
        {
            Instantiate(capture_particles_, transform.position, Quaternion.identity);
            played_effect_ = true;
        }

        if (((Vector2)transform.position - (Vector2)player.transform.position).magnitude <= capture_radius_ && !captured_)
        {
            capture_rate_ += 2;
            player.GetComponent<TheGudSuc>().Suc(true);

            Color temp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = Color.Lerp(temp, Color.white, capture_time_elapsed_ / time_to_capture_ * Time.deltaTime);

            if (capture_rate_ >= max_capture_rate_)
            {
                capture_rate_ = max_capture_rate_;
            }

            capture_time_elapsed_ += Time.deltaTime;
        }
        else
        {
            capture_rate_ -= 2;
            player.GetComponent<TheGudSuc>().Suc(false);

            if (!captured_)
            {
                GetComponent<SpriteRenderer>().color = Color.Lerp(gameObject.GetComponent<SpriteRenderer>().color, original_color_, time_to_capture_ * Time.deltaTime);
            }

            if (capture_rate_ <= 1 && !captured_)
            {
                capture_rate_ = 1;
            }
            else if (captured_)
            {
                capture_rate_ = 0;
                rotation_speed_ = 0;
            }

            capture_time_elapsed_ = 0;
        }

        z_rotate_ += Time.deltaTime * (rotation_speed_ + capture_rate_);
        transform.rotation = Quaternion.Euler(0, 0, z_rotate_);
    }
}
