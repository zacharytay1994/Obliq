using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_6ManagerScript : MonoBehaviour
{
    GameObject bomb_objective_, portal_, portal_manager_, player_;

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

        bomb_objective_ = GameObject.Find("Bomb");

        portal_ = GameObject.Find("Portal");
        portal_manager_ = GameObject.Find("PortalManager");
        portal_script_ = portal_manager_.GetComponent<Portal>();

        player_ = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //When bomb is defused, activate portal. When in range of portal, transport player to next level.
        if (bomb_objective_ == null && activate_portal_ == false)
        {
            // Activate portal
            portal_script_.SetActivatePortal(true);
            activate_portal_ = true;
        }

        // Check distance between player and portal
        Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
        if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true)
        {
            STM_.load_scene_Asynch("1-7");
        }
    }
}
