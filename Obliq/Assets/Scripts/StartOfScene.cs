using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOfScene : MonoBehaviour
{
    AudioManager am_;
    [SerializeField] List<string> loop_audio_;
    [SerializeField] List<string> play_once_audio_;

    [Space(10)]
    [SerializeField] bool choose_one_loop_ = true;
    [SerializeField] bool end_prev_scene_sounds_ = true;

    private void Awake()
    {
        am_ = FindObjectOfType<AudioManager>();

        if (end_prev_scene_sounds_)
        {
            am_.StopAllSound();
        }
        if(choose_one_loop_)
        {
            int chosen_audio = Random.Range(0, loop_audio_.Count);
            am_.PlaySoundOnLoop(loop_audio_[chosen_audio]);
        }
        else
        {
            for (int i = 0; i < loop_audio_.Count; i++)
            {
                am_.PlaySoundOnLoop(loop_audio_[i]);
            }
        }

        for (int i = 0; i < play_once_audio_.Count; i++)
        {
            am_.PlaySound(play_once_audio_[i]);
        }
    }




}
