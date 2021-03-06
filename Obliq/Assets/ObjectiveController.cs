﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectiveController : MonoBehaviour
{
    private float start_time_;
    float defuse_timer = 0.0f;
    bool defused = false;
    // Start is called before the first frame update
    void Start()
    {
        start_time_ = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        defuseBomb();
    }
    bool defuseBomb()
    {      
        if (Time.time - start_time_ >= 10 && defused == false)
        {
            Debug.Log("DEAD");
            return defused;
            //Application.Quit(); this is to close to program, does not work when testing with editor i.e pressing the play button 
        }
        if ((GameObject.Find("Entities/GoodGuys/GoodGuy").transform.position - GameObject.Find("Entities/Objective").transform.position).magnitude < 3.0f)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Debug.Log(defuse_timer += Time.deltaTime);
                defuse_timer += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                if (defuse_timer > 2.0f)
                {
                    Debug.Log("Alive");
                    defused = true;
                    return defused;
                }
                else
                {
                    defuse_timer = 0.0f;
                }
            }          
        }
        return defused;
    }
}



