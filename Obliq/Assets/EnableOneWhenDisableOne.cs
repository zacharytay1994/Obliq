using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOneWhenDisableOne : MonoBehaviour
{
    [SerializeField] GameObject to_enable_;
    [SerializeField] GameObject disable_prev_text_;
    [SerializeField] GameObject to_disable_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(to_disable_!=null)
        {
            if (!to_disable_.activeInHierarchy)
            {
                to_enable_.SetActive(true);
                disable_prev_text_.SetActive(false);
                Destroy(this);
            }
        }
        else
        {
            to_enable_.SetActive(true);
            disable_prev_text_.SetActive(false);
            Destroy(this);
        }
    }
}
