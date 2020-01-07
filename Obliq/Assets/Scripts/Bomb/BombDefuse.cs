
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BombDefuse : MonoBehaviour
{
    bool bomb_defused_ = false;
    [SerializeField]
    float bomb_defuse_range_ = 0.0f;
    [SerializeField]
    GameObject player_ ;

    // Bomb defuse timer
    [Header("BOMB DEFUSE TIMER")]
    [SerializeField]
    GameObject BombDefusePrefab;
    [SerializeField]
    float bomb_defuse_time_ = 5.0f;
    float defuse_time_left_;
    Image defuse_timer_bar_;

    // Change color of bomb based on progress
    [Header("CHANGE BOMB COLOR BASED ON PROGRESS")]
    [SerializeField]
    GameObject BombPrefab;
    [SerializeField]
    SpriteRenderer sprite_renderer_;
    float defuse_progress_;

    // Awake function
    void Awake()
    {
        sprite_renderer_ = BombPrefab.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.Find("Player");
        // Bomb defuse timer bar
        defuse_timer_bar_ = BombDefusePrefab.GetComponent<Image>();
        defuse_time_left_ = bomb_defuse_time_;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsWithinRange())
        {
            // If bomb is not defused, decrease defuse timer bar size.
            if (defuse_time_left_ <= 0)
            {
                bomb_defused_ = true;
                Destroy(gameObject);
                Destroy(BombDefusePrefab);
            }
            else
            {
                // Countdown
                defuse_time_left_ -= Time.deltaTime;
            }
        }
        else
        {
            // Reset timer
            defuse_time_left_ = bomb_defuse_time_;
        }
        
        // Defuse timer bar scales to time left
        defuse_timer_bar_.fillAmount = defuse_time_left_ / bomb_defuse_time_;

        // Bomb changes color based on progress
        UpdateBombColor(bomb_defuse_time_, defuse_time_left_);
    }

    void UpdateBombColor(float bomb_defuse_time_, float defuse_time_left_)
    {
        defuse_progress_ = (bomb_defuse_time_ - defuse_time_left_) / bomb_defuse_time_;

        // When bomb is fully defused, it changes from red to black
        sprite_renderer_.color = new Color(1 - defuse_progress_, 0f, 0f);
    }

    public bool BombDefused()
    {
        return bomb_defused_;
    }

    bool PlayerIsWithinRange()
    {
        if (((Vector2)gameObject.transform.position - (Vector2)player_.transform.position).magnitude < bomb_defuse_range_)
        {
            return true;
        }
        return false;
    }

    public float GetBombDefuseTime()
    {
        return bomb_defuse_time_;
    }

    public float GetDefuseTimeLeft()
    {
        return defuse_time_left_;
    }

    public float GetDefuseProgress()
    {
        return defuse_progress_;
    }
}
