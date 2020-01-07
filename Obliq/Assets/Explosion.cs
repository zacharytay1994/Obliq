using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    bool causes_hit_pause_ = false;
    [SerializeField]
    bool causes_screen_shake_ = false;
    [SerializeField]
    float screen_shake_base_duration_;
    [SerializeField]
    float screen_shake_base_magnitude_;
    [SerializeField] public int damage_;
    HitPause hit_pause_;
    DamagePopup damage_popup;
    CameraManager camera_manager_;
    
    // Start is called before the first frame update
    void Start()
    {
        damage_popup = GameObject.Find("World").GetComponent<DamagePopup>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
            damage_popup.Create(collision.gameObject, damage_, false);
            collision.gameObject.GetComponent<HealthComponent>().TakeDamage(damage_);
        }
    }
}
