using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDialogueCapPointTrigger : MonoBehaviour
{
    [SerializeField] GameObject nav_dialogue_;
    [SerializeField] GameObject portal_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(portal_.activeInHierarchy)
        {
            nav_dialogue_.SetActive(true);
        }
    }
}
