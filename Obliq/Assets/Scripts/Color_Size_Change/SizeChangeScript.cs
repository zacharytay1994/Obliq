using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangeScript : MonoBehaviour
{
    [Header("1. WHAT IS THIS SCRIPT USED FOR")]
    [SerializeField]
    ScriptPurpose script_purpose_ = ScriptPurpose.PlayerHealthIndicator;
    [Header("2. VARIABLES")]
    [SerializeField]
    float max_size_or_scale_;
    [SerializeField]
    float min_size_or_scale_;
    [Header("3. COMPONENTS (ADD TO SCRIPT WHEN \n NECESSARY)")]
    [SerializeField]
    HealthComponent player_health_component_;
    [SerializeField]
    RectTransform rect_transform_;

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
        // For player health indicator
        if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
        {
            // Player health component
            player_health_component_ = GameObject.Find("Player").GetComponent<HealthComponent>();

            // Get rect transform
            rect_transform_ = GameObject.Find("PlayerHealth").GetComponent<RectTransform>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // For player health indicator
        if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
            UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        // For player health indicator
        if (script_purpose_ == ScriptPurpose.PlayerHealthIndicator)
            UpdateHealth();
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

        // Change size of health indicator (the lower the health, the smaller the sprite)
        rect_transform_.localScale = new Vector3
            (max_size_or_scale_ * (1-player_health_loss_), max_size_or_scale_ * (1-player_health_loss_), 1);

        // If health indicator smaller than minimum scale, set to minimum scale
        if (rect_transform_.localScale.x <= min_size_or_scale_)
        {
            rect_transform_.localScale = new Vector3(min_size_or_scale_, min_size_or_scale_, 1);
        }
    }

    // ------------------------------------ //
    // END OF ADDITIONAL FUNCTIONS
    // ------------------------------------ //
}
