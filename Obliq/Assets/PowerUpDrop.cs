using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PowerUpDrop : MonoBehaviour
{
    [SerializeField] GameObject[] power_up_list_;
    UIManager ui_manager_;
    // Start is called before the first frame update
    void Start()
    {
       ui_manager_ = GameObject.Find("World").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnDestroy()
    {
        if (!ui_manager_.is_Quitting_)
        {
            if (gameObject != null)
            {
                int power_up_no = Random.Range(0, 3);
                if (power_up_list_[power_up_no] != null)
                {
                    GameObject powerup = Instantiate(power_up_list_[power_up_no]);
                    powerup.transform.position = gameObject.transform.position;
                }

            }
        }
        
    }
}
