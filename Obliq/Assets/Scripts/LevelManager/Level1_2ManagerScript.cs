using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_2ManagerScript : MonoBehaviour
{
    GameObject charger_1_, charger_2_, charger_3_, portal_, portal_manager_, player_;

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
        charger_2_ = GameObject.Find("Charger 2");
        charger_3_ = GameObject.Find("Charger 3");

        player_ = GameObject.Find("Player");

        portal_ = GameObject.Find("Portal");
        portal_manager_ = GameObject.Find("PortalManager");
        portal_script_ = portal_manager_.GetComponent<Portal>();
    }

    // Update is called once per frame
    void Update()
    {
        // When all chargers are killed, activate portal. When in range of portal, transport player to next level.
        if (charger_1_ == null && charger_2_ == null && charger_3_ == null && activate_portal_ == false)
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

        // If player dies first
        if (player_.GetComponent<HealthComponent>().getCurrentHp() <= 0)
        {
            STM_.load_scene_Asynch("1-2-1");
        }
    }
}
