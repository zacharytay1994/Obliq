using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Bool check for making portal active
    bool activate_portal_;

    // Audio Manager
    string portal_sound_ = "Portal";
    AudioManager am_;

    // Portal GameObject
    GameObject portal_;

    // Start is called before the first frame update
    void Start()
    {
        // Portal
        activate_portal_ = false;
        portal_ = GameObject.Find("Portal");

        // Audio Manager
        am_ = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activate_portal_ == false)
        {
            portal_.SetActive(false);
        }
        else
        {
            portal_.SetActive(true);
        }
    }

    public void SetActivatePortal(bool set_portal_active_)
    {
        Debug.Log("Portal Sound");
        am_.PlaySound(portal_sound_);
        activate_portal_ = set_portal_active_;
        
    }
}
