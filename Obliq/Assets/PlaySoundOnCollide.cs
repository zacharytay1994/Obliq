using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollide : MonoBehaviour
{
    AudioManager am_;
    [SerializeField] string sound_;
    [SerializeField] int layer_;
    [SerializeField] float damaged_timer_;
    SpriteRenderer sr_;
    float timer_ = 0;

    // Start is called before the first frame update
    void Awake()
    {
        am_ = FindObjectOfType<AudioManager>();
        sr_ = GetComponent<SpriteRenderer>();
       
    }

    private void Update()
    {
        if (timer_>0)
        {
            timer_ -= Time.deltaTime;
            sr_.color = new Color(sr_.color.r, sr_.color.b, sr_.color.g, 0.5f);
        }
        else
        {
            sr_.color = new Color(sr_.color.r, sr_.color.b, sr_.color.g, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == layer_)
        {
            am_.PlaySound(sound_);
            timer_ = damaged_timer_;
        }
    }


}
