using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnSpawn : MonoBehaviour
{
    AudioManager am_;
    [SerializeField] string sound_;
    float pitch_;
    // Start is called before the first frame update
    void Awake()
    {
        pitch_ = Random.Range(0.9f, 1.1f);
        am_ = FindObjectOfType<AudioManager>();
        am_.PlaySound(sound_,1,pitch_);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
