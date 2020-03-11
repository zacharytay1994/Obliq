using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDialogueCapPointTrigger : MonoBehaviour
{
    [SerializeField] GameObject nav_dialogue_;
    [SerializeField] GameObject prev_nav_dialogue_;
    [SerializeField] GameObject portal_;
    Portal portal_script_;
    // Start is called before the first frame update
    void Start()
    {
        portal_script_ = portal_.GetComponentInParent<Portal>();
    }

    // Update is called once per frame
    void Update()
    {
        if(portal_script_.activate_portal_)
        {
            nav_dialogue_.SetActive(true);

                prev_nav_dialogue_.SetActive(false);
            
        }
    }
}
