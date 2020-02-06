using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectOnPlayerTrigger : MonoBehaviour
{
    [SerializeField] GameObject object_to_activate_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("MainPlayer"))
    //    {
    //        object_to_activate_.SetActive(true);
    //        Destroy(this.gameObject);
    //    }
    //}
}
