using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    [Header("SELECT LEVEL")]
    [SerializeField]
    LevelSelector level_selector_ = LevelSelector.Tutorial;

    // Player
    GameObject player_;

    // Objective indicator
    [SerializeField]
    GameObject objective_indicator_;
    GameObject objective_indicator_parent_;
    GameObject[] objective_indicator_list_;

    // Manager for scene transition to next scene
    SceneTransitionLoader STM_;

    // Portal
    GameObject portal_, portal_manager_;
    Portal portal_script_;
    bool activate_portal_ = false;

    // Spawners
    GameObject spawner_1_, spawner_2_, spawner_3_, spawner_4_, spawner_5_, spawner_6_, spawner_7_, spawner_8_;

    // Chargers
    GameObject charger_1_, charger_2_, charger_3_;

    // Sectopod
    GameObject sectopod_;

    // Capture point (Objective)
    GameObject capture_point_;
    bool captured_;

    // List of enemies
    GameObject[] enemies_list_;

    //----------------------Tutorial-----------------------------
    // Training grunts
    GameObject training_grunt_, training_grunt_2_, training_grunt_3_;
    HealthComponent training_grunt_2_health_component_, training_grunt_3_health_component_;
    int training_grunt_2_max_hp_, training_grunt_3_max_hp_;

    // Timer after training grunt dies
    float wait_time_ = 1.0f;
    float timer_ = 0.0f;
    bool activate_timer_ = false;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize Scene Transition Loader
        STM_ = FindObjectOfType<SceneTransitionLoader>();

        // Initialize player
        player_ = GameObject.Find("Player");

        // Initialize objective indicator
        objective_indicator_ = GameObject.Find("ObjectiveIndicator");

        // Initialize portal
        portal_ = GameObject.Find("Portal");
        portal_manager_ = GameObject.Find("PortalManager");
        portal_script_ = portal_manager_.GetComponent<Portal>();

        // 1-0
        if (level_selector_ == LevelSelector.Tutorial)
        {
            // Initialize first training grunt
            training_grunt_ = GameObject.Find("TrainingGrunt");

            // Initialize second training grunt
            training_grunt_2_ = GameObject.Find("TrainingGrunt 2");
            training_grunt_2_health_component_ = training_grunt_2_.GetComponent<HealthComponent>();
            training_grunt_2_max_hp_ = training_grunt_2_health_component_.getMaxHp();
            training_grunt_2_.SetActive(false);

            // Initialize third training grunt
            training_grunt_3_ = GameObject.Find("TrainingGrunt 3");
            training_grunt_3_health_component_ = training_grunt_3_.GetComponent<HealthComponent>();
            training_grunt_3_max_hp_ = training_grunt_3_health_component_.getMaxHp();
            training_grunt_3_.SetActive(false);
        }

        // 1-1
        else if (level_selector_ == LevelSelector.One)
        {
            // Initialize spawners
            spawner_1_ = GameObject.Find("Spawner");
            spawner_2_ = GameObject.Find("Spawner 2");
        }

        // 1-2
        else if (level_selector_ == LevelSelector.Two)
        {
            // Initialize spawners
            spawner_1_ = GameObject.Find("Spawner");
            spawner_2_ = GameObject.Find("Spawner 2");
            spawner_3_ = GameObject.Find("Spawner 3");
            spawner_4_ = GameObject.Find("Spawner 4");
        }
        
        // 1-3, 1-4 or 1-9
        else if (level_selector_ == LevelSelector.Three || level_selector_ == LevelSelector.Four || level_selector_ == LevelSelector.Nine)
        {
            // Initialize capture point (Objective)
            capture_point_ = GameObject.Find("CapturePoint");
        }

        // 1-5
        else if (level_selector_ == LevelSelector.Five)
        {
            // Initialize charger
            charger_1_ = GameObject.Find("Charger");
        }

        // 1-6
        else if (level_selector_ == LevelSelector.Six)
        {
            // Initialize chargers
            charger_1_ = GameObject.Find("Charger");
            charger_2_ = GameObject.Find("Charger 2");
            charger_3_ = GameObject.Find("Charger 3");
        }

        // 1-7
        else if (level_selector_ == LevelSelector.Seven)
        {
            // Initialize chargers and spawners
            charger_1_ = GameObject.Find("Charger");
            charger_2_ = GameObject.Find("Charger 2");
            spawner_1_ = GameObject.Find("Spawner");
            spawner_2_ = GameObject.Find("Spawner 2");
            spawner_3_ = GameObject.Find("Spawner 3");
            spawner_4_ = GameObject.Find("Spawner 4");
            spawner_5_ = GameObject.Find("Spawner 5");
            spawner_6_ = GameObject.Find("Spawner 6");
            spawner_7_ = GameObject.Find("Spawner 7");
            spawner_8_ = GameObject.Find("Spawner 8");
        }

        // 1-8
        else if (level_selector_ == LevelSelector.Eight)
        {
            // Initialize spawners and sectopod
            spawner_1_ = GameObject.Find("Spawner");
            spawner_2_ = GameObject.Find("Spawner 2");
            spawner_3_ = GameObject.Find("Spawner 3");
            spawner_4_ = GameObject.Find("Spawner 4");
            spawner_5_ = GameObject.Find("Spawner 5");
            spawner_6_ = GameObject.Find("Spawner 6");
            sectopod_ = GameObject.Find("Sectopod");
        }

        // 1-10
        else if (level_selector_ == LevelSelector.Ten)
        {
            // Initialize spawners, sectopod and chargers
            spawner_1_ = GameObject.Find("Spawner");
            spawner_2_ = GameObject.Find("Spawner 2");
            spawner_3_ = GameObject.Find("Spawner 3");
            spawner_4_ = GameObject.Find("Spawner 4");
            spawner_5_ = GameObject.Find("Spawner 5");
            spawner_6_ = GameObject.Find("Spawner 6");
            spawner_7_ = GameObject.Find("Spawner 7");
            spawner_8_ = GameObject.Find("Spawner 8");
            sectopod_ = GameObject.Find("Sectopod");
            charger_1_ = GameObject.Find("Charger");
            charger_2_ = GameObject.Find("Charger 2");
            charger_3_ = GameObject.Find("Charger 3");

        }

        // Enemy list
        enemies_list_ = GameObject.FindGameObjectsWithTag("Enemy");

        objective_indicator_parent_ = GameObject.Find("Player_UI");

        foreach (GameObject enemy in enemies_list_)
        {
            GameObject gameobject_temp = GameObject.Instantiate(objective_indicator_, objective_indicator_parent_.transform);

            gameobject_temp.GetComponent<ObjectiveIndicator>().objective_ = enemy;
        }

        /*----------WHEN WE IMPLEMENT TRIGGER THEN UNCOMMENT----------*/
        /*// Disable all enemies at the start
        enemies_list_ = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies_list_)
        {
            enemy.SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        /*----------WHEN WE IMPLEMENT TRIGGER THEN UNCOMMENT----------*/
        /*// If walk over trigger, make all enemies active
        bool is_triggered_ = GameObject.Find("ActivateTriggerTilemap").GetComponent<ActivateEnemies>().activate_enemies_;

        if (is_triggered_)
        {
            foreach (GameObject enemy in enemies_list_)
            {
                enemy.SetActive(true);
            }
        }*/

        objective_indicator_list_ = GameObject.FindGameObjectsWithTag("ObjectiveIndicator");

        foreach (GameObject o in objective_indicator_list_)
        {
            if (o.GetComponent<ObjectiveIndicator>().objective_.name.Contains("Charger"))
            {
                o.GetComponent<SpriteRenderer>().color = o.GetComponent<ObjectiveIndicator>().objective_.GetComponent<SpriteRenderer>().color;
            }
        }        

        // Capture Point Levels
        if (level_selector_ == LevelSelector.Three || level_selector_ == LevelSelector.Four || level_selector_ == LevelSelector.Nine)
        {
            // Check if capture point is captured
            captured_ = capture_point_.GetComponent<CapturePoint>().captured_;

            // When capture point is captured, activate portal. When in range of portal, transport player to next level.
            if (captured_ == true && activate_portal_ == false)
            {
                // Activate portal
                portal_script_.SetActivatePortal(true);
                activate_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true)
            {
                if (level_selector_ == LevelSelector.Three)
                    STM_.load_scene_Asynch("1-4");

                if (level_selector_ == LevelSelector.Four)
                    STM_.load_scene_Asynch("1-5");

                if (level_selector_ == LevelSelector.Nine)
                    STM_.load_scene_Asynch("1-10");
            }
        }

        // 1-0
        if (level_selector_ == LevelSelector.Tutorial)
        {
            // If first training grunt is killed, spawn 2 and 3 after 1s, freeze player
            if (training_grunt_ != null)
            {
                if (training_grunt_.GetComponent<HealthComponent>().getCurrentHp() <= 0)
                {
                    // Activate timer
                    activate_timer_ = true;
                }
            }

            // Activate timer
            if (activate_timer_ == true)
            {
                timer_ += Time.deltaTime;
            }

            // After 1s
            if (timer_ >= wait_time_)
            {
                timer_ = wait_time_;

                // If not killed, set them to active
                if (training_grunt_2_ != null && training_grunt_3_ != null)
                {
                    training_grunt_2_.SetActive(true);
                    training_grunt_3_.SetActive(true);
                }
            }

            // If near player, slow down by 90%
            if (training_grunt_2_ != null && training_grunt_3_ != null)
            {
                int training_grunt_2_current_hp_ = training_grunt_2_health_component_.getCurrentHp();
                int training_grunt_3_current_hp_ = training_grunt_3_health_component_.getCurrentHp();

                Vector2 training_grunt_dist_ = training_grunt_3_.GetComponent<Transform>().position - training_grunt_2_.GetComponent<Transform>().position;

                training_grunt_2_.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
                training_grunt_3_.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);

                // Slow down within range of player
                if (training_grunt_dist_.magnitude < 8.0f)
                {
                    training_grunt_2_.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    training_grunt_3_.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
            }

            // After both grunts are killed
            else if (training_grunt_2_ == null && training_grunt_3_ == null && activate_portal_ == false)
            {
                portal_script_.SetActivatePortal(true);
                activate_portal_ = true;
            }

            // Portal
            Vector2 dist_to_portal_ = (Vector2)player_.transform.position - (Vector2)portal_.transform.position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true)
            {
                STM_.load_scene_Asynch("1-1");
            }
        }

        // 1-1
        else if (level_selector_ == LevelSelector.One)
        {
            // When the two top spawners are destroyed, activate portal. When in range of portal, transport player to next level.
            if (spawner_1_ == null && spawner_2_ == null && activate_portal_ == false)
            {
                portal_script_.SetActivatePortal(true);
                activate_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeInHierarchy == true)
            {
                STM_.load_scene_Asynch("1-2");
            }
        }

        // 1-2
        else if (level_selector_ == LevelSelector.Two)
        {
            // When the four top spawners are destroyed, activate portal. When in range of portal, transport player to next level.
            if (spawner_1_ == null && spawner_2_ == null && spawner_3_ == null && spawner_4_ == null && activate_portal_ == false)
            {
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

        // 1-5
        else if (level_selector_ == LevelSelector.Five)
        {
            // When charger is killed, activate portal. When in range of portal, transport player to next level.
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
                STM_.load_scene_Asynch("1-6");
            }
        }

        // 1-6
        else if (level_selector_ == LevelSelector.Six)
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
                STM_.load_scene_Asynch("1-7");
            }
        }

        // 1-7
        else if (level_selector_ == LevelSelector.Seven)
        {
            // When chargers are killed and spawners are destroyed, activate portal. When in range of portal, transport player to next level.
            if (charger_1_ == null && charger_2_ == null && spawner_1_ == null && spawner_2_ == null && spawner_3_ == null && spawner_4_ == null 
                && spawner_5_ == null && spawner_6_ == null && spawner_7_ == null && spawner_8_ == null && activate_portal_ == false)
            {
                // Activate portal
                portal_script_.SetActivatePortal(true);
                activate_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true)
            {
                STM_.load_scene_Asynch("1-8");
            }
        }

        // 1-8
        else if (level_selector_ == LevelSelector.Eight)
        {
            // When chargers are killed and spawners are destroyed, activate portal. When in range of portal, transport player to next level.
            if (spawner_1_ == null && spawner_2_ == null && spawner_3_ == null && spawner_4_ == null && spawner_5_ == null 
                && spawner_6_ == null && sectopod_ == null && activate_portal_ == false)
            {
                // Activate portal
                portal_script_.SetActivatePortal(true);
                activate_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true)
            {
                STM_.load_scene_Asynch("1-9");
            }
        }

        // 1-10
        else if (level_selector_ == LevelSelector.Ten)
        {
            // When chargers and sectopod are killed and spawners are destroyed, activate portal. When in range of portal, transport player to next level.
            if (spawner_1_ == null && spawner_2_ == null && spawner_3_ == null && spawner_4_ == null && spawner_5_ == null
                && spawner_6_ == null && spawner_7_ == null && spawner_8_ == null && sectopod_ == null
                &&charger_1_ == null && charger_2_ == null && charger_3_ == null && activate_portal_ == false)
            {
                // Activate portal
                portal_script_.SetActivatePortal(true);
                activate_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true)
            {
                //STM_.load_scene_Asynch("1-Boss");
            }
        }
    }
}
