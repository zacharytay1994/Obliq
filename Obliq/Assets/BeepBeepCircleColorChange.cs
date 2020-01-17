using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeepBeepCircleColorChange : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer beep_beep_circle_;
    [SerializeField]
    BombScript bs_;
    float bomb_timer_;
    float beep_timer_;
    float timer_;

    Color color_changed_ = new Color(1,1,1);

    bool currently_lit_ = false; //true for red, false for white

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timer_<=0.0f)
        {
            bomb_timer_ = bs_.time_left_;
            beep_timer_ = BombToBeepConversion(bomb_timer_);

            timer_ = beep_timer_;
            if(currently_lit_)
            {
                currently_lit_ = false;
            }
            else
            {
                currently_lit_ = true;
            }
        }

        if (timer_>=0)
        {
            timer_ -= Time.deltaTime;
            if (currently_lit_)
            {
                color_changed_.b += 1;
                color_changed_.g += 1;
            }
            else
            {
                color_changed_.b -= 1;
                color_changed_.g -= 1;
            }
            beep_beep_circle_.color = color_changed_;
        }


    }

    float BombToBeepConversion(float bomb_timer)
    {
        return bomb_timer / 10;
    }
}
