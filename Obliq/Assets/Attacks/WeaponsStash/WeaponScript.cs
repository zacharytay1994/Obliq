﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField]
    GameObject weapon_ = null;
    GameObject weapon_instance_ = null;
    [SerializeField]
    bool use_mouse_ = true;
    [SerializeField]
    KeyCode trigger_ = KeyCode.Space;
    //bool is_down_ = false;
    [SerializeField]
    bool has_recoil_ = true;
    bool recoil_start_ = false;
    [SerializeField]
    bool continuous_recoil_ = false;
    [SerializeField]
    float recoil_ = 0.0f;
    [SerializeField]
    int recoil_amount_ = 0;
    int recoil_amount_counter_ = 0;
    [SerializeField]
    float recoil_interval_ = 0.0f;
    float recoil_interval_counter_ = 0.0f;
    [SerializeField]
    float fixed_rate_ = 1.0f;
    float fixed_rate_counter_ = 0.0f;
    bool rate_reset_ = true;
    bool firing_ = false;
    // Start is called before the first frame update
    void Start()
    {
        recoil_interval_counter_ = recoil_interval_;
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon_ != GetComponent<TempWeaponSwitch>().weapon_2)
        {
            fixed_rate_counter_ = fixed_rate_;
        }
        if (weapon_ != null)
        {
            // incrememet rate reset
            if (!rate_reset_)
            {
                if (fixed_rate_counter_ < fixed_rate_)
                {
                    fixed_rate_counter_ += Time.deltaTime;
                }
                else
                {
                    fixed_rate_counter_ = 0.0f;
                    rate_reset_ = true;
                }
            }
            // input trigger
            if ((Input.GetKeyDown(trigger_) || (Input.GetMouseButtonDown(0) && use_mouse_)) && rate_reset_)
            {
                rate_reset_ = false;
                //is_down_ = true;
                weapon_instance_ = Instantiate(weapon_, transform.position, Quaternion.identity);
                weapon_instance_.GetComponent<ImAProjectile>().InitProj();
                // turn on recoil
                recoil_start_ = true;
                //firing_ = true;
            }
            if (Input.GetKeyUp(trigger_) || (Input.GetMouseButtonUp(0) && use_mouse_))
            {
                //is_down_ = false;
                Destroy(weapon_instance_);
                // turn off recoil
                recoil_start_ = false;
                ResetRecoil();
            }
            // make weapon follow player
            if (weapon_instance_ != null)
            {
                weapon_instance_.transform.position = gameObject.transform.position;
            }
            // update recoil
            if (has_recoil_)
            {
                Recoil();
            }
        }

        // weapon fix
        if (firing_ == true && !Input.GetKey(trigger_))
        {
            // key is up and weapon is firing
            firing_ = false;
            if (weapon_instance_ != null)
            {
                Destroy(weapon_instance_);
                // turn off recoil
                recoil_start_ = false;
                ResetRecoil();
            }
        }
    }

    void Recoil()
    {
        if (recoil_start_)
        {
            if (recoil_amount_counter_ < recoil_amount_ || continuous_recoil_)
            {
                if (recoil_interval_counter_ < recoil_interval_)
                {
                    recoil_interval_counter_ += Time.deltaTime;
                }
                else
                {
                    // get direction opposite mouse
                    Vector2 opp_direction = ((Vector2)gameObject.transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
                    // apply recoil
                    gameObject.GetComponent<Rigidbody2D>().AddForce(opp_direction * recoil_, ForceMode2D.Impulse);
                    recoil_amount_counter_++;
                    recoil_interval_counter_ = 0.0f;
                }
            }
            else
            {
                recoil_start_ = false;
                ResetRecoil();
            }
        }
    }

    void ResetRecoil()
    {
        recoil_amount_counter_ = 0;
        recoil_interval_counter_ = 0.0f;
    }

    public void SetWeapon(GameObject weapon, bool hasrecoil, bool continuousrecoil, float recoil, float recoilamount, float recoilinterval)
    {
        weapon_ = weapon;
        has_recoil_ = hasrecoil;
        continuous_recoil_ = continuousrecoil;
        recoil_ = recoil;
        recoil_amount_ = (int)recoilamount;
        recoil_interval_ = recoilinterval;
    }
    public GameObject GetWeapon()
    {
        return weapon_;
    }
}
