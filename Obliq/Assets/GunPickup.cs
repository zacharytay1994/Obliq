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
    AudioManager am_;
    [SerializeField]
    string pickup_sound_;
    // Start is called before the first frame update
    void Start()
    {
        am_ = FindObjectOfType<AudioManager>();
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
                player_.GetComponent<TempWeaponSwitch>().weapon_auto_switch = true;
                switch (weapon_no_)
                {
                    case 1:
                        am_.PlaySound(pickup_sound_);
                        player_.GetComponent<TempWeaponSwitch>().lock_state_1_ = true;
                        break;
                    case 2:
                        am_.PlaySound(pickup_sound_);
                        player_.GetComponent<TempWeaponSwitch>().lock_state_2_ = true;
                        break;
                    case 3:
                        am_.PlaySound(pickup_sound_);
                        player_.GetComponent<TempWeaponSwitch>().lock_state_3_ = true;
                        break;
                    case 4:
                        am_.PlaySound(pickup_sound_);
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
