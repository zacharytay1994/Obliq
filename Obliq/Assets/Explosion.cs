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

    [SerializeField]
    float random_size_ = 5.0f;
    [SerializeField]
    float speed_ = 50.0f;
    Vector2 random_direction_ = Vector2.zero;
    SpriteRenderer sr_ = null;
    [SerializeField]
    float fade_speed_ = 1.0f;
    float og_size_ = 0.0f;
    float fade_acceleration = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        hit_pause_ = FindObjectOfType<HitPause>();
        camera_manager_ = FindObjectOfType<CameraManager>();
        damage_popup = GameObject.Find("World").GetComponent<DamagePopup>();
        Destroy(gameObject, 5.0f);

        og_size_ = random_size_;
        random_size_ = Random.Range(1.0f, random_size_ - 2.0f);
        gameObject.transform.localScale = new Vector3(random_size_, random_size_, 1.0f);
        speed_ = (og_size_ - random_size_) * speed_;
        random_direction_ = GF.RotateVector(new Vector2(0.0f, 1.0f), Random.Range(0.0f, 360.0f));
        sr_ = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(random_direction_ * speed_ * Time.deltaTime);
        // make fade over time
        Color fade = sr_.color;
        if (fade.a > 0.0f)
        {
            fade_acceleration += ((og_size_ - random_size_) / og_size_) * fade_speed_ * Time.deltaTime;
            fade.a -= fade_acceleration;
            sr_.color = fade;
        }
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
