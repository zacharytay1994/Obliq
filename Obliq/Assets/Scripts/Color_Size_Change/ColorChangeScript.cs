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
    //[SerializeField]
    //WorldHandler object_prefab_;
    [Header("3. COMPONENTS (ADD TO SCRIPT WHEN \n NECESSARY)")]
    [SerializeField]
    HealthComponent player_health_component_;
    [SerializeField]
    SpriteRenderer sprite_renderer_;
    [SerializeField]
    GameObject bomb_prefab_;

    // ------------------------------------ //
    // ADD ADDITIONAL VARIABLES HERE
    // ------------------------------------ //

    // Variables for PlayerHealthIndicator (Player's max/current health and percent of health lost)
    float player_max_health_, player_current_health_, player_health_loss_;

    // Variable for bomb list
    float bomb_defuse_time_, defuse_time_left_, defuse_progress_;

    // ------------------------------------ //
    // END OF ADDITIONAL VARIABLES
    // ------------------------------------ //

    // Awake function
    void Awake()
    {
        // For player health indicator
        if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
        {
            // Get sprite renderer
            sprite_renderer_ = GameObject.Find("PlayerHealth").GetComponent<SpriteRenderer>();

            // Player component
            player_health_component_ = GameObject.Find("Player").GetComponent<HealthComponent>();
        }

        // For bomb defuse progress
        if (script_purpose_ == ScriptPurpose.BombDefuseProgress)
        {
            // Get sprite renderer
            sprite_renderer_ = gameObject.GetComponent<SpriteRenderer>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Difference in colour value
        Color diff_color_ = end_color_ - start_color_;
        Color amount_to_change_ = diff_color_ / player_max_health_;

        // For player health indicator
        if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
            UpdateHealth(amount_to_change_);

        // For bomb defuse progress
        if (script_purpose_ == ScriptPurpose.BombDefuseProgress)
            UpdateBombColor();
    }

    // Update is called once per frame
    void Update()
    {
        // Difference in colour value
        Color diff_color_ = end_color_ - start_color_;
        Color amount_to_change_ = diff_color_ / player_max_health_;

        // For player health indicator
        if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
            UpdateHealth(amount_to_change_);

        // For bomb defuse progress
        if (script_purpose_ == ScriptPurpose.BombDefuseProgress)
            UpdateBombColor();
    }

    // ------------------------------------ //
    // ADD ADDITIONAL FUNCTIONS HERE
    // ------------------------------------ //

    // Updates player health indicator (PlayerHealthIndicator)
    void UpdateHealth(Color amount_to_change_)
    {
        // Get player's current/max health
        player_current_health_ = player_health_component_.currentHp_;
        player_max_health_ = player_health_component_.maxHp_;

        // Calculate how much percent health is lost and change in scale of health indicator
        player_health_loss_ = (player_max_health_ - player_current_health_) / player_max_health_;

        // Change percentage of red/green/blue in player health indicator sprite
        // The higher the health, the higher the value of red/green/blue
        sprite_renderer_.color = new Color(
            start_color_.r + (amount_to_change_.r * player_health_loss_ * player_max_health_),
            start_color_.g + (amount_to_change_.g * player_health_loss_ * player_max_health_),
            start_color_.b + (amount_to_change_.b * player_health_loss_ * player_max_health_)
            );
    }

    // Updates bomb color based on progress
    void UpdateBombColor()
    {
        bomb_defuse_time_ = gameObject.GetComponent<BombDefuse>().GetBombDefuseTime();
        defuse_time_left_ = gameObject.GetComponent<BombDefuse>().GetDefuseTimeLeft();
        defuse_progress_ = gameObject.GetComponent<BombDefuse>().GetDefuseProgress();

        defuse_progress_ = (bomb_defuse_time_ - defuse_time_left_) / bomb_defuse_time_;

        // When bomb is fully defused, it changes from red to black
        //sprite_renderer_.color = new Color(
        //    start_color_.r + (amount_to_change_.r * player_health_loss_ * player_max_health_),
        //    start_color_.g + (amount_to_change_.g * player_health_loss_ * player_max_health_),
        //    start_color_.b + (amount_to_change_.b * player_health_loss_ * player_max_health_)
        //    );
    }

    // ------------------------------------ //
    // END OF ADDITIONAL FUNCTIONS
    // ------------------------------------ //
}
