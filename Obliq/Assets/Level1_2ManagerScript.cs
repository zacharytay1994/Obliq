using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1_2ManagerScript : MonoBehaviour
{
    GameObject charger_1_, charger_2_, charger_3_, portal_, player_;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise GameObjects and disable portal
        charger_1_ = GameObject.Find("Charger");
        charger_2_ = GameObject.Find("Charger 2");
        charger_3_ = GameObject.Find("Charger 3");
        portal_ = GameObject.Find("Portal");
        player_ = GameObject.Find("Player");
        portal_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // When all chargers are killed, activate portal. When in range of portal, transport player to next level.
        if (charger_1_ == null && charger_2_ == null && charger_3_ == null)
        {
            // Activate portal
            portal_.SetActive(true);

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f)
            {
                SceneManager.LoadScene("1-3");
            }
        }
    }
}
