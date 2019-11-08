using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeartDisplay : MonoBehaviour
{
    [SerializeField] GameObject red_heart_display_;
    [SerializeField] GameObject black_heart_display_;
    [SerializeField] GameObject half_heart_display_;
    [SerializeField] HealthComponent player_health_component_;
    [Space(10)]
    [SerializeField] float display_size_;
    [SerializeField] float display_spacing_;
    float last_hp_= 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(player_health_component_.currentHp_!=last_hp_)
        {
            UpdateHealth();

            last_hp_ = player_health_component_.currentHp_;
        }
    }

    void UpdateHealth()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        int health = player_health_component_.currentHp_;
        int full_heart = 0;
        int black_heart = 0;
        if (health%2 != 0)
        {
            full_heart = (health - 1)/2;
            black_heart = (player_health_component_.maxHp_ / 2) - (full_heart + 1);
        }
        else
        {
            full_heart = health / 2;
            black_heart = (player_health_component_.maxHp_ / 2) - (full_heart);
        }
        int num_hearts = 0;
        for (int h = 0; h < full_heart; h++)
        {
            GameObject temp_heart = Instantiate(red_heart_display_, transform);
            temp_heart.GetComponent<RectTransform>().localPosition = new Vector3((display_size_ + display_spacing_) * (num_hearts + 1), -(display_size_ / 2) - display_spacing_, 0);
            num_hearts++;
        }
        if (health % 2 != 0)
        {
            for (int h = 0; h < 1; h++)
            {
                GameObject temp_heart = Instantiate(half_heart_display_, transform);
                temp_heart.GetComponent<RectTransform>().localPosition = new Vector3((display_size_ + display_spacing_) * (num_hearts + 1), -(display_size_ / 2) - display_spacing_, 0);
                num_hearts++;
            }
        }

        for (int h = 0; h < black_heart; h++)
        {
            GameObject temp_heart = Instantiate(black_heart_display_, transform);
            temp_heart.GetComponent<RectTransform>().localPosition = new Vector3((display_size_ + display_spacing_) * (num_hearts + 1), -(display_size_ / 2) - display_spacing_, 0);
            num_hearts++;
        }

        //temp_heart.GetComponent<RectTransform>().localPosition = new Vector3((display_size_ / 2 * (h + 1)) + display_spacing_, -(display_size_ / 2) - display_spacing_, 0);
    }
}
