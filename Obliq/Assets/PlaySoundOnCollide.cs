using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollide : MonoBehaviour
{
    AudioManager am_;
    [SerializeField] string sound_;
    [SerializeField] int layer_;
    
    // Start is called before the first frame update
    void Awake()
    {
        am_ = FindObjectOfType<AudioManager>();
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == layer_)
        {
            am_.PlaySound(sound_);
        }
    }


}
