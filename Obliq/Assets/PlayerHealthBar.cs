using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    HealthComponent player_health_component_;
    [SerializeField] GameObject health_unit_;
    [SerializeField] GameObject health_loss_effect_;
    [SerializeField] float display_size_;
    [SerializeField] float display_spacing_;
    int num_healthbar;   
    // Start is called before the first frame update
    void Start()
    {
        player_health_component_ = GameObject.Find("Player").GetComponent<HealthComponent>();
        
        SpawnHealthbar();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDamage();
    }
    public void SpawnHealthbar()
    {
        for(int i = 0; i < player_health_component_.getCurrentHp(); i++)
        {
            GameObject health_unit = Instantiate(health_unit_, transform);
            health_unit.transform.SetParent(gameObject.transform);
            health_unit.GetComponent<RectTransform>().localPosition = new Vector3((display_size_ + display_spacing_) * (i + 1), -(display_size_ / 2) - display_spacing_, 0);
            num_healthbar++;
        }

    }
    void CheckForDamage()
    {
        if (player_health_component_.getCurrentHp() < num_healthbar && num_healthbar > 0 && transform.childCount > 0)
        {
            GameObject temp = Instantiate(health_loss_effect_, transform.GetChild(num_healthbar - 1).gameObject.transform.position, Quaternion.identity);
            temp.transform.SetParent(GameObject.Find("Player_UI").transform);
            Destroy(transform.GetChild(num_healthbar - 1).gameObject);
            num_healthbar--;
        }
        if(player_health_component_.getCurrentHp() <= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        if(player_health_component_.getCurrentHp() > num_healthbar)
        {
            GameObject health_unit = Instantiate(health_unit_, transform);
            health_unit.transform.SetParent(gameObject.transform);
            health_unit.GetComponent<RectTransform>().localPosition = new Vector3((display_size_ + display_spacing_) * (num_healthbar + 1),
                -(display_size_ / 2) - display_spacing_, 0);
        }
    }
}
