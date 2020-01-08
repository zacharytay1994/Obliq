using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_2_1ManagerScript : MonoBehaviour
{
    GameObject charger_1_, portal_, portal_manager_, player_;

    // Manager for scene transition to next scene
    SceneTransitionLoader STM_;

    // Portal component
    Portal portal_script_;
    bool activate_portal_ = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise GameObjects and disable portal
        STM_ = FindObjectOfType<SceneTransitionLoader>();

        charger_1_ = GameObject.Find("Charger");

        player_ = GameObject.Find("Player");

        portal_ = GameObject.Find("Portal");
        portal_manager_ = GameObject.Find("PortalManager");
        portal_script_ = portal_manager_.GetComponent<Portal>();
    }

    // Update is called once per frame
    void Update()
    {
        // When all chargers are killed, activate portal. When in range of portal, transport player to next level.
        if (charger_1_ == null && activate_portal_ == false)
        {
            // Activate portal
            portal_script_.SetActivatePortal(true);
            activate_portal_ = true;
        }

        // Check distance between player and portal
        Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
        if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true)
        {
            STM_.load_scene_Asynch("1-3");
        }
    }
}
