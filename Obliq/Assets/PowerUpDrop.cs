using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDrop : MonoBehaviour
{
    [SerializeField] GameObject[] power_up_list_;
    bool is_quitting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnApplicationQuit()
    {
        is_quitting = true;
    }
    void OnDestroy()
    {
        if (!is_quitting)
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
