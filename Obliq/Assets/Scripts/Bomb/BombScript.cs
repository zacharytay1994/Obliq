using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombScript : MonoBehaviour
{
    [SerializeField]
    float bomb_damage_;
    [SerializeField]
    float time_left_;
    float time_left_min_, time_left_sec_;
    bool stop_timer_ = false;
    bool bomb_explode_ = false;
    // reference to lists of entities
    public WorldHandler world_handler_reference_;
    // Bomb time remaining prefab
    [SerializeField]
    GameObject bomb_prefab_;
    [SerializeField]
    Text bomb_time_remaining_text_;

    // Start is called before the first frame update
    void Start()
    {
        // reference to lists of entities
        //world_handler_reference_ = GameObject.Find("World").GetComponent<WorldHandler>();

        //// Display timer
        //GameObject.Find("BombTimer").GetComponent<Text>().text = "Bomb timer: " + time_left_.ToString();

        // Display bomb time remaining
        time_left_min_ = (int)time_left_ / 60;
        time_left_sec_ = (int)time_left_ - (time_left_min_ * 60);
        bomb_time_remaining_text_.text = string.Format
            (bomb_prefab_.name + "\n" + "{0:00}:{1:00}", time_left_min_.ToString().ToString(), time_left_sec_.ToString());

        //// Display player health
        //GameObject.Find("GoodGuyHP").GetComponent<Text>().text = "Player health: " + GameObject.Find("Player").GetComponent<Entity>().health_.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //// Update timer
        //GameObject.Find("BombTimer").GetComponent<Text>().text = "Bomb timer: " + time_left_.ToString();

        // Display bomb time remaining
        time_left_min_ = (int)time_left_ / 60;
        time_left_sec_ = (int)time_left_ - (time_left_min_ * 60);
        bomb_time_remaining_text_.text = string.Format
            (bomb_prefab_.name + "\n" + "{0:00}:{1:00}", time_left_min_.ToString().ToString(), time_left_sec_.ToString());

        //// Update player health
        //GameObject.Find("GoodGuyHP").GetComponent<Text>().text = "Player health: " + GameObject.Find("Player").GetComponent<Entity>().health_.ToString();

        // When timer should not be stopped, continue to countdown
        if (!stop_timer_)
        {
            time_left_ -= Time.deltaTime;

            // When timer reaches 0, stop timer
            if (time_left_ <= 0)
            {
                time_left_ = 0;
                stop_timer_ = true;
            }
        }

        // When timer stops
        if ((time_left_ <= 0) && !bomb_explode_)
        {
            // Deal damage to all enemies
            //foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
            //{
            //    e.GetComponent<Entity>().TakeDamage(bomb_damage_);
            //}
            //foreach (GameObject g in GameObject.FindGameObjectWithTag("MainPlayer"))
            //{
            //    //g.GetComponent<Entity>().TakeDamage(bomb_damage_);
            //    g.GetComponent<HealthComponent>().TakeDamage((int)bomb_damage_);
            //}
            GameObject.Find("Player").GetComponent<HealthComponent>().TakeDamage((int)bomb_damage_);

            bomb_explode_ = true;
        }

        // Remove bomb after exploding
        else if (bomb_explode_)
        {
            Destroy(gameObject);
            Destroy(GameObject.FindGameObjectWithTag("ObjectiveProgress"));
        }
    }
}
