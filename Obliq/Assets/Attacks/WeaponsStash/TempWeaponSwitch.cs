using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWeaponSwitch : MonoBehaviour
{
    // weapon 1
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

    public bool lock_state_1_ = false;
    
    // weapon 2

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

    public bool lock_state_2_ = false;
   
    // weapon 3

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

    public bool lock_state_3_ = false;
    // weapon 4
    [SerializeField]
    KeyCode w4_switch_ = KeyCode.Alpha4;
    [SerializeField]
    GameObject weapon_4 = null;
    [SerializeField]
    bool w4_has_recoil = false;
    [SerializeField]
    bool w4_continuous_recoil_ = false;
    [SerializeField]
    Vector3 w4_recoil_data_ = new Vector3(0.0f, 0.0f, 0.0f);
    public bool lock_state_4_ = false;

    public bool weapon_auto_switch = false;
    // GUI WEAPONS
    SpriteRenderer weapon_1_sprite_;
    SpriteRenderer weapon_2_sprite_;
    SpriteRenderer weapon_3_sprite_;
    SpriteRenderer weapon_4_sprite_;


    // Start is called before the first frame update
    void Awake()
    {
        weapon_1_sprite_ = GameObject.Find("Weapon1").GetComponent<SpriteRenderer>();
        weapon_2_sprite_ = GameObject.Find("Weapon2").GetComponent<SpriteRenderer>();
        weapon_3_sprite_ = GameObject.Find("Weapon3").GetComponent<SpriteRenderer>();
        //weapon_4_sprite_ = GameObject.Find("Weapon4").GetComponent<SpriteRenderer>();

        GetGunStates();
        ChangeSelectedGUI(0);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchWeapon();
    }

    void SwitchWeapon()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1) || weapon_auto_switch)
        {
            if (lock_state_1_)
            {
                gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_1, w1_has_recoil,
                    w1_continuous_recoil_, w1_recoil_data_.x, w1_recoil_data_.y, w1_recoil_data_.z);
                ChangeSelectedGUI(0);
                weapon_auto_switch = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || weapon_auto_switch)
        {
            if (lock_state_2_)
            {
                gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_2, w2_has_recoil,
                    w2_continuous_recoil_, w2_recoil_data_.x, w2_recoil_data_.y, w2_recoil_data_.z);
                ChangeSelectedGUI(1);
                weapon_auto_switch = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || weapon_auto_switch)
        {
            if (lock_state_3_)
            {
                gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_3, w3_has_recoil,
                    w3_continuous_recoil_, w3_recoil_data_.x, w3_recoil_data_.y, w3_recoil_data_.z);
                ChangeSelectedGUI(2);
                weapon_auto_switch = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || weapon_auto_switch)
        {
            if (lock_state_4_)
            {
                gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_4, w4_has_recoil,
                    w4_continuous_recoil_, w4_recoil_data_.x, w4_recoil_data_.y, w4_recoil_data_.z);
                ChangeSelectedGUI(3);
                weapon_auto_switch = false;
            }
        }
    }

    void GetGunStates()
    {
        string r = TextIO.ReadFile("Gunstates.txt");
        lock_state_1_ = r[0] == '1' ? true : false;
        lock_state_2_ = r[1] == '1' ? true : false;
        lock_state_3_ = r[2] == '1' ? true : false;
        lock_state_4_ = r[3] == '1' ? true : false;
    }

    public void SaveGunStates()
    {
        string w = "";
        w += lock_state_1_ ? '1' : '0';
        w += lock_state_2_ ? '1' : '0';
        w += lock_state_3_ ? '1' : '0';
        w += lock_state_4_ ? '1' : '0';
        TextIO.WriteFile(w, "Gunstates.txt");
    }

    void ChangeSelectedGUI(int i)
    {
        //Vector3 trmp = gameObject.transform.localScale;
        //trmp.x *= 2;
        //gameObject.transform.localScale = trmp;
        // set everything to 10% transparency
        Color c = new Color(1.0f, 1.0f, 1.0f, 0.3f);
        Color c_0 = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        weapon_1_sprite_.color = lock_state_1_ ? c : c_0;
        weapon_2_sprite_.color = lock_state_2_ ? c : c_0;
        weapon_3_sprite_.color = lock_state_3_ ? c : c_0;
        //weapon_4_sprite_.color = lock_state_4_ ? c : c_0;

        c.a = 1.0f;
        // set selected to full alpha
        switch (i)
        {
            case 0:
                if (lock_state_1_)
                {
                    weapon_1_sprite_.color = c;
                }
                break;
            case 1:
                if (lock_state_2_)
                {
                    weapon_2_sprite_.color = c;
                }
                break;
            case 2:
                if (lock_state_3_)
                {
                    weapon_3_sprite_.color = c;
                }
                break;
            case 3:
                //weapon_4_sprite_.color = c;
                break;
        }
    }

    public void UpdateWeapon(int n)
    {
        switch(n)
        {
            case 1:
                gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_1, w1_has_recoil,
                    w1_continuous_recoil_, w1_recoil_data_.x, w1_recoil_data_.y, w1_recoil_data_.z);
                ChangeSelectedGUI(0);
                break;
            case 2:
                gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_2, w2_has_recoil,
                    w2_continuous_recoil_, w2_recoil_data_.x, w2_recoil_data_.y, w2_recoil_data_.z);
                ChangeSelectedGUI(1);
                break;
            case 3:
                gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_3, w3_has_recoil,
                    w3_continuous_recoil_, w3_recoil_data_.x, w3_recoil_data_.y, w3_recoil_data_.z);
                ChangeSelectedGUI(2);
                break;
            case 4:
                //gameObject.GetComponent<WeaponScript>().SetWeapon(weapon_4, w4_has_recoil,
                //    w4_continuous_recoil_, w4_recoil_data_.x, w4_recoil_data_.y, w4_recoil_data_.z);
                //ChangeSelectedGUI(3);
                break;
        }
    }
}
