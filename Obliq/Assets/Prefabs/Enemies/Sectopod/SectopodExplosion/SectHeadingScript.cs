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
        gameObject.GetComponent<ImAProjectile>().specified_direction_ = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        if (player != null)
        {
            LayerMask layerMask = LayerMask.GetMask("Walls");

            RaycastHit2D isHit = Physics2D.Raycast(transform.position,
                ((Vector2)player.transform.position - (Vector2)transform.position).normalized,
             ((Vector2)transform.position -
             (Vector2)player.transform.position).magnitude, layerMask);

            if (isHit.collider == null)
            {
                gameObject.GetComponent<ImAProjectile>().SetPause(false);
            }
            else
            {
                gameObject.GetComponent<ImAProjectile>().SetPause(true);
            }
        }
    }
}
