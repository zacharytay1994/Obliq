using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWeaponSwitch : MonoBehaviour
{
    [SerializeField]
    KeyCode w1_switch_ = KeyCode.Alpha1;
    [SerializeField]
    GameObject weapon_1 = null;
    [SerializeField]
    bool w1_has_recoil = false;
    [SerializeField]
    bool w1_continuous_recoil_ = false;
    [SerializeField]
    Vector3 w1_recoil_data_ = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField]
    KeyCode w2_switch_ = KeyCode.Alpha2;
    [SerializeField]
    GameObject weapon_2 = null;
    [SerializeField]
    bool w2_has_recoil = false;
    [SerializeField]
    bool w2_continuous_recoil_ = false;
    [SerializeField]
    Vector3 w2_recoil_data_ = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField]
    KeyCode w3_switch_ = KeyCode.Alpha3;
    [SerializeField]
    GameObject weapon_3 = null;
    [SerializeField]
    bool w3_has_recoil = false;
    [SerializeField]
    bool w3_continuous_recoil_ = false;
    [SerializeField]
    Vector3 w3_recoil_data_ = new Vector3(0.0f, 0.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SwitchWeapon();
    }

    void SwitchWeapon()
    {
        if (Input.GetKeyDown(w1_switch_))
        {
            gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_1, w1_has_recoil, 
                w1_continuous_recoil_, w1_recoil_data_.x, w1_recoil_data_.y, w1_recoil_data_.z);
        }
        else if (Input.GetKeyDown(w2_switch_))
        {
            gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_2, w2_has_recoil,
                w2_continuous_recoil_, w2_recoil_data_.x, w2_recoil_data_.y, w2_recoil_data_.z);
        }
        else if (Input.GetKeyDown(w3_switch_))
        {
            gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_3, w3_has_recoil,
                w3_continuous_recoil_, w3_recoil_data_.x, w3_recoil_data_.y, w3_recoil_data_.z);
        }
    }
}
