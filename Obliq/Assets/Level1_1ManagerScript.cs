using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1_1ManagerScript : MonoBehaviour
{
    GameObject spawner_1_, spawner_2_, spawner_3_, portal_, player_;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise GameObjects and disable portal
        spawner_1_ = GameObject.Find("Spawner");
        spawner_2_ = GameObject.Find("Spawner 2");
        spawner_3_ = GameObject.Find("Spawner 3");
        portal_ = GameObject.Find("Portal");
        player_ = GameObject.Find("Player");
        portal_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // When all spawners are destroyed, activate portal. When in range of portal, transport player to next level.
        if (spawner_1_ == null && spawner_2_ == null && spawner_3_ == null)
        {
            // Activate portal
            portal_.SetActive(true);

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f)
            {
                SceneManager.LoadScene("1-2");
            }
        }
    }
}
