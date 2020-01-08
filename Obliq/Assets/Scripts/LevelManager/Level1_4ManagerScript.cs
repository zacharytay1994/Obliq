using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_4ManagerScript : MonoBehaviour
{
    GameObject charger_1_, spawner_1_, spawner_2_, portal_, portal_manager_, player_;

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
        spawner_1_ = GameObject.Find("Spawner");
        spawner_2_ = GameObject.Find("Spawner 2");

        portal_ = GameObject.Find("Portal");
        portal_manager_ = GameObject.Find("PortalManager");
        portal_script_ = portal_manager_.GetComponent<Portal>();

        player_ = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // When charger is killed and spawners are destroyed, activate portal. When in range of portal, transport player to next level.
        if (charger_1_ == null && spawner_1_ == null && spawner_2_ == null && activate_portal_ == false)
        {
            // Activate portal
            portal_script_.SetActivatePortal(true);
            activate_portal_ = true;
        }

        // Check distance between player and portal
        Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
        if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true)
        {
            STM_.load_scene_Asynch("1-5");
        }
    }
}
