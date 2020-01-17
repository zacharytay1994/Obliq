using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBombDetection : MonoBehaviour
{
    GameObject player_;
    public bool player_within_range_improved_ = false;

    private void Start()
    {
        player_ = GameObject.FindGameObjectWithTag("MainPlayer");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MainPlayer"))
        {
            player_within_range_improved_ = true;
        }     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainPlayer"))
        {
            player_within_range_improved_ = false;
        }
    }
}
