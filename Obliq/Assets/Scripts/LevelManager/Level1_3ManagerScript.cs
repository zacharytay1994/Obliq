using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_3ManagerScript : MonoBehaviour
{
    GameObject bomb_objective_, portal_, player_;

    // Manager for scene transition to next scene
    SceneTransitionLoader STM_;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise GameObjects and disable portal
        STM_ = FindObjectOfType<SceneTransitionLoader>();
        bomb_objective_ = GameObject.Find("Bomb");
        portal_ = GameObject.Find("Portal");
        player_ = GameObject.Find("Player");
        portal_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // When bomb is defused, activate portal. When in range of portal, transport player to next level.
        if (bomb_objective_ == null)
        {
            // Activate portal
            portal_.SetActive(true);

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f)
            {
                STM_.load_scene_Asynch("1-4");
            }
        }
    }
}
