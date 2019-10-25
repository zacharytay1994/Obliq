using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHandler : MonoBehaviour
{
    GameObject radius_one_;
    GameObject radius_two_;
    GameObject radius_three;
    // Start is called before the first frame update
    void Start()
    {
        radius_one_ = GameObject.Find("RadiusOne");
        radius_two_ = GameObject.Find("RadiusTwo");
        radius_three = GameObject.Find("RadiusThree");
        SetMoveRadiusActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMoveRadiusActive(bool active)
    {
        radius_one_.SetActive(active);
        radius_two_.SetActive(active);
        radius_three.SetActive(active);
    }
}
