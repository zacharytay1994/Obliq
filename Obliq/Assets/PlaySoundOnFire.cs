using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnFire : MonoBehaviour
{
    AudioManager am_;
    [SerializeField] string sound_;
    GameObject gun_;
    ImAProjectile fire_script_;
    bool fired_in_prev_frame_;
    // Start is called before the first frame update
    void Awake()
    {
        am_ = FindObjectOfType<AudioManager>();
        gun_ = gameObject;
        fire_script_ = gun_.GetComponent<ImAProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fire_script_.start_spawning_ && !fired_in_prev_frame_)
        {
            am_.PlaySound(sound_);
        }
        fired_in_prev_frame_ = fire_script_.start_spawning_;
    }
}
