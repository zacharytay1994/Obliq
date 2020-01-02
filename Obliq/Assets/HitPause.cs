using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPause : MonoBehaviour
{
    [Range(0f, 0.5f)]
    public float base_duration_ = 0.05f;
    bool is_frozen_ = false;

    // Update is called once per frame
    void Update()
    {
        if(freeze_duration_timer_>0 && !is_frozen_)
        {
            StartCoroutine(DoFreeze());
        }
    }

    float freeze_duration_timer_ = 0f;

    public void Freeze(float damage_done_)
    {
        freeze_duration_timer_ = base_duration_*damage_done_;
    }

    IEnumerator DoFreeze()
    {
        is_frozen_ = true;
        float original_time_ = Time.timeScale;
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(freeze_duration_timer_);

        Time.timeScale = original_time_;
        freeze_duration_timer_ = 0;
        is_frozen_ = false;
    }
}
