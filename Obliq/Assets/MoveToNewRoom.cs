using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNewRoom : MonoBehaviour
{
    //get room database
    int room_database_size_ = 0;
    int room_number_ = 0;
    // Start is called before the first frame update
    void Start()
    {
        room_number_ = Random.Range(0, room_database_size_);
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
