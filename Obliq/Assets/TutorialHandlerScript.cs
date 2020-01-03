using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHandlerScript : MonoBehaviour
{
    // Training grunts
    GameObject training_grunt_, training_grunt_2_, training_grunt_3_, player_, portal_;

    // Timer after training grunt dies
    float wait_time_ = 1.0f;
    float timer_ = 0.0f;
    bool activate_timer_ = false;

    // Start is called before the first frame update
    void Start()
    {
        training_grunt_ = GameObject.Find("TrainingGrunt");

        training_grunt_2_ = GameObject.Find("TrainingGrunt 2");
        training_grunt_2_.SetActive(false);

        training_grunt_3_ = GameObject.Find("TrainingGrunt 3");
        training_grunt_3_.SetActive(false);

        player_ = GameObject.Find("Player");

        portal_ = GameObject.Find("Portal");
        portal_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If first training grunt is killed, spawn 2 and 3 after 1s, freeze player
        if (training_grunt_ != null)
        {
            if (training_grunt_.GetComponent<HealthComponent>().getCurrentHp() <= 0)
            {
                // Activate timer
                activate_timer_ = true;

                // Freeze player
                player_.GetComponent<PlayerController>().SetAcceleration(0f);
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
            Vector2 training_grunt_2_dist_ = training_grunt_2_.GetComponent<Transform>().position - player_.GetComponent<Transform>().position;
            Vector2 training_grunt_3_dist_ = training_grunt_3_.GetComponent<Transform>().position - player_.GetComponent<Transform>().position;

            training_grunt_2_.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            training_grunt_3_.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);

            if (training_grunt_2_dist_.magnitude < 5.0f)
            {
                training_grunt_2_.GetComponent<Rigidbody2D>().velocity -= training_grunt_2_.GetComponent<Rigidbody2D>().velocity * 0.9f;
            }
            if (training_grunt_3_dist_.magnitude < 5.0f)
            {
                training_grunt_3_.GetComponent<Rigidbody2D>().velocity -= training_grunt_3_.GetComponent<Rigidbody2D>().velocity * 0.9f;
            }
        }
        else if (training_grunt_2_ == null && training_grunt_3_ == null)
        {
            player_.GetComponent<PlayerController>().SetAcceleration(30.0f);
            portal_.SetActive(true);
        }

        // Portal
        Vector2 dist_to_portal_ = player_.GetComponent<Transform>().position - portal_.GetComponent<Transform>().position;
        if (dist_to_portal_.magnitude <= 3.0f)
        {
            SceneManager.LoadScene("1-1");
        }
    }
}
