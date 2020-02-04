using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleAlphaToObjectDistance : MonoBehaviour
{
    bool player_within_trigger_;
    float alpha_;
    [SerializeField] float alpha_roc_;
    TextMeshPro prisoner_wails_;
    
    // Start is called before the first frame update
    void Start()
    {
        player_within_trigger_ = false;
        alpha_ = 0;
        prisoner_wails_ = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player_within_trigger_)
        {
            if(alpha_<1)
            {
                alpha_ += alpha_roc_;
            }
        }
        else
        {
            if(alpha_>0)
            {
                alpha_ -= alpha_roc_;
            }

        }

        prisoner_wails_.color = new Color(1, 1, 1, alpha_);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MainPlayer"))
        {
            player_within_trigger_ = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainPlayer"))
        {
            player_within_trigger_ = false;
        }
        
    }

}
