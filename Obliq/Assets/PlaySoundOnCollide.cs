using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollide : MonoBehaviour
{
    AudioManager am_;
    [SerializeField] string sound_;
    [SerializeField] LayerMask layer_;
    [SerializeField] float damaged_timer_;
    [SerializeField] float sound_timer_ = 0;
    SpriteRenderer sr_;
    float timer_ = 0;
    float s_timer_;
    

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
        if(s_timer_>0)
        {
            s_timer_ -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(layer_ == (layer_ | (1 << collision.gameObject.layer)))
        {
            if(s_timer_ <=0)
            {
                am_.PlaySound(sound_);
                s_timer_ = sound_timer_;
            }
            
            timer_ = damaged_timer_;
            
        }
    }


}
