using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenHPLost : MonoBehaviour
{
    AudioManager am_;
    HealthComponent hc_;
    [SerializeField] string hit_sound_;
    int prev_hp_;
    int current_hp_;
    // Start is called before the first frame update
    void Start()
    {
        hc_ = GetComponent<HealthComponent>();
        am_ = FindObjectOfType<AudioManager>();
        current_hp_ = hc_.getCurrentHp();
        prev_hp_ = current_hp_;
    }

    // Update is called once per frame
    void Update()
    {
        current_hp_ = hc_.getCurrentHp();

        if (prev_hp_>current_hp_)
        {
            am_.PlaySound(hit_sound_);
        }

        prev_hp_ = current_hp_;
    }
}
