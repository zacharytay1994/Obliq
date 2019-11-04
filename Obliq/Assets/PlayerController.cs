using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera camera_;
    [SerializeField] float player_speed_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerFacingDirection();

        
        if(Input.GetKey(KeyCode.W))
        {
            transform.position+= new Vector3(0.0f, player_speed_, 0.0f);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, -player_speed_, 0.0f);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-player_speed_, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(player_speed_, 0.0f, 0.0f);
        }
    }

    float PlayerFacingDirection()
    {
        Vector2 mouseLocation = Input.mousePosition;
        mouseLocation = camera_.ScreenToWorldPoint(mouseLocation);
        float angle = AngleBetween(transform.position, mouseLocation);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        return angle;
    }

    float AngleBetween(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y-b.y, a.x-b.x )*Mathf.Rad2Deg + 90;
    }
}
