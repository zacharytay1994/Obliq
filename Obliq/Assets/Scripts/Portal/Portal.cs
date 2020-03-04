using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Bool check for making portal active
    public bool activate_portal_;

    // Audio Manager
    string portal_sound_ = "Portal";
    AudioManager am_;
    bool sound_played_ = false;

    // Portal GameObject
    GameObject portal_;

    // Awake is called before the first frame update
    void Awake()
    {
        // Portal
        portal_ = GameObject.Find("Portal");

        // Audio Manager
        am_ = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (portal_.activeSelf == true && sound_played_ == false)
        {
            Debug.Log("Portal Sound");
            am_.PlaySound(portal_sound_);
            sound_played_ = true;
        }
    }
}