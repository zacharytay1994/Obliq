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
    int target_count_;
    float indicator_offset_;
    float indicator_fade_range = 20;

    // Manager for scene transition to next scene
    SceneTransitionLoader STM_;

    // Portal
    GameObject portal_, portal_manager_;
    float portal_activation_time = 3.0f;
    float portal_timer_ = 0.0f;
    bool activate_portal_timer_ = false;
    bool can_enter_portal_ = false;

    // Spawners
    GameObject spawner_1_, spawner_2_, spawner_3_, spawner_4_, spawner_5_, spawner_6_, spawner_7_, spawner_8_;

    // Chargers
    GameObject charger_1_, charger_2_, charger_3_;

    // Sectopod
    GameObject sectopod_;

    // Boss
    GameObject boss_;

    // Capture point (Objective)
    GameObject capture_point_;
    bool captured_;

    // List of enemies / objectives (e.g. Spawners, Capture Points)
    GameObject[] targets_list_;

    // Check to stop the enabling of enemies
    bool stop_enemies_enable_ = false;

    //----------------------Tutorial-----------------------------
    // Training grunts
    GameObject training_grunt_, training_grunt_2_, training_grunt_3_;
    HealthComponent training_grunt_2_health_component_, training_grunt_3_health_component_;
    int training_grunt_2_max_hp_, training_grunt_3_max_hp_;
    bool activate_training_grunts_ = false;

    // Timer after training grunt dies
    float traininggrunt_wait_time_ = 1.0f;
    float traininggrunt_timer_ = 0.0f;
    bool activate_traininggrunt_timer_ = false;


    // Start is called before the first frame update
    void Start()
    {
        //------------------------------INITIALIZING GENERAL GAME OBJECTS------------------------------
        // Initialize Scene Transition Loader
        STM_ = FindObjectOfType<SceneTransitionLoader>();

        // Initialize player
        player_ = GameObject.Find("Player");

        // Initialize objective indicator
        objective_indicator_ = GameObject.Find("ObjectiveIndicator");

        // Initialize portal
        portal_ = GameObject.Find("Portal");
        portal_manager_ = GameObject.Find("PortalManager");
        portal_.SetActive(false);

        // Spawn objecive indicators for each target
        objective_indicator_parent_ = player_;

        //------------------------------INITIALIZING LEVEL-SPECIFIC GAME OBJECTS------------------------------
        // If objective of the level is to capture the capture point, initialize the capture point.
        // Else, initialize enemies.
        // 1-3, 1-4 or 1-9
        if (GameObject.FindGameObjectsWithTag("Objective").Length != 0)
        {
            // Initialize capture point (Objective) and set captured_ to false on start
            capture_point_ = GameObject.Find("CapturePoint");
            capture_point_.GetComponent<CapturePoint>().captured_ = false;
        }
        else
        {
            // 1-0
            if (level_selector_ == LevelSelector.Tutorial)
            {
                // Initialize first training grunt
                training_grunt_ = GameObject.Find("TrainingGrunt");

                // Initialize second training grunt
                training_grunt_2_ = GameObject.Find("TrainingGrunt 2");
                training_grunt_2_health_component_ = training_grunt_2_.GetComponent<HealthComponent>();
                training_grunt_2_max_hp_ = training_grunt_2_health_component_.getMaxHp();

                // Initialize third training grunt
                training_grunt_3_ = GameObject.Find("TrainingGrunt 3");
                training_grunt_3_health_component_ = training_grunt_3_.GetComponent<HealthComponent>();
                training_grunt_3_max_hp_ = training_grunt_3_health_component_.getMaxHp();
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

            // Boss level
            else if (level_selector_ == LevelSelector.Boss)
            {
                // Initialize boss
                boss_ = GameObject.Find("BossBody");
            }
        }

        //------------------------------ASSIGNING OBJECTIVE INDICATORS TO EACH TARGET------------------------------
        // For levels except the boss level (do not create indicators in the boss level)
        if (level_selector_ != LevelSelector.Boss)
        {
            // If level is a capture point level, create indicator for capture point (objective)
            if (GameObject.FindGameObjectsWithTag("Objective").Length != 0)
            {
                // Objective list
                targets_list_ = GameObject.FindGameObjectsWithTag("Objective");
            }
            else
            {
                // Enemy list
                targets_list_ = GameObject.FindGameObjectsWithTag("Enemy");
            }

            // Set how far the indicator is from the player based on the number of targets
            target_count_ = targets_list_.Length;

            if (target_count_ <= 6)
            {
                indicator_offset_ = 2;
            }
            else
            {
                indicator_offset_ = 5;
            }

            // Initialize an indicator for each target
            foreach (GameObject target in targets_list_)
            {
                GameObject gameobject_temp = GameObject.Instantiate(objective_indicator_, objective_indicator_parent_.transform);
                gameobject_temp.GetComponent<ObjectiveIndicator>().SetObjective(target);
                gameobject_temp.GetComponent<ObjectiveIndicator>().offset_amount_ = indicator_offset_;
            }

            // Disable all enemies at the start
            if (GameObject.FindGameObjectsWithTag("Objective").Length == 0)
            {
                foreach (GameObject enemy in targets_list_)
                {
                    if (enemy.name != "CapturePoint")
                    {
                        enemy.SetActive(false);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //------------------------CHEAT CODE TO MOVE TO LEVEL----------------------------------
        if (Input.GetKeyDown(KeyCode.F1))
        {
            STM_.load_scene_Asynch("1-0");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            STM_.load_scene_Asynch("1-1");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            STM_.load_scene_Asynch("1-2");
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            STM_.load_scene_Asynch("1-3");
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            STM_.load_scene_Asynch("1-4");
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            STM_.load_scene_Asynch("1-5");
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            STM_.load_scene_Asynch("1-6");
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            STM_.load_scene_Asynch("1-7");
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            STM_.load_scene_Asynch("1-8");
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            STM_.load_scene_Asynch("1-9");
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            STM_.load_scene_Asynch("1-10");
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            STM_.load_scene_Asynch("1-Boss");
        }
        //------------------------CHEAT CODE END DELETE DIS----------------------------------

        // If level is not boss level
        if (level_selector_ != LevelSelector.Boss)
        {
            // If level has no capture point (objective is to kill all enemies)
            if (GameObject.FindGameObjectsWithTag("Objective").Length == 0)
            {
                // If walk over trigger, make all enemies active
                bool is_triggered_ = GameObject.Find("ActivateTriggerTilemap").GetComponent<ActivateEnemies>().activate_enemies_;

                if (is_triggered_ && !stop_enemies_enable_)
                {
                    foreach (GameObject enemy in targets_list_)
                    {
                        enemy.SetActive(true);
                    }

                    stop_enemies_enable_ = true;
                }
            }

            // Change color if indicator's target is a charger
            objective_indicator_list_ = GameObject.FindGameObjectsWithTag("ObjectiveIndicator");

            foreach (GameObject o in objective_indicator_list_)
            {
                if (o.GetComponent<ObjectiveIndicator>() != null)
                {
                    if (o.GetComponent<ObjectiveIndicator>().GetObjective() != null)
                    {
                        // Change color if target is charger
                        if (o.GetComponent<ObjectiveIndicator>().GetObjective().name.Contains("Charger"))
                        {
                            float charger_color_r = o.GetComponent<ObjectiveIndicator>().GetObjective().GetComponent<SpriteRenderer>().color.r;
                            float charger_color_g = o.GetComponent<ObjectiveIndicator>().GetObjective().GetComponent<SpriteRenderer>().color.g;
                            float charger_color_b = o.GetComponent<ObjectiveIndicator>().GetObjective().GetComponent<SpriteRenderer>().color.b;

                            o.GetComponent<Transform>().GetChild(0).GetComponent<SpriteRenderer>().color =
                                new Color(charger_color_r, charger_color_g, charger_color_b);
                        }

                        Vector2 vector_from_indicator_to_target = o.GetComponent<ObjectiveIndicator>().GetObjective().transform.position - o.transform.position;

                        if (vector_from_indicator_to_target.magnitude <= indicator_fade_range)
                        {
                            float indicator_color_r = o.GetComponent<Transform>().GetChild(0).GetComponent<SpriteRenderer>().color.r;
                            float indicator_color_g = o.GetComponent<Transform>().GetChild(0).GetComponent<SpriteRenderer>().color.g;
                            float indicator_color_b = o.GetComponent<Transform>().GetChild(0).GetComponent<SpriteRenderer>().color.b;
                            float indicator_color_a = (255 / indicator_fade_range * vector_from_indicator_to_target.magnitude) / 255;

                            o.GetComponent<Transform>().GetChild(0).GetComponent<SpriteRenderer>().color =
                                new Color(indicator_color_r, indicator_color_g, indicator_color_b, indicator_color_a);
                        }
                    }
                }
            }
        }
        
        // Capture Point Levels
        if (GameObject.FindGameObjectsWithTag("Objective").Length != 0 && level_selector_ != LevelSelector.Boss)
        {
            // Get status of capture point (captured/not captured)
            captured_ = capture_point_.GetComponent<CapturePoint>().captured_;

            // When capture point is captured, activate portal. When in range of portal, transport player to next level.
            if (captured_ == true && portal_.activeSelf == false)
            {
                portal_.SetActive(true);
            }

            // When portal is active, start timer for entering portal (time before player can enter portal)
            if (portal_.activeSelf == true)
            {
                portal_timer_ += Time.deltaTime;
            }

            // When portal timer reaches defined time, player can enter portal
            if (portal_timer_ >= portal_activation_time)
            {
                portal_timer_ = portal_activation_time;
                can_enter_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true && can_enter_portal_ == true)
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
        else if (level_selector_ == LevelSelector.Tutorial)
        {
            // Disable training grunts 2 & 3 on start
            if (!activate_training_grunts_)
            {
                training_grunt_2_.SetActive(false);
                training_grunt_3_.SetActive(false);
            }

            // If first training grunt is killed, spawn 2 and 3
            if (training_grunt_ == null)
            {
                // Activate timer
                activate_traininggrunt_timer_ = true;
                activate_training_grunts_ = true;

            }

            // Activate timer
            if (activate_traininggrunt_timer_ == true)
            {
                traininggrunt_timer_ += Time.deltaTime;
            }

            // After 1s
            if (traininggrunt_timer_ >= traininggrunt_wait_time_)
            {
                traininggrunt_timer_ = traininggrunt_wait_time_;

                // If not killed, set them to active
                if (training_grunt_2_ != null && training_grunt_3_ != null && activate_training_grunts_ == true)
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

            // After both grunts are killed, activate portal. When in range of portal, transport player to next level.
            else if (training_grunt_2_ == null && training_grunt_3_ == null && portal_.activeSelf == false)
            {
                portal_.SetActive(true);
            }

            // When portal is active, start timer for entering portal (time before player can enter portal)
            if (portal_.activeSelf == true)
            {
                portal_timer_ += Time.deltaTime;
            }

            // When portal timer reaches defined time, player can enter portal
            if (portal_timer_ >= portal_activation_time)
            {
                portal_timer_ = portal_activation_time;
                can_enter_portal_ = true;
            }

            // Portal
            Vector2 dist_to_portal_ = (Vector2)player_.transform.position - (Vector2)portal_.transform.position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true && can_enter_portal_ == true)
            {
                STM_.load_scene_Asynch("1-1");
            }
        }

        // 1-1
        else if (level_selector_ == LevelSelector.One)
        {
            // When the two top spawners are destroyed, activate portal. When in range of portal, transport player to next level.
            if (spawner_1_ == null && spawner_2_ == null && portal_.activeSelf == false)
            {
                portal_.SetActive(true);
            }

            // When portal is active, start timer for entering portal (time before player can enter portal)
            if (portal_.activeSelf == true)
            {
                portal_timer_ += Time.deltaTime;
            }

            // When portal timer reaches defined time, player can enter portal
            if (portal_timer_ >= portal_activation_time)
            {
                portal_timer_ = portal_activation_time;
                can_enter_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true && can_enter_portal_ == true)
            {
                STM_.load_scene_Asynch("1-2");
            }
        }

        // 1-2
        else if (level_selector_ == LevelSelector.Two)
        {
            // When the four top spawners are destroyed, activate portal. When in range of portal, transport player to next level.
            if (spawner_1_ == null && spawner_2_ == null && spawner_3_ == null && spawner_4_ == null && portal_.activeSelf == false)
            {
                portal_.SetActive(true);
            }

            // When portal is active, start timer for entering portal (time before player can enter portal)
            if (portal_.activeSelf == true)
            {
                portal_timer_ += Time.deltaTime;
            }

            // When portal timer reaches defined time, player can enter portal
            if (portal_timer_ >= portal_activation_time)
            {
                portal_timer_ = portal_activation_time;
                can_enter_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true && can_enter_portal_ == true)
            {
                STM_.load_scene_Asynch("1-3");
            }
        }

        // 1-5
        else if (level_selector_ == LevelSelector.Five)
        {
            // When charger is killed, activate portal. When in range of portal, transport player to next level.
            if (charger_1_ == null && portal_.activeSelf == false)
            {
                portal_.SetActive(true);
            }

            // When portal is active, start timer for entering portal (time before player can enter portal)
            if (portal_.activeSelf == true)
            {
                portal_timer_ += Time.deltaTime;
            }

            // When portal timer reaches defined time, player can enter portal
            if (portal_timer_ >= portal_activation_time)
            {
                portal_timer_ = portal_activation_time;
                can_enter_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true && can_enter_portal_ == true)
            {
                STM_.load_scene_Asynch("1-6");
            }
        }

        // 1-6
        else if (level_selector_ == LevelSelector.Six)
        {
            // When all chargers are killed, activate portal. When in range of portal, transport player to next level.
            if (charger_1_ == null && charger_2_ == null && charger_3_ == null && portal_.activeSelf == false)
            {
                portal_.SetActive(true);
            }

            // When portal is active, start timer for entering portal (time before player can enter portal)
            if (portal_.activeSelf == true)
            {
                portal_timer_ += Time.deltaTime;
            }

            // When portal timer reaches defined time, player can enter portal
            if (portal_timer_ >= portal_activation_time)
            {
                portal_timer_ = portal_activation_time;
                can_enter_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true && can_enter_portal_ == true)
            {
                STM_.load_scene_Asynch("1-7");
            }
        }

        // 1-7
        else if (level_selector_ == LevelSelector.Seven)
        {
            // When chargers are killed and spawners are destroyed, activate portal. When in range of portal, transport player to next level.
            if (charger_1_ == null && charger_2_ == null && spawner_1_ == null && spawner_2_ == null && spawner_3_ == null && spawner_4_ == null 
                && spawner_5_ == null && spawner_6_ == null && portal_.activeSelf == false)
            {
                portal_.SetActive(true);
            }

            // When portal is active, start timer for entering portal (time before player can enter portal)
            if (portal_.activeSelf == true)
            {
                portal_timer_ += Time.deltaTime;
            }

            // When portal timer reaches defined time, player can enter portal
            if (portal_timer_ >= portal_activation_time)
            {
                portal_timer_ = portal_activation_time;
                can_enter_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true && can_enter_portal_ == true)
            {
                STM_.load_scene_Asynch("1-8");
            }
        }

        // 1-8
        else if (level_selector_ == LevelSelector.Eight)
        {
            // When chargers are killed and spawners are destroyed, activate portal. When in range of portal, transport player to next level.
            if (spawner_1_ == null && spawner_2_ == null && spawner_3_ == null && spawner_4_ == null && spawner_5_ == null 
                && spawner_6_ == null && sectopod_ == null && portal_.activeSelf == false)
            {
                portal_.SetActive(true);
            }

            // When portal is active, start timer for entering portal (time before player can enter portal)
            if (portal_.activeSelf == true)
            {
                portal_timer_ += Time.deltaTime;
            }

            // When portal timer reaches defined time, player can enter portal
            if (portal_timer_ >= portal_activation_time)
            {
                portal_timer_ = portal_activation_time;
                can_enter_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true && can_enter_portal_ == true)
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
                &&charger_1_ == null && charger_2_ == null && charger_3_ == null && portal_.activeSelf == false)
            {
                portal_.SetActive(true);
            }

            // When portal is active, start timer for entering portal (time before player can enter portal)
            if (portal_.activeSelf == true)
            {
                portal_timer_ += Time.deltaTime;
            }

            // When portal timer reaches defined time, player can enter portal
            if (portal_timer_ >= portal_activation_time)
            {
                portal_timer_ = portal_activation_time;
                can_enter_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true && can_enter_portal_ == true)
            {
                //STM_.load_scene_Asynch("1-Boss");
                STM_.load_scene_Asynch("CreditScene");
            }
        }

        // Boss level
        else if (level_selector_ == LevelSelector.Boss)
        {
            // When chargers and sectopod are killed and spawners are destroyed, activate portal. When in range of portal, transport player to next level.
            if (boss_.activeSelf == true && boss_.GetComponent<BossBody>().boss_defeated_ == true && portal_.activeSelf == false)
            {
                portal_.SetActive(true);
                boss_.SetActive(false);
            }

            // When portal is active, start timer for entering portal (time before player can enter portal)
            if (portal_.activeSelf == true)
            {
                portal_timer_ += Time.deltaTime;
            }

            // When portal timer reaches defined time, player can enter portal
            if (portal_timer_ >= portal_activation_time)
            {
                portal_timer_ = portal_activation_time;
                can_enter_portal_ = true;
            }

            // Check distance between player and portal
            Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
            if (dist_to_portal_.magnitude <= 3.0f && portal_.activeSelf == true && can_enter_portal_ == true)
            {
                STM_.load_scene_Asynch("CreditScene");
            }
        }
    }
}
