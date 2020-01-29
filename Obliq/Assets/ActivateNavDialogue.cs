using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNavDialogue : MonoBehaviour
{
    [SerializeField] GameObject object_to_activate_;
    [SerializeField] List<GameObject> objects_to_destroy_;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainPlayer") && objects_to_destroy_.Count<=0)
        {
            object_to_activate_.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}

