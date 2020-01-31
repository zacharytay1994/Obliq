
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashFill : MonoBehaviour
{
    [SerializeField]
    float dashbar_fill_time_;
    Image dashbar_img_;
    bool filling_;
    // Start is called before the first frame update
    void Start()
    {
        dashbar_img_ = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        dashbar_img_.fillAmount += 1 / dashbar_fill_time_ * Time.deltaTime;

        if (dashbar_img_.fillAmount == 1)
        {
            dashbar_img_.enabled = false;
        }
        else
        {
            dashbar_img_.enabled = true;
        }
    }
    public void dashbar()
    {       
        dashbar_img_.fillAmount = 0.0f;
    }
}



