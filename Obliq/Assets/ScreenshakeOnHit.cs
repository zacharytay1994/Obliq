using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshakeOnHit : MonoBehaviour
{
    CameraManager cam_;
    HitPause hpause_;
    HealthComponent hc_;
    int current_hp_;
    int prev_hp_;
    
    // Start is called before the first frame update
    void Start()
    {
        cam_=FindObjectOfType<CameraManager>();
        hpause_ = FindObjectOfType<HitPause>();
        hc_ = GetComponent<HealthComponent>();
        current_hp_ = hc_.getCurrentHp();
        prev_hp_ = current_hp_;
    }

    // Update is called once per frame
    void Update()
    {
        current_hp_ = hc_.getCurrentHp();
        if(prev_hp_>current_hp_)
        {
            cam_.Shake();
            hpause_.Freeze(1);

        }
        prev_hp_ = current_hp_;
    }
}
