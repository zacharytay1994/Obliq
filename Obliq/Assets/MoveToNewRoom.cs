using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNewRoom : MonoBehaviour
{
    //get room database
    int room_database_size_ = 100;
    int room_number_ = 0;
    Color room_color_ = Color.blue;
    // Start is called before the first frame update
    void Start()
    {
        room_number_ = Random.Range(0, room_database_size_);
        //set room color based on room difficulty/type
        if(room_number_>=0&&room_number_<20)
        {
            room_color_ = Color.blue;
        }
        else if (room_number_ >= 20 && room_number_ < 40)
        {
            room_color_ = Color.green;
        }
        else if (room_number_ >= 40 && room_number_ < 60)
        {
            room_color_ = Color.yellow;
        }
        else if (room_number_ >= 60 && room_number_ < 80)
        {
            room_color_ = Color.red;
        }
        else if (room_number_ >= 80 && room_number_ < 100)
        {
            room_color_ = Color.magenta;
        }

        GetComponent<SpriteRenderer>().color = room_color_;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //loads room number
    }
}
