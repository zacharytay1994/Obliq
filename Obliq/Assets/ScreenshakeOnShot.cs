using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshakeOnShot : MonoBehaviour
{
    CameraManager cam_;
    HitPause hpause_;
    bool key_pressed_;
    bool prev_key_pressed_;
    // Start is called before the first frame update
    void Start()
    {
        cam_ = FindObjectOfType<CameraManager>();
        hpause_ = FindObjectOfType<HitPause>();
        key_pressed_ = false;
        prev_key_pressed_ = true;
    }

    // Update is called once per frame
    void Update()
    {
        key_pressed_ = false;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("PRESSED");
            
            key_pressed_ = true; 
        }

        if(prev_key_pressed_== false && key_pressed_ == true)
        {
            cam_.Shake(1);
        }

        prev_key_pressed_ = key_pressed_;
    }
}
