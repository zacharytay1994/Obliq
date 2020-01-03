using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField]
    int damage_ = 0;
    [SerializeField]
    LayerMask enemy_mask_ = 0;
    [SerializeField]
    bool causes_hit_pause_ = false;
    [SerializeField]
    bool causes_screen_shake_ = false;
    [SerializeField]
    float screen_shake_base_duration_;
    [SerializeField]
    float screen_shake_base_magnitude_;
    [SerializeField]
    [Range(0,100)]
    int crit_chance;

    [SerializeField]
    int crit_modifier;
    bool knock_back_ = false;
   

    HitPause hit_pause_;
    CameraManager camera_manager_;
    DamagePopup damage_popup_manager_;
    // Start is called before the first frame update
    void Start()
    {
        hit_pause_ = FindObjectOfType<HitPause>();
        camera_manager_ = FindObjectOfType<CameraManager>();
        damage_popup_manager_ = GameObject.Find("World").GetComponent<DamagePopup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
            if (enemy_mask_ == (enemy_mask_ | (1 << collision.gameObject.layer)))
            {
                if (collision.gameObject.GetComponent<HealthComponent>() != null)
                {
                    if (causes_screen_shake_)
                    {
                        camera_manager_.Shake(hit_pause_.base_duration_ * damage_ * screen_shake_base_duration_, damage_ * screen_shake_base_magnitude_);
                    }
                    if (causes_hit_pause_)
                    {
                        hit_pause_.Freeze(damage_);
                    }

                    if (Random.Range(1, 100) <= crit_chance)
                    {
                        Debug.Log("CRIT");
                        if (!collision.gameObject.GetComponent<HealthComponent>().isInvincible() && collision.gameObject != GameObject.Find("Player"))
                        {
                            damage_popup_manager_.Create(gameObject, damage_ * crit_modifier, true);
                        }
                        collision.gameObject.GetComponent<HealthComponent>().TakeDamage(damage_ * crit_modifier);
                    }
                    else
                    {
                        if (!collision.gameObject.GetComponent<HealthComponent>().isInvincible() && collision.gameObject != GameObject.Find("Player"))
                        {
                            if (gameObject != null)
                            {
                                damage_popup_manager_.Create(gameObject, damage_, true);
                            }

                        }
                        collision.gameObject.GetComponent<HealthComponent>().TakeDamage(damage_);
                    }
                    // get direction
                    Vector2 direction = ((Vector2)collision.gameObject.transform.position - (Vector2)transform.position).normalized;

                    if (collision.gameObject.GetComponent<BloodEffect>() != null)
                    {
                        collision.gameObject.GetComponent<BloodEffect>().DrawBlood(direction);
                    }

                if (knock_back_ && GetComponent<BombKnockback>() != null)
                {
                    GetComponent<BombKnockback>().ForcePush();
                }

                }
            }

        
    }
}
