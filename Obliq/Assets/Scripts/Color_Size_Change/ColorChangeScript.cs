using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeScript : MonoBehaviour
{
    [Header("1. WHAT IS THIS SCRIPT USED FOR?")]
    [SerializeField]
    ScriptPurpose script_purpose_ = ScriptPurpose.PlayerHealthIndicator;
    [Header("2. VARIABLES")]
    // Trying to get start/end color to work, don't use yet
    [SerializeField]
    Color start_color_ = Color.white;
    [SerializeField]
    Color end_color_ = Color.white;
    [SerializeField]
    int start_red_;
    [SerializeField]
    int start_green_;
    [SerializeField]
    int start_blue_;
    //[SerializeField]
    //WorldHandler object_prefab_;
    [Header("3. COMPONENTS (ADD TO SCRIPT WHEN \n NECESSARY)")]
    [SerializeField]
    HealthComponent player_health_component_;
    [SerializeField]
    SpriteRenderer sprite_renderer_;

    // ------------------------------------ //
    // ADD ADDITIONAL VARIABLES HERE
    // ------------------------------------ //

    // Variables for PlayerHealthIndicator (Player's max/current health and percent of health lost)
    float player_max_health_, player_current_health_, player_health_loss_;

    // ------------------------------------ //
    // END OF ADDITIONAL VARIABLES
    // ------------------------------------ //

    // Awake function
    void Awake()
    {
        // Get sprite renderer
        sprite_renderer_ = GameObject.Find("PlayerHealth").GetComponent<SpriteRenderer>();

        // For player health indicator
        if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
        {
            // Player component
            player_health_component_ = GameObject.Find("Player").GetComponent<HealthComponent>();
        }

        //// For bomb defuse progress
        //if (script_purpose_ == ScriptPurpose.BombDefuseProgress)
        //{
        //    // Player component
        //    player_health_component_ = GameObject.Find("Player").GetComponent<HealthComponent>();
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        // Difference in colour value
        Color diff_color_ = end_color_ - start_color_;
        Color to_change_ = diff_color_ / player_max_health_;

        sprite_renderer_.color = start_color_;
        Debug.Log("gay");
        Debug.Log("start red: " + start_color_.r);
        Debug.Log("start green: " + start_color_.g);
        Debug.Log("start blue: " + start_color_.b);
        // For player health indicator
        //if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
        //UpdateHealth(to_change_);

        //// For bomb defuse progress
        //if (script_purpose_ == ScriptPurpose.BombDefuseProgress)
        //    UpdateBombColor();
    }

    // Update is called once per frame
    void Update()
    {
        // Difference in colour value
        //Color diff_color_ = end_color_ - start_color_;
        //Color to_change_ = diff_color_ / player_max_health_;

        // For player health indicator
        //if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
            //UpdateHealth(to_change_);

        //// For bomb defuse progress
        //if (script_purpose_ == ScriptPurpose.BombDefuseProgress)
        //    UpdateBombColor();
    }

    // ------------------------------------ //
    // ADD ADDITIONAL FUNCTIONS HERE
    // ------------------------------------ //

    // Updates player health indicator (PlayerHealthIndicator)
    void UpdateHealth(Color to_change_)
    {
        // Get player's current/max health
        player_current_health_ = player_health_component_.currentHp_;
        player_max_health_ = player_health_component_.maxHp_;

        // Calculate how much percent health is lost and change in scale of health indicator
        player_health_loss_ = (player_max_health_ - player_current_health_) / player_max_health_;

        // Change percentage of red and green in player health indicator sprite
        // The higher the health, the higher the value of green
        //sprite_renderer_.color = new Color(player_health_loss_, 1 - player_health_loss_, 0f);
        //sprite_renderer_.color = new Color(start_color_.r + to_change_.r, start_color_.g + to_change_.g, start_color_.b + to_change_.b);
        //sprite_renderer_.color = start_color_ + (to_change_ * player_health_loss_);
        /*sprite_renderer_.color = new Color(
            start_color_.r + (to_change_.r * player_health_loss_ * player_max_health_),
            start_color_.g + (to_change_.g * player_health_loss_ * player_max_health_),
            start_color_.b + (to_change_.b * player_health_loss_ * player_max_health_)
            );*/
        //sprite_renderer_.color = new Color(start_color_.r, start_color_.g, start_color_.b);
        //sprite_renderer_.color = new Color(start_red_ / 255, start_green_ / 255, start_blue_ / 255);
        sprite_renderer_.material.color = start_color_;
        //sprite_renderer_.color = new Color(end_color_.r, end_color_.g, end_color_.b);
        Debug.Log("start red: " + start_color_.r);
        Debug.Log("start green: " + start_color_.g);
        Debug.Log("start blue: " + start_color_.b);
        Debug.Log("end red: " + end_color_.r);
        Debug.Log("end green: " + end_color_.g);
        Debug.Log("end blue: " + end_color_.b);
    }

    //// Updates bomb color based on progress
    //void UpdateBombColor()
    //{
    //    // Get player's current/max health
    //    player_current_health_ = player_health_component_.currentHp_;
    //    player_max_health_ = player_health_component_.maxHp_;

    //    // Calculate how much percent health is lost and change in scale of health indicator
    //    player_health_loss_ = (player_max_health_ - player_current_health_) / player_max_health_;

    //    // Change percentage of red and green in player health indicator sprite
    //    // The higher the health, the higher the value of green
    //    sprite_renderer_.color = new Color(player_health_loss_, 1 - player_health_loss_, 0f);
    //}

    // ------------------------------------ //
    // END OF ADDITIONAL FUNCTIONS
    // ------------------------------------ //
}
