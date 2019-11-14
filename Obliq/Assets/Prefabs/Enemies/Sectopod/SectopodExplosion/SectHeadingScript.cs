using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectHeadingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            gameObject.GetComponent<ImAProjectile>().specified_direction_ = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        }
    }
}
