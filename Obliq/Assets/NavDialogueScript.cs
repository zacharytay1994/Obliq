using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDialogueScript : MonoBehaviour
{
    [SerializeField] List<GameObject> objects_to_destroy_;
    [SerializeField] GameObject nav_dialogue_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i<objects_to_destroy_.Count;i++)
        {
            if(objects_to_destroy_[i] == null)
            {
                objects_to_destroy_.RemoveAt(i);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MainPlayer") && objects_to_destroy_.Count<=0)
        {
            nav_dialogue_.SetActive(true);
        }
    }
}
