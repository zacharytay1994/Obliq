using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3Script : MonoBehaviour
{
    [SerializeField] GameObject tutorial_3_text_;
    [SerializeField] List<GameObject> enemies_needed_to_be_deleted_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies_needed_to_be_deleted_.Count == 0)
        {
            tutorial_3_text_.SetActive(true);
            Destroy(this);
        }
    }
}
