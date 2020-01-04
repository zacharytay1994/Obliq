using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField]
    [Range(1, 4)]
    int weapon_no_;
    [SerializeField]
    float pickup_radius_;
    GameObject player_;
    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
        if (player_ != null)
        {
            Debug.Log(((Vector2)player_.transform.position - (Vector2)gameObject.transform.position).magnitude);
            if (((Vector2)player_.transform.position - (Vector2)gameObject.transform.position).magnitude < pickup_radius_)
            {
                switch (weapon_no_)
                {
                    case 1:
                        player_.GetComponent<TempWeaponSwitch>().lock_state_1_ = true;                       
                        break;
                    case 2:
                        player_.GetComponent<TempWeaponSwitch>().lock_state_2_ = true;
                        break;
                    case 3:
                        player_.GetComponent<TempWeaponSwitch>().lock_state_3_ = true;
                        break;
                    case 4:
                        player_.GetComponent<TempWeaponSwitch>().lock_state_4_ = true;
                        break;
                    default:
                        break;
                }
                Destroy(gameObject);
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
       
          


        
    }
}
