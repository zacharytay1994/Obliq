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
    Color start_color_;
    [SerializeField]
    Color end_color_;
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
        // For player health indicator
        if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
            UpdateHealth();

        //// For bomb defuse progress
        //if (script_purpose_ == ScriptPurpose.BombDefuseProgress)
        //    UpdateBombColor();
    }

    // Update is called once per frame
    void Update()
    {
        // For player health indicator
        if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
            UpdateHealth();

        //// For bomb defuse progress
        //if (script_purpose_ == ScriptPurpose.BombDefuseProgress)
        //    UpdateBombColor();
    }

    // ------------------------------------ //
    // ADD ADDITIONAL FUNCTIONS HERE
    // ------------------------------------ //

    // Updates player health indicator (PlayerHealthIndicator)
    void UpdateHealth()
    {
        // Get player's current/max health
        player_current_health_ = player_health_component_.currentHp_;
        player_max_health_ = player_health_component_.maxHp_;

        // Calculate how much percent health is lost and change in scale of health indicator
        player_health_loss_ = (player_max_health_ - player_current_health_) / player_max_health_;

        // Change percentage of red and green in player health indicator sprite
        // The higher the health, the higher the value of green
        sprite_renderer_.color = new Color(player_health_loss_, 1 - player_health_loss_, 0f);
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
